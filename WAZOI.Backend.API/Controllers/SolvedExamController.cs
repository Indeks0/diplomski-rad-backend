using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Models;
using WAZOI.Backend.Models.Common;
using WAZOI.Backend.Services.Common;

namespace WAZOI.Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolvedExamController : ControllerBase
    {
        #region Fields

        protected readonly IMapper _mapper;
        private readonly ISolvedExamService _service;
        private readonly ISubjectStudentService _subjectStudentService;
        private readonly UserManager<User> _userManager;

        #endregion Fields

        #region Constructors

        public SolvedExamController(ISolvedExamService service, IMapper mapper, UserManager<User> userManager, ISubjectStudentService subjectStudentService)
        {
            _service = service;
            _mapper = mapper;
            _userManager = userManager;
            _subjectStudentService = subjectStudentService;
        }

        #endregion Constructors

        #region Methods

        [HttpGet]
        [Route("get-solved-exam")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result != null)
            {
                return Ok(result);
            }
            else { return BadRequest(); }
        }

        [HttpPut]
        [Route("update-solved-exam")]
        public async Task<IActionResult> UpdateSolvedExamAsync(SolvedExamRest solvedExam)
        {
            var result = await _service.UpdateAndGradeSolvedExamAsync(_mapper.Map<ISolvedExamDto>(solvedExam), solvedExam.ExamId);
            if (result > 0)
            {
                return Ok(result);
            }
            else { return BadRequest(); }
        }

        [HttpGet]
        [Route("get-student-solved-exams")]
        public async Task<IActionResult> GetStudentSolvedExamsAsync(Guid studentId, Guid subjectId)
        {
            var user = await _userManager.GetUserAsync(User);
            var userRole = (await _userManager.GetRolesAsync(user)).First();
            if (userRole.Contains(UserRoles.Student))
            {
                var subjectStudent = await _subjectStudentService.GetSubjectStudentAsync(subjectId, user.Id);
                subjectId = subjectStudent.Id;
            }
            var result = await _service.GetStudentSolvedExamsAsync(studentId);
            if (result != null)
            {
                return Ok(
                    new { items = result });
            }
            else { return BadRequest(); }
        }

        #endregion Methods
    }

    public class SolvedExamRest
    {
        #region Properties

        public Guid Id { get; set; }
        public Guid ExamId { get; set; }
        public string Content { get; set; }
        public float MaximumPoints { get; set; }
        public float ScoredPoints { get; set; }
        public float ScoredPercentage { get; set; }
        public bool isLocked { get; set; }
        public int Grade { get; set; }

        #endregion Properties
    }
}