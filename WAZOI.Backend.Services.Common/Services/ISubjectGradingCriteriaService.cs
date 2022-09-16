using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Services.Common
{
    public interface ISubjectGradingCriteriaService
    {
        #region Methods

        Task<int> DeleteAsync(Guid id);

        Task<int> InsertAsync(ISubjectGradingCriteriaDto dto);

        Task<int> UpdateAsync(ISubjectGradingCriteriaDto dto);

        Task<List<ISubjectGradingCriteriaDto>> GetAllBySubjectAsync(Guid subjectId);

        #endregion Methods
    }
}