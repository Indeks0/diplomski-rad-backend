using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOI.Backend.DAL.Entities
{
    [Table("Questions")]
    public class Question
    {
        #region Properties

        [Key]
        public Guid Id { get; set; }

        public Guid SubjectId { get; set; }

        [Column(TypeName = "jsonb")]
        public string Content { get; set; }

        public string Type { get; set; }

        public virtual Subject Subject { get; set; }

        #endregion Properties
    }
}