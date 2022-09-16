using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Models
{
    public class QuestionDto : IQuestionDto
    {
        #region Properties

        public Guid Id { get; set; }
        public Guid SubjectId { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public ISubjectDto Subject { get; set; }

        #endregion Properties
    }
}