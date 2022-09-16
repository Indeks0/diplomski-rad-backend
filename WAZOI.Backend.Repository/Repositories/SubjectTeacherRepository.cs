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
    public class SubjectTeacherRepository : GenericRepository<ISubjectTeacherDto, SubjectTeacher>, ISubjectTeacherRepository
    {
        #region Constructors

        public SubjectTeacherRepository(AppDataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ISubjectTeacherDto> FindSubjectTeacherAsync(Guid userId, Guid subjectId)
        {
            var result = _context.SubjectTeacher.Where(x => x.SubjectId == subjectId).Where(x => x.UserId == userId).ToList();
            if (result.Any())
            {
                return _mapper.Map<ISubjectTeacherDto>(result.First());
            };
            return null;
        }

        public async Task<List<ISubjectTeacherDto>> FindSubjectTeachersAsync(Guid subjectId)
        {
            var result = _context.SubjectTeacher.Where(x => x.SubjectId == subjectId).Include(x => x.User).ToList();
            if (result.Any())
            {
                return _mapper.Map<List<ISubjectTeacherDto>>(result);
            };
            return null;
        }

        public async Task<bool> GetIsTeacherAdminAsync(Guid userId, Guid subjectId)
        {
            var result = _context.SubjectTeacher.Any(x => x.UserId == userId && x.SubjectId == subjectId && x.IsSubjectAdmin == true);
            return result;
        }

        public async Task<bool> IsTeacher(Guid userId, Guid subjectId)
        {
            var result = _context.SubjectTeacher.Any(x => x.UserId == userId && x.SubjectId == subjectId);
            return result;
        }

        #endregion Constructors
    }
}