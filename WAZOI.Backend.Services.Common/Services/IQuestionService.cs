using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Services.Common
{
    public interface IQuestionService
    {
        #region Methods

        Task<int> CreateQuestionsAsync(List<IQuestionDto> questionsDomain);

        Task<List<IQuestionDto>> FindQuestionsAsync(Guid subjectId, string type);

        Task<int> DeleteRangeAsync(List<IQuestionDto> questions);

        #endregion Methods
    }
}