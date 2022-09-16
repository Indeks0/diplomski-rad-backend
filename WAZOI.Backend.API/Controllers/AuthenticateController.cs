using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Infrastructure;
using WAZOI.Backend.Models;
using WAZOI.Backend.Services.Common.MailService;

#nullable disable

namespace WAZOI.Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        #region Fields

        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;
        private IOptions<FrontendDomainOptions> _frontendDomainOptions { get; }

        #endregion Fields

        #region Constructors

        public AuthenticateController(
            UserManager<User> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            IConfiguration configuration, IEmailSender emailSender,
            IOptions<FrontendDomainOptions> frontendDomainOptions)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailSender = emailSender;
            _frontendDomainOptions = frontendDomainOptions;
        }

        #endregion Constructors

        #region Methods

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDataRest loginData)
        {
            var user = await _userManager.FindByEmailAsync(loginData.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginData.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = CreateToken(authClaims);
                var refreshToken = GenerateRefreshToken();

                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokenValidityInDays);

                await _userManager.UpdateAsync(user);

                return Ok(new
                {
                    Email = loginData.Email,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo,
                    Role = userRoles.First(),
                    Name = user.Name,
                    Surname = user.Surname
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            var accessToken = tokenModel.AccessToken;
            var refreshToken = tokenModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            string email = principal.Identity.Name;

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newAccessToken = CreateToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                Expiration = newAccessToken.ValidTo,
                refreshToken = newRefreshToken
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDataRest registerData)
        {
            var userExists = await _userManager.FindByEmailAsync(registerData.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status409Conflict,
                    new Response { Status = "Error", Message = "User already exists!" });

            User user = new()
            {
                UserName = registerData.Username,
                Email = registerData.Email,
                Name = registerData.Name,
                Surname = registerData.Surname,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            var result = await _userManager.CreateAsync(user, registerData.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "User creation failed!" });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Student))
                await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRoles.Student));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Teacher))
                await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRoles.Teacher));

            if (registerData.Role == UserRoles.Student)
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Student);
            }
            if (registerData.Role == UserRoles.Teacher)
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Teacher);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            await _emailSender.SendRegistrationEmailAsync(user.Email, token);

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var tokenEscaped = Uri.UnescapeDataString(token);
            var result = await _userManager.ConfirmEmailAsync(user, tokenEscaped);

            if (!result.Succeeded)
            {
                return BadRequest();
            }
            else
            {
                return Ok(new
                {
                    Status = true,
                    Message = "Your email is confirmed succesfully",
                    StatusCode = System.Net.HttpStatusCode.OK,
                });
            }
        }

        [HttpGet]
        [Route("get-password-token")]
        public async Task<IActionResult> SendPasswordResetTokenToEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User not found!" });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            await _emailSender.SendEmailAsync(user.Email, "Promjena lozinke", "Pozdrav, sljedeći kod iskoristite za promjenu lozinke: " + token);

            return Ok(new
            {
                Status = true,
                Message = "Provjerite Vaš email",
                StatusCode = System.Net.HttpStatusCode.OK.ToString(),
            });
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword(string email, string token, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User not found!" });

            var result = await _userManager.ResetPasswordAsync(user, token, password);

            return Ok(new
            {
                Status = true,
                Message = "Lozinka izmjenjena",
                StatusCode = System.Net.HttpStatusCode.OK.ToString(),
            });
        }

        [HttpPost]
        [Route("reset-password-logged-in")]
        public async Task<IActionResult> ResetPasswordLoggedIn(string email, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User not found!" });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            return Ok(new
            {
                Status = true,
                Message = "Lozinka izmjenjena",
                StatusCode = System.Net.HttpStatusCode.OK.ToString(),
            });
        }

        [Authorize]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return BadRequest("Invalid user name");

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return NoContent();
        }

        [Authorize]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }

            return NoContent();
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        #endregion Methods

        #region Classes

        public class LoginDataRest
        {
            #region Properties

            public string? Email { get; set; }

            public string? Password { get; set; }

            #endregion Properties
        }

        public class RegisterDataRest
        {
            #region Properties

            public string? Email { get; set; }
            public string? Name { get; set; }
            public string? Password { get; set; }
            public string? Surname { get; set; }
            public string? Username { get; set; }
            public string? Role { get; set; }

            #endregion Properties
        }

        public class Response
        {
            #region Properties

            public string? Message { get; set; }
            public string? Status { get; set; }

            #endregion Properties
        }

        public class TokenModel
        {
            #region Properties

            public string? AccessToken { get; set; }
            public string? RefreshToken { get; set; }

            #endregion Properties
        }

        #endregion Classes
    }
}