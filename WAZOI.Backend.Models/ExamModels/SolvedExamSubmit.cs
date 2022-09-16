using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Models
{
    public class SolvedExamSubmitDto : ISolvedExamSubmitDto
    {
        #region Properties

        public Guid StudentExamId { get; set; }
        public List<ISolvedExamAnswerDto> Answers { get; set; }
        public Guid ExamId { get; set; }

        #endregion Properties
    }
}