using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOI.Backend.Models.Common
{
    public interface ISubjectNoticeDto
    {
        #region Properties

        Guid Id { get; set; }

        string Title { get; set; }
        string Description { get; set; }
        DateTime DateCreated { get; set; }

        Guid TeacherId { get; set; }
        ISubjectTeacherDto Teacher { get; set; }
        Guid SubjectId { get; set; }
        ISubjectDto Subject { get; set; }

        #endregion Properties
    }
}