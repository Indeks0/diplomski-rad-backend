using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Models
{
    public class ExamQuestionDto : IExamQuestionDto
    {
        #region Properties

        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public int Points { get; set; }
        public int OrdinalNumber { get; set; }
        public Guid ExamId { get; set; }
        public IExamDto Exam { get; set; }
        public IQuestionDto Question { get; set; }

        #endregion Properties
    }
}