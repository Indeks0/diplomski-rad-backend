using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOI.Backend.Models.Common
{
    public interface ISolvedExamSubmitDto
    {
        #region Properties

        Guid ExamId { get; set; }
        Guid StudentExamId { get; set; }
        List<ISolvedExamAnswerDto> Answers { get; set; }

        #endregion Properties
    }
}