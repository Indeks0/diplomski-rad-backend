using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Models
{
    public class SolvedExamAnswerDto : ISolvedExamAnswerDto
    {
        #region Properties

        public Guid Id { get; set; }
        public string Answer { get; set; }

        #endregion Properties
    }
}