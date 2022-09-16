using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Repository.Common
{
    public interface ISubjectRepository : IGenericRepository<ISubjectDto, Subject>
    {
        #region Methods

        Task<(List<ISubjectDto> Subjects, int TotalCount)> FindSubjectsAsync(int page, int rpp, string? searchQuery);

        void CreateSubject(ISubjectTeacherDto teacherWithSubject);

        Task<(List<ISubjectDto> Subjects, int TotalCount)> FindSubjectsForUserAsync(Guid userId, string role, int page, int rpp, string? searchQuery);

        #endregion Methods
    }
}