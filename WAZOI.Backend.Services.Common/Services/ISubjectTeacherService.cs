using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Services.Common
{
    public interface ISubjectTeacherService
    {
        #region Methods

        Task<int> InsertTeacherAsync(Guid userId, Guid subjectId);

        Task<List<ISubjectTeacherDto>> FindSubjectTeachersAsync(Guid subjectId);

        Task<bool> GetIsTeacherAdminAsync(Guid userId, Guid subjectId);

        Task<int> DeleteAsync(ISubjectTeacherDto subjectTeacher);

        #endregion Methods
    }
}