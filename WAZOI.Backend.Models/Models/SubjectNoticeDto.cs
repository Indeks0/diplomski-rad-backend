using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Models
{
    public class SubjectNoticeDto : ISubjectNoticeDto
    {
        #region Properties

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid TeacherId { get; set; }
        public ISubjectTeacherDto Teacher { get; set; }
        public Guid SubjectId { get; set; }
        public ISubjectDto Subject { get; set; }

        #endregion Properties
    }
}