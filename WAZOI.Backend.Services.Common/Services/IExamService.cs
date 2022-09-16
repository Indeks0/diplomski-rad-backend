using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Services.Common
{
    public interface IExamService
    {
        #region Methods

        Task<int> CreateExamAsync(IExamDto examDomain);

        Task<(List<IExamDto> OpenExams, List<IExamDto> FutureExams)> GetExamsForStudentAsync(Guid subjectId, Guid studentId);

        Task<IExamDto> GetExamForSolvingAsync(Guid id);

        Task<List<IExamDto>> GetExamsForLockingAsync();

        Task<bool> CalculateAndSaveSolvedExamAsync(ISolvedExamSubmitDto solvedExamSubmitDomain);

        Task<int> UpdateAsync(IExamDto exam);

        Task<List<IExamDto>> GetLockedExamsAsync(Guid subjectId);

        Task<List<IExamDto>> GetOpenExamsAsync(Guid subjectId);

        Task<List<IExamDto>> GetFutureExamsAsync(Guid subjectId);

        Task<bool> DeleteAsync(Guid id);

        Task<IExamDto> GetSolvedExamsAsync(Guid examId);

        Task<bool> CheckIfGradingCriteriaIsUsedAsync(Guid criteriaId);

        Task<IExamDto> GetExamWithCriteriaAsync(Guid examId);

        Task<int> LockSolvedExamsAsync(Guid examId);

        #endregion Methods
    }
}