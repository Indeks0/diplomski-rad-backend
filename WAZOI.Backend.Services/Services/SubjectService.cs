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
    public class SubjectService : ISubjectService
    {
        #region Fields

        private IUnitOfWork _uow;
        private ISubjectRepository _repository;
        private IUserSubjectJoinRequestRepository _userSubjectJoinRequestRepository;
        private ISubjectStudentRepository _subjectStudentRepository;

        #endregion Fields

        #region Constructors

        public SubjectService(IUnitOfWork uow)
        {
            this._uow = uow;
            _repository = uow.SubjectRepository;
            _userSubjectJoinRequestRepository = uow.UserSubjectJoinRequestRepository;
            _subjectStudentRepository = uow.SubjectStudentRepository;
        }

        #endregion Constructors

        #region Methods

        public async Task<int> DeleteAsync(ISubjectDto dto)
        {
            _repository.Delete(dto);
            return await _uow.SaveAsync();
        }

        public Task<(List<ISubjectDto> Subjects, int TotalCount)> FindSubjectsAsync(int page, int rpp, string? searchQuery)
        {
            return _repository.FindSubjectsAsync(page, rpp, searchQuery);
        }

        public async Task<(List<ISubjectDto> Subjects, int TotalCount)> FindSubjectsWithJoinedStatusAsync(Guid userId, string userRole, int page, int rpp, string? searchQuery)
        {
            var result = await _repository.FindSubjectsAsync(page, rpp, searchQuery);
            foreach (var subject in result.Subjects)
            {
                var subjectStudent = await _subjectStudentRepository.GetSubjectStudentAsync(subject.Id, userId);

                if (subjectStudent != null)
                {
                    subject.EnrollmentStatus = "joined";
                }
                else
                {
                    var userSubjectJoinRequest = await _userSubjectJoinRequestRepository.FindBySubjectIdAsync(subject.Id, userId);

                    if (userSubjectJoinRequest != null)
                    {
                        subject.EnrollmentStatus = "pending";
                    }
                    else
                    {
                        subject.EnrollmentStatus = "none";
                    }
                }
            }
            return result;
        }

        public async Task<bool> CreateSubjectAsync(ISubjectDto subject, Guid userId)
        {
            var teacher = new SubjectTeacherDto();
            teacher.UserId = userId;
            teacher.Subject = subject;
            teacher.IsSubjectAdmin = true;

            _repository.CreateSubject(teacher);
            return await _uow.SaveAsync() > 0;
        }

        public Task<IEnumerable<ISubjectDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ISubjectDto> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public void Insert(ISubjectDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateAsync(ISubjectDto dto)
        {
            _repository.PutAsync(dto);

            return await _uow.SaveAsync();
        }

        public void InsertRange(IEnumerable<ISubjectDto> dtos)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<ISubjectDto> dtos)
        {
            throw new NotImplementedException();
        }

        public async Task<(List<ISubjectDto> Subjects, int TotalCount)> FindSubjectsForUserAsync(Guid userId, string role, int page, int rpp, string? searchQuery)
        {
            var subjects = await _repository.FindSubjectsForUserAsync(userId, role, page, rpp, searchQuery);
            foreach (var subject in subjects.Subjects)
            {
                subject.EnrollmentStatus = "joined";
            }
            return subjects;
        }

        #endregion Methods
    }
}