using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Repository.Common
{
    public interface IExamQuestionRepository : IGenericRepository<IExamQuestionDto, ExamQuestion>
    {
        #region Methods

        Task<List<IExamQuestionDto>> GetExamQuestionsAsync(Guid examId);

        #endregion Methods
    }
}