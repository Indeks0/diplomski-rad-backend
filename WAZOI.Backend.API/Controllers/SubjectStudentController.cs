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
    public class SubjectStudentController : ControllerBase
    {
        #region Fields

        protected readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ISubjectStudentService _service;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        #endregion Fields

        #region Constructors

        public SubjectStudentController(ISubjectStudentService service,
            RoleManager<IdentityRole<Guid>> roleManager, UserManager<User> userManager, IMapper mapper)
        {
            _service = service;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        #endregion Constructors

        #region Methods

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> FindSubjectStudents(Guid subjectId, int page = DefaultApplicationValues.DefaultPage, int rpp = DefaultApplicationValues.DefaultRpp, string? searchQuery = null)
        {
            var result = await _service.FindSubjectStudentsAsync(subjectId, page, rpp, searchQuery);

            return Ok(
                    new
                    {
                        items = result.Students,
                        totalCount = result.TotalCount
                    });
        }

        [HttpGet]
        [Route("subject-student")]
        public async Task<IActionResult> GetSubjectStudent(Guid subjectId)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _service.GetSubjectStudentAsync(subjectId, user.Id);

            if (result != null)
            {
                return Ok(result);
            }
            else { return BadRequest(); }
        }

        [HttpDelete]
        [Route("delete-student")]
        public async Task<IActionResult> DeleteSubjectStudentAsync([FromBody] SubjectStudentRest subjectStudent)
        {
            var result = await _service.DeleteAsync(subjectStudent.Id.Value);

            return Ok(result);
        }

        #endregion Methods
    }

    public class SubjectStudentRest
    {
        #region Properties

        public Guid? Id { get; set; }
        public Guid? SubjectId { get; set; }

        public Guid? UserId { get; set; }

        #endregion Properties
    }
}