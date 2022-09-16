using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOI.Backend.Models.Common
{
    public interface ISubjectDto
    {
        #region Properties

        Guid Id { get; set; }

        string Name { get; set; }

        string Description { get; set; }
        string Color { get; set; }
        string EnrollmentStatus { get; set; }
        List<ISubjectStudentDto> SubjectStudents { get; set; }

        List<ISubjectTeacherDto> SubjectTeachers { get; set; }

        #endregion Properties
    }
}