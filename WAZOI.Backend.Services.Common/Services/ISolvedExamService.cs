using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Services.Common
{
    public interface ISolvedExamService
    {
        #region Methods

        Task<int> CreateSolvedExamAsync(ISolvedExamDto solvedExam);

        Task<ISolvedExamDto> GetByIdAsync(Guid id);

        Task<int> UpdateAndGradeSolvedExamAsync(ISolvedExamDto solvedExam, Guid examId);

        Task<List<ISolvedExamDto>> GetStudentSolvedExamsAsync(Guid studentId);

        #endregion Methods
    }
}