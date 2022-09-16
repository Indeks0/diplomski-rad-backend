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
    public class SolvedExamRepository
        : GenericRepository<ISolvedExamDto, SolvedExam>, ISolvedExamRepository
    {
        #region Constructors

        public SolvedExamRepository(AppDataContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        #endregion Constructors

        #region Methods

        public async Task<List<ISolvedExamDto>> GetStudentSolvedExamsAsync(Guid studentId)
        {
            var exams = _context.SolvedExam.Where(x => x.ExamStudent.StudentId == studentId).Include(x => x.ExamStudent).ThenInclude(e => e.Exam).ToList();
            return _mapper.Map<List<ISolvedExamDto>>(exams);
        }

        #endregion Methods
    }
}