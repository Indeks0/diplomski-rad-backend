using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Models
{
    public class SolvedExamDto : ISolvedExamDto
    {
        #region Properties

        public Guid Id { get; set; }
        public Guid ExamStudentId { get; set; }
        public string Content { get; set; }
        public float MaximumPoints { get; set; }
        public float ScoredPoints { get; set; }
        public float ScoredPercentage { get; set; }
        public int Grade { get; set; }
        public bool isLocked { get; set; }
        public IExamStudentDto ExamStudent { get; set; }

        #endregion Properties
    }
}