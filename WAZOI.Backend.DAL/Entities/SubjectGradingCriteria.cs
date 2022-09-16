using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOI.Backend.DAL.Entities
{
    [Table("SubjectGradingCriteria")]
    public class SubjectGradingCriteria
    {
        #region Properties

        [Key]
        public Guid Id { get; set; }

        public Guid SubjectId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float GradeA { get; set; }

        [Required]
        public float GradeB { get; set; }

        [Required]
        public float GradeC { get; set; }

        [Required]
        public float GradeD { get; set; }

        #endregion Properties
    }
}