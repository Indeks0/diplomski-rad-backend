namespace WAZOI.Backend.Models.Common
{
    public interface IUserDto
    {
        #region Properties

        string Name { get; set; }
        public string Email { get; set; }
        string Surname { get; set; }

        List<ISubjectStudentDto> SubjectStudents { get; set; }
        List<ISubjectTeacherDto> SubjectTeachers { get; set; }

        #endregion Properties
    }
}