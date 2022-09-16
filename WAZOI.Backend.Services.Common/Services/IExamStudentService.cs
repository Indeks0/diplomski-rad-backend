using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Services.Common
{
    public interface IExamStudentService
    {
        #region Methods

        Task<int> CreateExamStudentAsync(IExamStudentDto examStudentDto);

        Task<IExamStudentDto> GetStudentExamAsync(Guid examId, Guid studentId);

        Task<int> UpdateAsync(IExamStudentDto studentExam);

        #endregion Methods
    }
}