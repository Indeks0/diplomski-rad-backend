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
    public class SubjectStudentRepository : GenericRepository<ISubjectStudentDto, SubjectStudent>, ISubjectStudentRepository
    {
        #region Constructors

        public SubjectStudentRepository(AppDataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ISubjectStudentDto> FindBySubjectIdAsync(Guid subjectId)
        {
            var result = _context.SubjectStudent.Where(x => x.SubjectId == subjectId).ToList();
            if (result.Any())
            {
                return _mapper.Map<ISubjectStudentDto>(result.First());
            };
            return null;
        }

        public async Task<ISubjectStudentDto> GetSubjectStudentAsync(Guid subjectId, Guid userId)
        {
            var result = _context.SubjectStudent.Where(x => x.SubjectId == subjectId).Where(x => x.UserId == userId).ToList();
            if (result.Any())
            {
                return _mapper.Map<ISubjectStudentDto>(result.First());
            };
            return null;
        }

        public async Task<(List<ISubjectStudentDto> Students, int TotalCount)> FindSubjectStudentsAsync(Guid subjectId, int page, int rpp, string? searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                var result = base._context.SubjectStudent.Where(subjectStudent => subjectStudent.SubjectId == subjectId).Include(i => i.User).OrderBy(o => o.User.Surname).Skip((page - 1) * rpp).Take(rpp).ToList();
                var totalCount = base._context.SubjectStudent.Where(subjectStudent => subjectStudent.SubjectId == subjectId).Count();
                return (base._mapper.Map<List<ISubjectStudentDto>>(result), totalCount);
            }
            else
            {
                var result = base._context.SubjectStudent.Where(subjectStudent => subjectStudent.SubjectId == subjectId).Where(x => EF.Functions.ILike(x.User.Name, $"%{searchQuery}%") || EF.Functions.ILike(x.User.Surname, $"%{searchQuery}%")).Include(i => i.User).OrderBy(o => o.User.Surname).OrderBy(o => o.User.Surname).Skip((page - 1) * rpp).Take(rpp).ToList();
                var totalCount = base._context.SubjectStudent.Where(subjectStudent => subjectStudent.SubjectId == subjectId).Where(x => EF.Functions.ILike(x.User.Name, $"%{searchQuery}%") || EF.Functions.ILike(x.User.Surname, $"%{searchQuery}%")).Count();
                return (base._mapper.Map<List<ISubjectStudentDto>>(result), totalCount);
            }
        }

        #endregion Constructors
    }
}