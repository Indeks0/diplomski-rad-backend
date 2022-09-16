using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;
using WAZOI.Backend.Repository.Common;
using WAZOI.Backend.Services.Common;

namespace WAZOI.Backend.Services
{
    public class ExamQuestionService : IExamQuestionService
    {
        #region Fields

        private IUnitOfWork _uow;
        private IExamQuestionRepository _repository;

        #endregion Fields

        #region Constructors

        public ExamQuestionService(IUnitOfWork uow)
        {
            this._uow = uow;
            _repository = uow.ExamQuestionRepository;
        }

        #endregion Constructors

        #region Methods

        public async Task<List<IExamQuestionDto>> GetExamQuestionsAsync(Guid examId)
        {
            var result = await _repository.GetExamQuestionsAsync(examId);
            return result;
        }

        #endregion Methods
    }
}