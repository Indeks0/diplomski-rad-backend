using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Models.Common;
using WAZOI.Backend.Services.Common;

namespace WAZOI.Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        #region Fields

        protected readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IQuestionService _service;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        #endregion Fields

        #region Constructors

        public QuestionController(IQuestionService service,
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
        [Route("find-questions")]
        public async Task<IActionResult> FindQuestions(Guid subjectId, string type = null)
        {
            var result = await _service.FindQuestionsAsync(subjectId, type);

            return Ok(result);
        }

        [HttpDelete]
        [Route("delete-questions")]
        public async Task<IActionResult> DeleteQuestions([FromBody] QuestionsListRest questions)
        {
            var questionsDomain = _mapper.Map<List<IQuestionDto>>(questions.Questions);
            var result = await _service.DeleteRangeAsync(questionsDomain);

            if (result > 0)
            {
                return Ok(result);
            }
            else { return BadRequest(); }
        }

        [HttpPost]
        [Route("create-questions")]
        public async Task<IActionResult> CreateQuestions([FromBody] QuestionsListRest questions)
        {
            var questionsDomain = _mapper.Map<List<IQuestionDto>>(questions.Questions);
            var result = await _service.CreateQuestionsAsync(questionsDomain);

            if (result > 0)
            {
                return Ok(result);
            }
            else { return BadRequest(); }
        }

        #endregion Methods
    }

    public class QuestionsListRest
    {
        #region Properties

        public List<QuestionRest> Questions { get; set; }

        #endregion Properties
    }

    public class QuestionRest
    {
        #region Properties

        public Guid Id { get; set; }
        public Guid SubjectId { get; set; }
        public string? Content { get; set; }
        public string? Type { get; set; }

        #endregion Properties
    }
}