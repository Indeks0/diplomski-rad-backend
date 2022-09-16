using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOI.Backend.Models.Common
{
    public interface IQuestionDto
    {
        #region Properties

        Guid Id { get; set; }
        Guid SubjectId { get; set; }
        string Content { get; set; }
        string Type { get; set; }

        ISubjectDto Subject { get; set; }

        #endregion Properties
    }
}