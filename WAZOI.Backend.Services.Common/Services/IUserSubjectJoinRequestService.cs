using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Services.Common
{
    public interface IUserSubjectJoinRequestService
    {
        #region Methods

        Task<(List<IUserSubjectJoinRequestDto> Requests, int TotalCount)> FindJoinRequestsAsync(Guid subjectId, int page, int rpp);

        Task<bool> AcceptJoinRequestAsync(IUserSubjectJoinRequestDto joinRequest);

        Task<bool> DeclineJoinRequestAsync(IUserSubjectJoinRequestDto joinRequest);

        Task<bool> CreateJoinRequestAsync(IUserSubjectJoinRequestDto joinRequest);

        #endregion Methods
    }
}