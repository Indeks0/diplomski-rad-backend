using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOI.Backend.Models.Common
{
    public interface ISubjectTeacherDto
    {
        #region Properties

        Guid id { get; set; }

        ISubjectDto Subject { get; set; }

        Guid SubjectId { get; set; }

        IUserDto User { get; set; }

        Guid UserId { get; set; }

        bool IsSubjectAdmin { get; set; }

        #endregion Properties
    }
}