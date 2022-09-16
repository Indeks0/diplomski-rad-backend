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
    public class QuestionService : IQuestionService
    {
        #region Fields

        private IUnitOfWork _uow;
        private IQuestionRepository _repository;

        #endregion Fields

        #region Constructors

        public QuestionService(IUnitOfWork uow)
        {
            this._uow = uow;
            _repository = uow.QuestionRepository;
        }

        #endregion Constructors

        #region Methods

        public async Task<int> CreateQuestionsAsync(List<IQuestionDto> questionsDomain)
        {
            _repository.InsertRange(questionsDomain);

            return await _uow.SaveAsync();
        }

        public async Task<List<IQuestionDto>> FindQuestionsAsync(Guid subjectId, string type)
        {
            var result = await _repository.FindQuestionsAsync(subjectId, type);
            return result;
        }

        public async Task<int> DeleteRangeAsync(List<IQuestionDto> questions)
        {
            _repository.DeleteRange(questions);
            var result = await _uow.SaveAsync();
            return result;
        }

        #endregion Methods
    }
}