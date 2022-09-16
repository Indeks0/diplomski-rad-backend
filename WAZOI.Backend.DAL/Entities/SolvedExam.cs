using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOI.Backend.DAL.Entities
{
    [Table("SolvedExams")]
    public class SolvedExam
    {
        #region Properties

        [Key]
        public Guid Id { get; set; }

        public Guid ExamStudentId { get; set; }

        [Column(TypeName = "jsonb")]
        public string Content { get; set; }

        public float MaximumPoints { get; set; }
        public float ScoredPoints { get; set; }
        public float ScoredPercentage { get; set; }
        public int Grade { get; set; }
        public bool isLocked { get; set; }

        public virtual ExamStudent ExamStudent { get; set; }

        #endregion Properties
    }
}