﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Models
{
    public class SubjectTeacherDto : ISubjectTeacherDto
    {
        #region Properties

        public Guid id { get; set; }
        public ISubjectDto Subject { get; set; }
        public Guid SubjectId { get; set; }
        public IUserDto User { get; set; }
        public Guid UserId { get; set; }
        public bool IsSubjectAdmin { get; set; }

        #endregion Properties
    }
}