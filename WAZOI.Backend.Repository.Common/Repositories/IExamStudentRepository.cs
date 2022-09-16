using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Repository.Common
{
    public interface IExamStudentRepository : IGenericRepository<IExamStudentDto, ExamStudent>
    {
        #region Methods

        Task<IExamStudentDto> GetExamAccessAsync(Guid examId, Guid studentId);

        #endregion Methods
    }
}