using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Services.Common
{
    public interface IExamQuestionService
    {
        #region Methods

        Task<List<IExamQuestionDto>> GetExamQuestionsAsync(Guid examId);

        #endregion Methods
    }
}