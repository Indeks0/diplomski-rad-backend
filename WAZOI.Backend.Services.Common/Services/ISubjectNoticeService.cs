using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Services.Common
{
    public interface ISubjectNoticeService
    {
        #region Methods

        Task<bool> CreateSubjectNoticeAsync(ISubjectNoticeDto subjectNotice, Guid userId);

        Task<List<ISubjectNoticeDto>> GetSubjectNoticesAsync(Guid subjectId, int rpp);

        #endregion Methods
    }
}