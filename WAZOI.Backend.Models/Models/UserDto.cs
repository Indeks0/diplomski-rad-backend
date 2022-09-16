using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Models
{
    internal class UserDto : IUserDto
    {
        #region Properties

        public string Name { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
        public List<ISubjectStudentDto> SubjectStudents { get; set; }
        public List<ISubjectTeacherDto> SubjectTeachers { get; set; }

        #endregion Properties
    }
}