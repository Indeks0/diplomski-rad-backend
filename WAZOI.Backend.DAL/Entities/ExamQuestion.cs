using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOI.Backend.DAL.Entities
{
    [Table("ExamQuestions")]
    public class ExamQuestion
    {
        #region Properties

        [Key]
        public Guid Id { get; set; }

        public Guid QuestionId { get; set; }
        public Guid ExamId { get; set; }
        public int Points { get; set; }
        public int OrdinalNumber { get; set; }
        public virtual Exam Exam { get; set; }
        public virtual Question Question { get; set; }

        #endregion Properties
    }
}