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
    public class ExamQuestionRepository : GenericRepository<IExamQuestionDto, ExamQuestion>, IExamQuestionRepository
    {
        #region Constructors

        public ExamQuestionRepository(AppDataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<IExamQuestionDto>> GetExamQuestionsAsync(Guid examId)
        {
            var result = _context.ExamQuestion.Where(x => x.ExamId == examId).Include(x => x.Question).ToList();
            return _mapper.Map<List<IExamQuestionDto>>(result);
        }

        #endregion Constructors
    }
}