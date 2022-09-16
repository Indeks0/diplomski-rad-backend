using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models;
using WAZOI.Backend.Models.Common;
using WAZOI.Backend.Repository.Common;
using WAZOI.Backend.Services.Common;

namespace WAZOI.Backend.Services
{
    public class SubjectTeacherService : ISubjectTeacherService
    {
        #region Fields

        private ISubjectTeacherRepository _repository;
        private IUnitOfWork _uow;

        #endregion Fields

        #region Constructors

        public SubjectTeacherService(IUnitOfWork uow)
        {
            this._uow = uow;
            _repository = _uow.SubjectTeacherRepository;
        }

        #endregion Constructors

        #region Methods

        public async Task<int> InsertTeacherAsync(Guid userId, Guid subjectId)
        {
            var subjectTeacher = new SubjectTeacherDto();
            subjectTeacher.UserId = userId;
            subjectTeacher.SubjectId = subjectId;
            subjectTeacher.IsSubjectAdmin = false;

            var isTeacher = await _repository.IsTeacher(userId, subjectId);

            if (isTeacher)
            {
                return 0;
            }

            _repository.Insert(subjectTeacher);

            return await _uow.SaveAsync();
        }

        public async Task<List<ISubjectTeacherDto>> FindSubjectTeachersAsync(Guid subjectId)
        {
            var teachers = await _repository.FindSubjectTeachersAsync(subjectId);

            return teachers;
        }

        public async Task<bool> GetIsTeacherAdminAsync(Guid userId, Guid subjectId)
        {
            var isTeacher = await _repository.GetIsTeacherAdminAsync(userId, subjectId);
            return isTeacher;
        }

        public async Task<int> DeleteAsync(ISubjectTeacherDto subjectTeacher)
        {
            _repository.Delete(subjectTeacher);
            return await _uow.SaveAsync();
        }

        #endregion Methods
    }
}