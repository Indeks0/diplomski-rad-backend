using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Models;
using WAZOI.Backend.Models.Common;
using WAZOI.Backend.Repository.Common;
using WAZOI.Backend.Services.Common;
using WAZOI.Backend.Services.Common.MailService;

namespace WAZOI.Backend.Services
{
    public class SubjectStudentService : ISubjectStudentService
    {
        #region Fields

        private ISubjectStudentRepository _repository;
        private IUnitOfWork _uow;
        private IEmailSender _emailSender;

        #endregion Fields

        #region Constructors

        public SubjectStudentService(IUnitOfWork uow, IEmailSender emailSender)
        {
            this._uow = uow;
            _repository = _uow.SubjectStudentRepository;
            _emailSender = emailSender;
        }

        #endregion Constructors

        #region Methods

        public async Task<int> DeleteAsync(Guid id)
        {
            var dto = new SubjectStudentDto();
            dto.Id = id;
            _repository.Delete(dto);
            return await _uow.SaveAsync();
        }

        public Task<(List<ISubjectStudentDto> Students, int TotalCount)> FindSubjectStudentsAsync(Guid subjectId, int page, int rpp, string? searchQuery)
        {
            return _repository.FindSubjectStudentsAsync(subjectId, page, rpp, searchQuery);
        }

        public async Task<ISubjectStudentDto> GetSubjectStudentAsync(Guid subjectId, Guid userId)
        {
            var result = await _repository.GetSubjectStudentAsync(subjectId, userId);
            return result;
        }

        public async void SendEmailsToStudentsAsync((List<ISubjectStudentDto> Students, int TotalCount) students, IExamDto exam, string subjectName)
        {
            foreach (var student in students.Students)
            {
                await _emailSender.SendEmailAsync(student.User.Email, "Obavijest o ispitu", "Poštovani,\n " +
                    "iz predmeta " + subjectName + " kreiran je ispit: " + exam.Name + ". \n" +
                    "Ispitu je moguće pristupiti: od " + exam.DateOpenStart.ToLocalTime().ToString() + "do: " + exam.DateOpenEnd.ToLocalTime().ToString() + " .");
            }
        }

        #endregion Methods
    }
}