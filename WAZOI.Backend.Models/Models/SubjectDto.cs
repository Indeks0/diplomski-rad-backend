using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Models
{
    public class SubjectDto : ISubjectDto
    {
        #region Properties

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ISubjectStudentDto> SubjectStudents { get; set; }
        public List<ISubjectTeacherDto> SubjectTeachers { get; set; }
        public string Color { get; set; }
        public string EnrollmentStatus { get; set; }

        #endregion Properties
    }
}