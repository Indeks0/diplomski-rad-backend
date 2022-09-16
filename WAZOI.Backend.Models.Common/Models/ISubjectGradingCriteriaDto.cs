using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOI.Backend.Models.Common
{
    public interface ISubjectGradingCriteriaDto
    {
        #region Properties

        Guid Id { get; set; }

        Guid SubjectId { get; set; }

        string Name { get; set; }

        float GradeA { get; set; }

        float GradeB { get; set; }

        float GradeC { get; set; }

        float GradeD { get; set; }

        #endregion Properties
    }
}