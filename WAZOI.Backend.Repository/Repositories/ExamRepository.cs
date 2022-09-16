using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.DAL;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Models.Common;
using WAZOI.Backend.Repository.Common;

namespace WAZOI.Backend.Repository
{
    public class ExamRepository : GenericRepository<IExamDto, Exam>, IExamRepository
    {
        #region Constructors

        public ExamRepository(AppDataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        #endregion Constructors

        #region Methods

        public async Task<IExamDto> GetExamForSolvingAsync(Guid id)
        {
            var exam = base._context.Exam.Where(x => x.Id == id).Include(x => x.Questions).ThenInclude(x => x.Question).ToList();
            return _mapper.Map<IExamDto>(exam.First());
        }

        public async Task<List<IExamDto>> GetExamsForLockingAsync()
        {
            var exams = base._context.Exam.Where(e => e.DateOpenEnd < DateTime.Now.ToUniversalTime()).Where(e => e.IsLocked == false).ToList();

            return _mapper.Map<List<IExamDto>>(exams);
        }

        public async Task<(List<IExamDto> OpenExams, List<IExamDto> FutureExams)> GetExamsForStudentAsync(Guid subjectId, Guid studentId)
        {
            var exams = base._context.Exam.Where(e => e.SubjectId == subjectId && e.IsLocked == false).Where(e => e.DateOpenEnd > DateTime.Now.ToUniversalTime()).Where(x => x.Students.Any(s => s.StudentId == studentId && s.SolvedExam == null)).ToList();

            var openExams = exams.Where(e => e.DateOpenStart < DateTime.Now.ToUniversalTime() && e.DateOpenEnd > DateTime.Now.ToUniversalTime()).ToList();

            exams.RemoveAll(item => openExams.Any(o => o.Id == item.Id));

            return (_mapper.Map<List<IExamDto>>(openExams), _mapper.Map<List<IExamDto>>(exams));
        }

        public async Task<List<IExamDto>> GetFutureExamsAsync(Guid subjectId)
        {
            var exams = _context.Exam.Where(x => x.SubjectId == subjectId && !x.IsLocked).Where(e => e.DateOpenStart > DateTime.Now.ToUniversalTime()).ToList();
            return _mapper.Map<List<IExamDto>>(exams);
        }

        public async Task<List<IExamDto>> GetLockedExamsAsync(Guid subjectId)
        {
            var exams = _context.Exam.Where(x => x.SubjectId == subjectId && x.IsLocked == true).ToList();
            return _mapper.Map<List<IExamDto>>(exams);
        }

        public async Task<List<IExamDto>> GetOpenExamsAsync(Guid subjectId)
        {
            var exams = _context.Exam.Where(e => e.SubjectId == subjectId && !e.IsLocked).Where(e => e.DateOpenStart < DateTime.Now.ToUniversalTime() && e.DateOpenEnd > DateTime.Now.ToUniversalTime()).ToList();
            return _mapper.Map<List<IExamDto>>(exams);
        }

        public async Task<IExamDto> GetSolvedExamsAsync(Guid examId)
        {
            var exam = _context.Exam.Where(x => x.Id == examId).Include(x => x.Students.Where(s => s.SolvedExam != null)).ThenInclude(s => s.SolvedExam).Include(x => x.Students).ThenInclude(s => s.Student).ThenInclude(s => s.User).ToList().FirstOrDefault();
            return _mapper.Map<IExamDto>(exam);
        }

        public async Task<bool> CheckIfGradingCriteriaIsUsedAsync(Guid criteriaId)
        {
            return _context.Exam.Any(x => x.SubjectGradingCriteriaId == criteriaId);
        }

        public async Task<IExamDto> GetExamWithCriteriaAsync(Guid examId)
        {
            var exam = _context.Exam.Where(x => x.Id == examId).Include(x => x.GradingCriterion).FirstOrDefault();
            return _mapper.Map<IExamDto>(exam);
        }

        public void LockSolvedExamsAsync(Guid examId)
        {
            var exam = _context.Exam.Where(x => x.Id == examId).Include(x => x.Students).ThenInclude(s => s.SolvedExam).FirstOrDefault();

            foreach (var examStudent in exam.Students)
            {
                examStudent.SolvedExam.isLocked = true;
            }

            _context.Update(exam);
        }

        #endregion Methods
    }
}