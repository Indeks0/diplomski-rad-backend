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
    public class SolvedExamService : ISolvedExamService
    {
        #region Fields

        private IUnitOfWork _uow;
        private ISolvedExamRepository _repository;
        private IExamRepository _examRepository;

        #endregion Fields

        #region Constructors

        public SolvedExamService(IUnitOfWork uow, IExamRepository examRepository)
        {
            this._uow = uow;
            _repository = uow.SolvedExamRepository;
            _examRepository = examRepository;
        }

        #endregion Constructors

        #region Methods

        public async Task<int> CreateSolvedExamAsync(ISolvedExamDto solvedExam)
        {
            _repository.Insert(solvedExam);
            return await _uow.SaveAsync();
        }

        public async Task<ISolvedExamDto> GetByIdAsync(Guid id)
        {
            var solvedExam = await _repository.GetByIdAsync(id);
            return solvedExam;
        }

        public async Task<int> UpdateAndGradeSolvedExamAsync(ISolvedExamDto solvedExam, Guid examId)
        {
            var exam = await _examRepository.GetExamWithCriteriaAsync(examId);
            var solvedExamFromDb = await _repository.GetByIdAsync(solvedExam.Id);

            solvedExamFromDb.Content = solvedExam.Content;
            solvedExamFromDb.ScoredPoints = (float)Math.Round(solvedExam.ScoredPoints, 2);
            solvedExamFromDb.ScoredPercentage = (float)Math.Round(solvedExamFromDb.ScoredPoints / solvedExamFromDb.MaximumPoints * 100, 2);

            if (solvedExamFromDb.ScoredPercentage >= exam.GradingCriterion.GradeA)
            {
                solvedExamFromDb.Grade = 5;
            }
            else if (solvedExamFromDb.ScoredPercentage >= exam.GradingCriterion.GradeB)
            {
                solvedExamFromDb.Grade = 4;
            }
            else if (solvedExamFromDb.ScoredPercentage >= exam.GradingCriterion.GradeC)
            {
                solvedExamFromDb.Grade = 3;
            }
            else if (solvedExamFromDb.ScoredPercentage >= exam.GradingCriterion.GradeD)
            {
                solvedExamFromDb.Grade = 2;
            }
            else
            {
                solvedExamFromDb.Grade = 1;
            }

            _repository.PutAsync(solvedExamFromDb);

            var result = await _uow.SaveAsync();
            return result;
        }

        public Task<List<ISolvedExamDto>> GetStudentSolvedExamsAsync(Guid studentId)
        {
            var solvedExams = _repository.GetStudentSolvedExamsAsync(studentId);
            return solvedExams;
        }

        #endregion Methods
    }
}