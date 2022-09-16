using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Models
{
    public class ExamDto : IExamDto
    {
        #region Properties

        public Guid Id { get; set; }
        public Guid SubjectGradingCriteriaId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool RandomizedQuestionsOrder { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateOpenStart { get; set; }
        public DateTime DateOpenEnd { get; set; }
        public Guid SubjectId { get; set; }
        public ISubjectDto Subject { get; set; }
        public List<IExamQuestionDto> Questions { get; set; }
        public List<IExamStudentDto> Students { get; set; }

        public int DurationInMins { get; set; }
        public bool IsLocked { get; set; }
        public ISubjectGradingCriteriaDto GradingCriterion { get; set; }

        #endregion Properties
    }
}