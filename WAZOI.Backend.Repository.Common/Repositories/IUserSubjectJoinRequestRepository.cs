using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Repository.Common
{
    public interface IUserSubjectJoinRequestRepository : IGenericRepository<IUserSubjectJoinRequestDto, UserSubjectJoinRequest>
    {
        #region Methods

        Task<(List<IUserSubjectJoinRequestDto> Requests, int TotalCount)> FindJoinRequestsAsync(Guid subjectId, int page, int rpp);

        Task<IUserSubjectJoinRequestDto> FindBySubjectIdAsync(Guid subjectId, Guid userId);

        #endregion Methods
    }
}