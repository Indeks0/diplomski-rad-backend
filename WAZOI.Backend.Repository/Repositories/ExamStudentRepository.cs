using AutoMapper;
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
    public class ExamStudentRepository : GenericRepository<IExamStudentDto, ExamStudent>, IExamStudentRepository
    {
        #region Constructors

        public ExamStudentRepository(AppDataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        #endregion Constructors

        #region Methods

        public async Task<IExamStudentDto> GetExamAccessAsync(Guid examId, Guid studentId)
        {
            var result = _context.ExamStudent.Where(x => x.ExamId == examId).Where(x => x.StudentId == studentId).ToList();
            return _mapper.Map<IExamStudentDto>(result.FirstOrDefault());
        }

        #endregion Methods
    }
}