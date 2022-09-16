using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Repository.Common
{
    public interface ISubjectGradingCriteriaRepository : IGenericRepository<ISubjectGradingCriteriaDto, SubjectGradingCriteria>
    {
        #region Methods

        Task<List<ISubjectGradingCriteriaDto>> GetAllBySubjectAsync(Guid subjectId);

        #endregion Methods
    }
}