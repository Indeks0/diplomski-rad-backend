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
    public class QuestionRepository : GenericRepository<IQuestionDto, Question>, IQuestionRepository
    {
        #region Constructors

        public QuestionRepository(AppDataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<IQuestionDto>> FindQuestionsAsync(Guid subjectId, string type)
        {
            if (!string.IsNullOrEmpty(type))
            {
                var result = base._context.Question.Where(x => x.SubjectId == subjectId).Where(y => EF.Functions.ILike(y.Type, $"%{type}%")).ToList();
                return base._mapper.Map<List<IQuestionDto>>(result);
            }
            else
            {
                var result = base._context.Question.Where(x => x.SubjectId == subjectId).ToList();
                return base._mapper.Map<List<IQuestionDto>>(result);
            }
        }

        #endregion Constructors
    }
}