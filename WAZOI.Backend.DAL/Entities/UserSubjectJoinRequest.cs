using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOI.Backend.DAL.Entities
{
    [Table("UserSubjectJoinRequests")]
    public class UserSubjectJoinRequest
    {
        #region Properties

        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public Guid SubjectId { get; set; }
        public virtual User User { get; set; }
        public virtual Subject Subject { get; set; }

        #endregion Properties
    }
}