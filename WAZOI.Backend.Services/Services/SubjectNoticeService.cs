using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;
using WAZOI.Backend.Repository.Common;
using WAZOI.Backend.Services.Common;

namespace WAZOI.Backend.Services
{
    public class SubjectNoticeService : ISubjectNoticeService
    {
        #region Fields

        private IUnitOfWork _uow;
        private ISubjectNoticeRepository _repository;

        #endregion Fields

        #region Constructors

        public SubjectNoticeService(IUnitOfWork uow)
        {
            this._uow = uow;
            _repository = uow.SubjectNoticeRepository;
        }

        #endregion Constructors

        #region Methods

        public async Task<List<ISubjectNoticeDto>> GetSubjectNoticesAsync(Guid subjectId, int rpp)
        {
            return await _repository.GetSubjectNoticesAsync(subjectId, rpp);
        }

        public async Task<bool> CreateSubjectNoticeAsync(ISubjectNoticeDto subjectNotice, Guid userId)
        {
            return await _repository.CreateSubjectNoticeAsync(subjectNotice, userId);
        }

        #endregion Methods
    }
}