using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOI.Backend.Models.Common
{
    public interface ISolvedExamAnswerDto
    {
        #region Properties

        Guid Id { get; set; }
        string Answer { get; set; }

        #endregion Properties
    }
}