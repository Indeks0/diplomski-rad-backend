using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Repository.Common
{
    public interface IQuestionRepository : IGenericRepository<IQuestionDto, Question>
    {
        #region Methods

        Task<List<IQuestionDto>> FindQuestionsAsync(Guid subjectId, string type);

        #endregion Methods
    }
}