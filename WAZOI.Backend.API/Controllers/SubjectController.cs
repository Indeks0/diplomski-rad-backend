using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Infrastructure;
using WAZOI.Backend.Models;
using WAZOI.Backend.Models.Common;
using WAZOI.Backend.Services.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WAZOI.Backend.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        #region Fields

        protected readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ISubjectService _service;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ISubjectTeacherService _subjectTeacherService;

        #endregion Fields

        #region Constructors

        public SubjectController(ISubjectService service,
            RoleManager<IdentityRole<Guid>> roleManager, UserManager<User> userManager, IMapper mapper,
            ISubjectTeacherService subjectTeacherService)
        {
            _service = service;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _subjectTeacherService = subjectTeacherService;
        }

        #endregion Constructors

        #region Methods

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result != null)
            {
                return Ok(result);
            }
            else { return NotFound(); }
        }

        [HttpGet]
        [Route("find-subjects")]
        public async Task<IActionResult> FindSubjectsAsync(int page = DefaultApplicationValues.DefaultPage, int rpp = DefaultApplicationValues.DefaultRpp, string? searchQuery = null)
        {
            var result = await _service.FindSubjectsAsync(page, rpp, searchQuery);

            return Ok(
                    new
                    {
                        items = result.Subjects,
                        totalCount = result.TotalCount
                    });
        }

        [HttpGet]
        [Route("find-subjects-with-joinStatus")]
        public async Task<IActionResult> FindSubjectsWithJoinedStatusAsync(int page = DefaultApplicationValues.DefaultPage, int rpp = DefaultApplicationValues.DefaultRpp, string? searchQuery = null)
        {
            var user = await _userManager.GetUserAsync(User);
            var userRole = (await _userManager.GetRolesAsync(user)).First();
            var result = await _service.FindSubjectsWithJoinedStatusAsync(user.Id, userRole, page, rpp, searchQuery);

            return Ok(
                    new
                    {
                        items = result.Subjects,
                        totalCount = result.TotalCount
                    });
        }

        [HttpGet]
        [Route("find-subjects-by-user")]
        public async Task<IActionResult> FindSubjectsByUserAsync(int page = DefaultApplicationValues.DefaultPage, int rpp = DefaultApplicationValues.DefaultRpp, string? searchQuery = null)
        {
            var user = await _userManager.GetUserAsync(User);
            var userRole = (await _userManager.GetRolesAsync(user)).First();

            var result = await _service.FindSubjectsForUserAsync(user.Id, userRole, page, rpp, searchQuery);

            return Ok(
                    new
                    {
                        items = result.Subjects,
                        totalCount = result.TotalCount
                    });
        }

        [HttpPost]
        [Route("create-subject")]
        public async Task<IActionResult> PostAsync([FromBody] SubjectRest subject)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await _service.CreateSubjectAsync(_mapper.Map<ISubjectDto>(subject), userId);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("add-teacher")]
        public async Task<IActionResult> AddTeacherAsync(string email, Guid subjectId)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userRole = (await _userManager.GetRolesAsync(user)).First();

            if (user != null && userRole == UserRoles.Teacher)
            {
                var result = await _subjectTeacherService.InsertTeacherAsync(user.Id, subjectId);

                if (result > 0)
                {
                    return Ok();
                }
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("get-subject-teachers")]
        public async Task<IActionResult> FindSubjectTeachersAsync(Guid subjectId)
        {
            var result = await _subjectTeacherService.FindSubjectTeachersAsync(subjectId);

            return Ok(new { items = result });
        }

        [HttpGet]
        [Route("get-is-teacher-admin")]
        public async Task<IActionResult> GetIsTeacherAdminAsync(Guid subjectId)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await _subjectTeacherService.GetIsTeacherAdminAsync(userId, subjectId);

            return Ok(result);
        }

        [HttpPut]
        [Route("update-subject")]
        public async Task<IActionResult> UpdateSubjectAsync(SubjectRest subject)
        {
            var domainSubject = _mapper.Map<ISubjectDto>(subject);

            var result = await _service.UpdateAsync(domainSubject);

            return Ok(result);
        }

        [HttpDelete]
        [Route("delete-subject")]
        public async Task<IActionResult> DeleteSubjectAsync([FromBody] SubjectRest subject)
        {
            var domainSubject = _mapper.Map<ISubjectDto>(subject);

            var result = await _service.DeleteAsync(domainSubject);

            return Ok(result);
        }

        [HttpDelete]
        [Route("delete-teacher")]
        public async Task<IActionResult> DeleteSubjectTeacherAsync([FromBody] SubjectTeacherRest subjectTeacher)
        {
            var domainSubject = _mapper.Map<ISubjectTeacherDto>(subjectTeacher);

            var result = await _subjectTeacherService.DeleteAsync(domainSubject);

            return Ok(result);
        }

        #endregion Methods
    }

    public class SubjectRest
    {
        #region Properties

        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Color { get; set; }

        #endregion Properties
    }

    public class SubjectTeacherRest
    {
        #region Properties

        public Guid? Id { get; set; }

        #endregion Properties
    }
}