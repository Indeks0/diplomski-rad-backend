using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Infrastructure;
using WAZOI.Backend.Models.Common;
using WAZOI.Backend.Services.Common;

namespace WAZOI.Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSubjectJoinRequestController : ControllerBase
    {
        #region Fields

        protected readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserSubjectJoinRequestService _service;

        #endregion Fields

        #region Constructors

        public UserSubjectJoinRequestController(IUserSubjectJoinRequestService service, UserManager<User> userManager, IMapper mapper)
        {
            _service = service;
            _userManager = userManager;
            _mapper = mapper;
        }

        #endregion Constructors

        #region Methods

        [HttpGet]
        [Route("get-requests")]
        public async Task<IActionResult> FindSubjectJoinRequests(Guid subjectId, int page = DefaultApplicationValues.DefaultPage, int rpp = DefaultApplicationValues.DefaultRpp)
        {
            var result = await _service.FindJoinRequestsAsync(subjectId, page, rpp);

            return Ok(new
            {
                items = result.Requests,
                totalCount = result.TotalCount
            });
        }

        [HttpPost]
        [Route("create-request")]
        public async Task<IActionResult> CreateJoinRequest(UserSubjectJoinRequestRest joinRequest)
        {
            joinRequest.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await _service.CreateJoinRequestAsync(_mapper.Map<IUserSubjectJoinRequestDto>(joinRequest));

            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("accept-request")]
        public async Task<IActionResult> AcceptJoinRequest(UserSubjectJoinRequestRest joinRequest)
        {
            var result = await _service.AcceptJoinRequestAsync(_mapper.Map<IUserSubjectJoinRequestDto>(joinRequest));

            if (result)
            {
                return Ok(result);
            }
            else { return BadRequest(); }
        }

        [HttpDelete]
        [Route("decline-request")]
        public async Task<IActionResult> DeclineJoinRequest([FromBody] UserSubjectJoinRequestRest joinRequest)
        {
            var result = await _service.DeclineJoinRequestAsync(_mapper.Map<IUserSubjectJoinRequestDto>(joinRequest));

            if (result)
            {
                return Ok(result);
            }
            else { return BadRequest(); }
        }

        #endregion Methods
    }

    public class UserSubjectJoinRequestRest
    {
        #region Properties

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid SubjectId { get; set; }

        #endregion Properties
    }
}