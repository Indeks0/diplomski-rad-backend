using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOI.Backend.DAL.Entities
{
    [Table("Exam")]
    public class Exam
    {
        #region Properties

        [Key]
        public Guid Id { get; set; }

        public Guid SubjectGradingCriteriaId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool RandomizedQuestionsOrder { get; set; }
        public bool IsLocked { get; set; }
        public int DurationInMins { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateOpenStart { get; set; }
        public DateTime DateOpenEnd { get; set; }

        public Guid SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual SubjectGradingCriteria GradingCriterion { get; set; }
        public virtual List<ExamQuestion> Questions { get; set; }
        public virtual List<ExamStudent> Students { get; set; }

        #endregion Properties
    }
}