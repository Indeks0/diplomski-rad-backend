using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOI.Backend.Models.Common
{
    public interface IExamDto
    {
        #region Properties

        Guid Id { get; set; }
        Guid SubjectGradingCriteriaId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        bool RandomizedQuestionsOrder { get; set; }
        bool IsLocked { get; set; }

        DateTime DateCreated { get; set; }
        DateTime DateOpenStart { get; set; }
        DateTime DateOpenEnd { get; set; }
        List<IExamQuestionDto> Questions { get; set; }
        List<IExamStudentDto> Students { get; set; }

        int DurationInMins { get; set; }
        Guid SubjectId { get; set; }
        ISubjectDto Subject { get; set; }

        ISubjectGradingCriteriaDto GradingCriterion { get; set; }

        #endregion Properties
    }
}