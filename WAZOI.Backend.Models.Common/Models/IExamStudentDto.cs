using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOI.Backend.Models.Common
{
    public interface IExamStudentDto
    {
        #region Properties

        public Guid Id { get; set; }

        Guid StudentId { get; set; }
        Guid ExamId { get; set; }
        ISubjectStudentDto Student { get; set; }
        IExamDto Exam { get; set; }
        ISolvedExamDto SolvedExam { get; set; }
        bool HasExamAccess { get; set; }

        #endregion Properties
    }
}