using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOI.Backend.Models.Common
{
    public interface IUserSubjectJoinRequestDto
    {
        #region Properties

        Guid Id { get; set; }
        Guid UserId { get; set; }
        Guid SubjectId { get; set; }
        IUserDto User { get; set; }
        ISubjectDto Subject { get; set; }

        #endregion Properties
    }
}