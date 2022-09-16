using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Repository.Common
{
    public interface IExamRepository : IGenericRepository<IExamDto, Exam>
    {
        #region Methods

        Task<(List<IExamDto> OpenExams, List<IExamDto> FutureExams)> GetExamsForStudentAsync(Guid subjectId, Guid studentId);

        Task<IExamDto> GetExamForSolvingAsync(Guid id);

        Task<List<IExamDto>> GetExamsForLockingAsync();

        Task<List<IExamDto>> GetLockedExamsAsync(Guid subjectId);

        Task<List<IExamDto>> GetOpenExamsAsync(Guid subjectId);

        Task<List<IExamDto>> GetFutureExamsAsync(Guid subjectId);

        Task<IExamDto> GetSolvedExamsAsync(Guid examId);

        Task<bool> CheckIfGradingCriteriaIsUsedAsync(Guid criteriaId);

        Task<IExamDto> GetExamWithCriteriaAsync(Guid examId);

        void LockSolvedExamsAsync(Guid examId);

        #endregion Methods
    }
}