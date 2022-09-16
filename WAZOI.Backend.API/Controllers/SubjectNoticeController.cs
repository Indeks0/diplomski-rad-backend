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
    public class SubjectNoticeController : ControllerBase
    {
        #region Fields

        protected readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ISubjectNoticeService _service;

        #endregion Fields

        #region Constructors

        public SubjectNoticeController(ISubjectNoticeService service, UserManager<User> userManager, IMapper mapper)
        {
            _service = service;
            _userManager = userManager;
            _mapper = mapper;
        }

        #endregion Constructors

        #region Methods

        [HttpPost]
        [Route("create-notice")]
        public async Task<IActionResult> PostSubjectNotice([FromBody] SubjectNoticeRest subjectNotice)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await _service.CreateSubjectNoticeAsync(_mapper.Map<ISubjectNoticeDto>(subjectNotice), userId);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("get-notices")]
        public async Task<IActionResult> GetSubjectNotices(Guid subjectId, int rpp = DefaultApplicationValues.DefaultRpp)
        {
            var result = await _service.GetSubjectNoticesAsync(subjectId, rpp);

            return Ok(
        new
        {
            items = result,
        });
        }

        #endregion Methods
    }

    public class SubjectNoticeRest
    {
        #region Properties

        public string Title { get; set; }
        public string Description { get; set; }
        public Guid SubjectId { get; set; }

        #endregion Properties
    }
}