using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Models
{
    public class ExamStudentDto : IExamStudentDto
    {
        #region Properties

        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid ExamId { get; set; }
        public ISubjectStudentDto Student { get; set; }
        public IExamDto Exam { get; set; }
        public ISolvedExamDto SolvedExam { get; set; }
        public bool HasExamAccess { get; set; }

        #endregion Properties
    }
}