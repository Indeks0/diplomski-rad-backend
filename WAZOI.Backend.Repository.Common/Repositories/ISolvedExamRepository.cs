using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Repository.Common
{
    public interface ISolvedExamRepository : IGenericRepository<ISolvedExamDto, SolvedExam>
    {
        #region Methods

        Task<List<ISolvedExamDto>> GetStudentSolvedExamsAsync(Guid studentId);

        #endregion Methods
    }
}