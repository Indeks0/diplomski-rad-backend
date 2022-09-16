using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Infrastructure;
using WAZOI.Backend.Models.Common;
using WAZOI.Backend.Services.Common;
using WAZOI.Backend.Services.Common.MailService;

namespace WAZOI.Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        #region Fields

        protected readonly IMapper _mapper;
        private readonly IExamService _service;
        private readonly IExamStudentService _examStudentService;
        private ISubjectStudentService _subjectStudentService;

        #endregion Fields

        #region Constructors

        public ExamController(IExamService service,
            IExamStudentService examStudentService,
            IMapper mapper,
            ISubjectStudentService subjectStudentService)
        {
            _service = service;
            _examStudentService = examStudentService;
            _mapper = mapper;
            _subjectStudentService = subjectStudentService;
        }

        #endregion Constructors

        #region Methods

        [HttpPost]
        [Route("create-exam")]
        public async Task<IActionResult> CreateExam([FromBody] ExamRest exam)
        {
            var examDomain = _mapper.Map<IExamDto>(exam);

            if (exam.SendNotification)
            {
                var students = await _subjectStudentService.FindSubjectStudentsAsync(exam.SubjectId, 1, 1000, null);

                _subjectStudentService.SendEmailsToStudentsAsync(students, examDomain, exam.SubjectName);
            }
            var result = await _service.CreateExamAsync(examDomain);
            if (result > 0)
            {
                return Ok
                    (result);
            }
            else { return BadRequest(); }
        }

        [HttpGet]
        [Route("get-student-exams")]
        public async Task<IActionResult> GetStudentExams(Guid subjectId, Guid studentId)
        {
            var result = await _service.GetExamsForStudentAsync(subjectId, studentId);

            return Ok
                (new
                {
                    OpenExams = result.OpenExams,
                    FutureExams = result.FutureExams,
                });
        }

        [HttpGet]
        [Route("get-student-exam")]
        public async Task<IActionResult> GetStudentExam(Guid examId, Guid studentId)
        {
            var studentExam = await _examStudentService.GetStudentExamAsync(examId, studentId);
            if (!studentExam.HasExamAccess)
            {
                return BadRequest();
            }

            var result = await _service.GetExamForSolvingAsync(examId);

            if (result != null)
            {
                return Ok
                    (new
                    {
                        exam = result,
                        studentExamId = studentExam.Id,
                    });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("solved-exam-submit")]
        public async Task<IActionResult> PostSolvedExam(SolvedExamSubmitRest solvedExamSubmit)
        {
            var solvedExamSubmitDomain = _mapper.Map<ISolvedExamSubmitDto>(solvedExamSubmit);

            var result = await _service.CalculateAndSaveSolvedExamAsync(solvedExamSubmitDomain);

            if (result)
            {
                return Ok
                    (result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("get-open-exams")]
        public async Task<IActionResult> GetOpenExams(Guid subjectId)
        {
            var result = await _service.GetOpenExamsAsync(subjectId);

            return Ok
                (result);
        }

        [HttpGet]
        [Route("get-future-exams")]
        public async Task<IActionResult> GetFutureExams(Guid subjectId)
        {
            var result = await _service.GetFutureExamsAsync(subjectId);

            return Ok
                (result);
        }

        [HttpGet]
        [Route("get-locked-exams")]
        public async Task<IActionResult> GetLockedExams(Guid subjectId)
        {
            var result = await _service.GetLockedExamsAsync(subjectId);

            return Ok
                (result);
        }

        [HttpDelete]
        [Route("delete-exam")]
        public async Task<IActionResult> DeleteExam(Guid id)
        {
            var result = await _service.DeleteAsync(id);

            if (result)
            {
                return Ok
                    (result);
            }
            else { return BadRequest(); }
        }

        [HttpGet]
        [Route("get-solved-exams")]
        public async Task<IActionResult> GetSolvedExamsAsync(Guid examId)
        {
            var result = await _service.GetSolvedExamsAsync(examId);

            return Ok
                (new
                {
                    items = result,
                });
        }

        [HttpPost]
        [Route("lock-solved-exams")]
        public async Task<IActionResult> LockSolvedExamsAsync(Guid examId)
        {
            var result = await _service.LockSolvedExamsAsync(examId);

            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        #endregion Methods
    }

    public class ExamRest
    {
        #region Properties

        public Guid Id { get; set; }
        public Guid SubjectGradingCriteriaId { get; set; }
        public string? Name { get; set; }
        public string? SubjectName { get; set; }
        public string? Description { get; set; }
        public int DurationInMins { get; set; }
        public bool RandomizedQuestionsOrder { get; set; }
        public bool SendNotification { get; set; }

        public DateTime? DateOpenStart { get; set; }
        public DateTime? DateOpenEnd { get; set; }

        public Guid SubjectId { get; set; }
        public virtual List<ExamQuestionRest>? Questions { get; set; }
        public List<ExamStudentRest>? Students { get; set; }

        #endregion Properties
    }

    public class ExamQuestionRest
    {
        #region Properties

        public Guid QuestionId { get; set; }
        public int Points { get; set; }

        #endregion Properties
    }

    public class SolvedExamSubmitRest
    {
        #region Properties

        public Guid ExamId { get; set; }
        public Guid StudentExamId { get; set; }
        public List<SolvedExamAnswerRest>? Answers { get; set; }

        #endregion Properties
    }

    public class SolvedExamAnswerRest
    {
        #region Properties

        public Guid Id { get; set; }

        public string Answer { get; set; }

        #endregion Properties
    }
}