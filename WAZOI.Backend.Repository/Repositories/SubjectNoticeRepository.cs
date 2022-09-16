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
    public class SubjectNoticeRepository : GenericRepository<ISubjectNoticeDto, SubjectNotice>, ISubjectNoticeRepository
    {
        #region Constructors

        public SubjectNoticeRepository(AppDataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        #endregion Constructors

        #region Methods

        public async Task<bool> CreateSubjectNoticeAsync(ISubjectNoticeDto subjectNotice, Guid userId)
        {
            var subjectTeacher = _context.SubjectTeacher.Where(x => x.SubjectId == subjectNotice.SubjectId && x.UserId == userId);
            if (!subjectTeacher.Any())
            {
                return false;
            }
            subjectNotice.TeacherId = subjectTeacher.FirstOrDefault().Id;

            await base._context.SubjectNotice.AddAsync(base._mapper.Map<SubjectNotice>(subjectNotice));
            var result = await base._context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<List<ISubjectNoticeDto>> GetSubjectNoticesAsync(Guid subjectId, int rpp)
        {
            var result = _context.SubjectNotice.Where(x => x.SubjectId == subjectId).OrderByDescending(x => x.DateCreated).Take(rpp).ToList();

            return _mapper.Map<List<ISubjectNoticeDto>>(result);
        }

        #endregion Methods
    }
}