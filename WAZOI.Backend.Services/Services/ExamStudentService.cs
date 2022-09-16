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
    public class ExamStudentService : IExamStudentService
    {
        #region Fields

        private IUnitOfWork _uow;
        private IExamStudentRepository _repository;

        #endregion Fields

        #region Constructors

        public ExamStudentService(IUnitOfWork uow)
        {
            this._uow = uow;
            _repository = uow.ExamStudentRepository;
        }

        #endregion Constructors

        #region Methods

        public async Task<int> CreateExamStudentAsync(IExamStudentDto examStudentDto)
        {
            _repository.Insert(examStudentDto);
            return await _uow.SaveAsync();
        }

        public async Task<IExamStudentDto> GetStudentExamAsync(Guid examId, Guid studentId)
        {
            var result = await _repository.GetExamAccessAsync(examId, studentId);
            return result;
        }

        public async Task<int> UpdateAsync(IExamStudentDto examStudent)
        {
            _repository.PutAsync(examStudent);
            return await _uow.SaveAsync();
        }

        #endregion Methods
    }
}