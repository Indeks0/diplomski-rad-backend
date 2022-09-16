using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Services.Common
{
    public interface ISubjectStudentService
    {
        #region Methods

        Task<(List<ISubjectStudentDto> Students, int TotalCount)> FindSubjectStudentsAsync(Guid subjectId, int page, int rpp, string? searchQuery);

        Task<int> DeleteAsync(Guid id);

        Task<ISubjectStudentDto> GetSubjectStudentAsync(Guid subjectId, Guid userId);

        void SendEmailsToStudentsAsync((List<ISubjectStudentDto> Students, int TotalCount) students, IExamDto exam, string subjectName);

        #endregion Methods
    }
}