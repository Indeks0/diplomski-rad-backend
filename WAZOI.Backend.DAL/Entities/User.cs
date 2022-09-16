using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WAZOI.Backend.DAL.Entities
{
    public class User : IdentityUser<Guid>
    {
        #region Constructors

        public string Name { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string Surname { get; set; }

        #region Navigation

        public virtual List<SubjectStudent> SubjectStudents { get; set; }
        public virtual List<SubjectTeacher> SubjectTeachers { get; set; }

        #endregion Navigation

        #endregion Constructors
    }
}