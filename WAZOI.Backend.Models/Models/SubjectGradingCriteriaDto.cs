using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Models
{
    public class SubjectGradingCriteriaDto : ISubjectGradingCriteriaDto
    {
        #region Properties

        public Guid Id { get; set; }
        public Guid SubjectId { get; set; }
        public string Name { get; set; }
        public float GradeA { get; set; }
        public float GradeB { get; set; }
        public float GradeC { get; set; }
        public float GradeD { get; set; }

        #endregion Properties
    }
}