using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOI.Backend.DAL.Entities
{
    [Table("Subjects")]
    public class Subject
    {
        #region Properties

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public string Color { get; set; }

        #endregion Properties

        #region Navigation

        public virtual List<SubjectStudent> SubjectStudents { get; set; }
        public virtual List<SubjectTeacher> SubjectTeachers { get; set; }

        public virtual List<Exam> Exams { get; set; }
        public virtual List<Question> Questions { get; set; }
        public virtual List<SubjectNotice> Notices { get; set; }
        public virtual List<UserSubjectJoinRequest> UserSubjectJoinRequests { get; set; }
        public virtual List<SubjectGradingCriteria> SubjectGradingCriteria { get; set; }

        #endregion Navigation
    }
}