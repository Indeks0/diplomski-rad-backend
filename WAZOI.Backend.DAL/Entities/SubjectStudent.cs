using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOI.Backend.DAL.Entities
{
    [Table("SubjectStudents")]
    public class SubjectStudent
    {
        #region Properties

        [Key]
        public Guid Id { get; set; }

        #endregion Properties

        #region Navigation

        public virtual Subject Subject { get; set; }

        public Guid SubjectId { get; set; }

        public virtual User User { get; set; }
        public Guid UserId { get; set; }

        #endregion Navigation
    }
}