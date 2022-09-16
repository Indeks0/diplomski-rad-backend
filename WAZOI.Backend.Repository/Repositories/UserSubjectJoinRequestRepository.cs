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
    public class UserSubjectJoinRequestRepository : GenericRepository<IUserSubjectJoinRequestDto, UserSubjectJoinRequest>, IUserSubjectJoinRequestRepository
    {
        #region Constructors

        public UserSubjectJoinRequestRepository(AppDataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<IUserSubjectJoinRequestDto> FindBySubjectIdAsync(Guid subjectId, Guid userId)
        {
            var result = _context.UserSubjectJoinRequest.Where(x => x.SubjectId == subjectId && x.UserId == userId).ToList();
            if (result.Any())
            {
                return _mapper.Map<IUserSubjectJoinRequestDto>(result.First());
            };
            return null;
        }

        public async Task<(List<IUserSubjectJoinRequestDto> Requests, int TotalCount)> FindJoinRequestsAsync(Guid subjectId, int page, int rpp)
        {
            var requests = base._context.UserSubjectJoinRequest.Include(i => i.User).Where(x => x.SubjectId == subjectId).OrderBy(y => y.User.Surname).Skip((page - 1) * rpp).Take(rpp).ToList();
            var totalCount = base._context.UserSubjectJoinRequest.Where(x => x.SubjectId == subjectId).Count();

            return (base._mapper.Map<List<IUserSubjectJoinRequestDto>>(requests), totalCount);
        }

        #endregion Constructors
    }
}