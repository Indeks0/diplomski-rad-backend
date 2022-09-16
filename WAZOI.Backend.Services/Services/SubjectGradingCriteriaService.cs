using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models;
using WAZOI.Backend.Models.Common;
using WAZOI.Backend.Repository.Common;
using WAZOI.Backend.Services.Common;

namespace WAZOI.Backend.Services
{
    public class SubjectGradingCriteriaService : ISubjectGradingCriteriaService
    {
        #region Fields

        private IUnitOfWork _uow;
        private ISubjectGradingCriteriaRepository _repository;
        private IExamService _examService;

        #endregion Fields

        #region Constructors

        public SubjectGradingCriteriaService(IUnitOfWork uow, IExamService examService)
        {
            this._uow = uow;
            _repository = uow.SubjectGradingCriteriaRepository;
            _examService = examService;
        }

        #endregion Constructors

        #region Methods

        public async Task<int> DeleteAsync(Guid id)
        {
            var isUsedInExam = await _examService.CheckIfGradingCriteriaIsUsedAsync(id);
            if (isUsedInExam)
            {
                return 0;
            }
            var dto = new SubjectGradingCriteriaDto();
            dto.Id = id;
            _repository.Delete(dto);
            return await _uow.SaveAsync();
        }

        public async Task<int> InsertAsync(ISubjectGradingCriteriaDto dto)
        {
            _repository.Insert(dto);
            return await _uow.SaveAsync();
        }

        public async Task<int> UpdateAsync(ISubjectGradingCriteriaDto dto)
        {
            _repository.PutAsync(dto);
            return await _uow.SaveAsync();
        }

        public async Task<List<ISubjectGradingCriteriaDto>> GetAllBySubjectAsync(Guid subjectId)
        {
            var criteria = await _repository.GetAllBySubjectAsync(subjectId);
            return criteria;
        }

        #endregion Methods
    }
}