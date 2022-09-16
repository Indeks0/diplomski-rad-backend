using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WAZOI.Backend.Models.Common;
using WAZOI.Backend.Services.Common;

namespace WAZOI.Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectGradingCriteriaController : ControllerBase
    {
        #region Fields

        protected readonly IMapper _mapper;
        private readonly ISubjectGradingCriteriaService _service;

        #endregion Fields

        #region Constructors

        public SubjectGradingCriteriaController(ISubjectGradingCriteriaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        #endregion Constructors

        #region Methods

        [HttpGet]
        [Route("get-by-subject")]
        public async Task<IActionResult> GetAllBySubjectAsync(Guid subjectId)
        {
            var result = await _service.GetAllBySubjectAsync(subjectId);
            return Ok(result);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync(SubjectGradingCriteriaRest subjectGradingCriteria)
        {
            var result = await _service.InsertAsync(_mapper.Map<ISubjectGradingCriteriaDto>(subjectGradingCriteria));
            if (result > 0)
            {
                return Ok(result);
            }
            else { return BadRequest(); }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            if (result > 0)
                return Ok(result);
            return BadRequest("There are exams which depend on this grading criterion.");
        }

        #endregion Methods
    }

    public class SubjectGradingCriteriaRest
    {
        #region Properties

        public Guid Id { get; set; }

        public Guid SubjectId { get; set; }
        public string Name { get; set; }
        public float GradeA { get; set; }
        public float GradeB { get; set; }
        public float GradeC { get; set; }
        public float GradeD { get; set; }

        #endregion Properties
    }
}