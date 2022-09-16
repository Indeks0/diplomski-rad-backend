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
    public class UserSubjectJoinRequestService : IUserSubjectJoinRequestService
    {
        #region Fields

        private IUnitOfWork _uow;
        private IUserSubjectJoinRequestRepository _repository;
        private ISubjectStudentRepository _subjectStudentRepository;

        #endregion Fields

        #region Constructors

        public UserSubjectJoinRequestService(IUnitOfWork uow)
        {
            this._uow = uow;
            _repository = uow.UserSubjectJoinRequestRepository;
            _subjectStudentRepository = uow.SubjectStudentRepository;
        }

        #endregion Constructors

        #region Methods

        public async Task<(List<IUserSubjectJoinRequestDto> Requests, int TotalCount)> FindJoinRequestsAsync(Guid subjectId, int page, int rpp)
        {
            var result = await _repository.FindJoinRequestsAsync(subjectId, page, rpp);
            return result;
        }

        public async Task<bool> AcceptJoinRequestAsync(IUserSubjectJoinRequestDto joinRequest)
        {
            var subjectStudent = new SubjectStudentDto();
            subjectStudent.SubjectId = joinRequest.SubjectId;
            subjectStudent.UserId = joinRequest.UserId;

            _subjectStudentRepository.Insert(subjectStudent);

            _repository.Delete(joinRequest);

            var result = await _uow.SaveAsync();
            return result > 0;
        }

        public async Task<bool> DeclineJoinRequestAsync(IUserSubjectJoinRequestDto joinRequest)
        {
            _repository.Delete(joinRequest);

            var result = await _uow.SaveAsync();
            return result > 0;
        }

        public async Task<bool> CreateJoinRequestAsync(IUserSubjectJoinRequestDto joinRequest)
        {
            _repository.Insert(joinRequest);

            var result = await _uow.SaveAsync();
            return result > 0;
        }

        #endregion Methods
    }
}