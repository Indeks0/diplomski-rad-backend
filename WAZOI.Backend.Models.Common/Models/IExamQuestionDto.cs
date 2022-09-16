using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOI.Backend.Models.Common
{
    public interface IExamQuestionDto
    {
        #region Properties

        Guid Id { get; set; }

        Guid QuestionId { get; set; }
        Guid ExamId { get; set; }
        int Points { get; set; }
        int OrdinalNumber { get; set; }
        IExamDto Exam { get; set; }
        IQuestionDto Question { get; set; }

        #endregion Properties
    }
}