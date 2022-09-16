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
    public class ExamStudentController : ControllerBase
    {
        #region Fields

        protected readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IExamStudentService _service;

        #endregion Fields

        #region Constructors

        public ExamStudentController(IExamStudentService service,
           UserManager<User> userManager, IMapper mapper)
        {
            _service = service;
            _userManager = userManager;
            _mapper = mapper;
        }

        #endregion Constructors

        #region Methods

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(ExamStudentRest examStudent)
        {
            var result = await _service.CreateExamStudentAsync(_mapper.Map<IExamStudentDto>(examStudent));

            if (result > 0)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        #endregion Methods
    }

    public partial class ExamStudentRest
    {
        #region Properties

        public Guid StudentId { get; set; }
        public Guid ExamId { get; set; }

        #endregion Properties
    }
}