using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Models
{
    public class UserSubjectJoinRequestDto : IUserSubjectJoinRequestDto
    {
        #region Properties

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid SubjectId { get; set; }
        public IUserDto User { get; set; }
        public ISubjectDto Subject { get; set; }

        #endregion Properties
    }
}