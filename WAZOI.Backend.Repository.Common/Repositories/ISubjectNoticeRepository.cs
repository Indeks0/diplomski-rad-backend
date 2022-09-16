using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Repository.Common
{
    public interface ISubjectNoticeRepository : IGenericRepository<ISubjectNoticeDto, SubjectNotice>
    {
        #region Methods

        Task<bool> CreateSubjectNoticeAsync(ISubjectNoticeDto subjectNotice, Guid userId);

        Task<List<ISubjectNoticeDto>> GetSubjectNoticesAsync(Guid subjectId, int rpp);

        #endregion Methods
    }
}