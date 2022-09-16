using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Repository.Common
{
    public interface ISubjectStudentRepository : IGenericRepository<ISubjectStudentDto, SubjectStudent>
    {
        #region Methods

        Task<ISubjectStudentDto> FindBySubjectIdAsync(Guid subjectId);

        Task<ISubjectStudentDto> GetSubjectStudentAsync(Guid subjectId, Guid userId);

        Task<(List<ISubjectStudentDto> Students, int TotalCount)> FindSubjectStudentsAsync(Guid subjectId, int page, int rpp, string? searchQuery);

        #endregion Methods
    }
}