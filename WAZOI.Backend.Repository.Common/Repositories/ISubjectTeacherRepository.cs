using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Repository.Common
{
    public interface ISubjectTeacherRepository : IGenericRepository<ISubjectTeacherDto, SubjectTeacher>
    {
        #region Methods

        Task<ISubjectTeacherDto> FindSubjectTeacherAsync(Guid userId, Guid subjectId);

        Task<bool> IsTeacher(Guid userId, Guid subjectId);

        Task<List<ISubjectTeacherDto>> FindSubjectTeachersAsync(Guid subjectId);

        Task<bool> GetIsTeacherAdminAsync(Guid userId, Guid subjectId);

        #endregion Methods
    }
}