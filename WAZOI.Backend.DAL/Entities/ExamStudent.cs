using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOI.Backend.DAL.Entities
{
    [Table("ExamStudents")]
    public class ExamStudent
    {
        #region Properties

        [Key]
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }
        public Guid ExamId { get; set; }
        public bool HasExamAccess { get; set; }
        public virtual SubjectStudent Student { get; set; }
        public virtual Exam Exam { get; set; }
        public virtual SolvedExam SolvedExam { get; set; }

        #endregion Properties
    }
}