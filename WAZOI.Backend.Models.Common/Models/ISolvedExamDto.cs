using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOI.Backend.Models.Common
{
    public interface ISolvedExamDto
    {
        #region Properties

        Guid Id { get; set; }
        Guid ExamStudentId { get; set; }
        string Content { get; set; }
        float MaximumPoints { get; set; }
        float ScoredPoints { get; set; }
        float ScoredPercentage { get; set; }
        bool isLocked { get; set; }
        int Grade { get; set; }
        IExamStudentDto ExamStudent { get; set; }

        #endregion Properties
    }
}