using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOI.Backend.DAL.Entities
{
    [Table("SubjectNotices")]
    public class SubjectNotice
    {
        #region Properties

        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }

        public Guid TeacherId { get; set; }
        public virtual SubjectTeacher Teacher { get; set; }
        public Guid SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        #endregion Properties
    }
}