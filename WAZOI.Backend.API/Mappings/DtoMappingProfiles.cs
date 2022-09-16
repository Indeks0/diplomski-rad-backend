using AutoMapper;
using WAZOI.Backend.API.Controllers;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Models;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.API
{
    public class DtoMappingProfiles : Profile
    {
        #region Constructors

        public DtoMappingProfiles()
        {
            CreateMap<SubjectRest, ISubjectDto>().As<SubjectDto>();
            CreateMap<SubjectRest, SubjectDto>();

            CreateMap<ExamRest, IExamDto>().As<ExamDto>();
            CreateMap<ExamRest, ExamDto>().AfterMap((d, s) =>
            {
                var counter = 0;
                foreach (var question in s.Questions)
                {
                    question.OrdinalNumber = counter++;
                }
            });

            CreateMap<ExamQuestionRest, IExamQuestionDto>().As<ExamQuestionDto>();
            CreateMap<ExamQuestionRest, ExamQuestionDto>();

            CreateMap<ExamStudentRest, IExamStudentDto>().As<ExamStudentDto>();
            CreateMap<ExamStudentRest, ExamStudentDto>();

            CreateMap<SubjectNoticeRest, ISubjectNoticeDto>().As<SubjectNoticeDto>();
            CreateMap<SubjectNoticeRest, SubjectNoticeDto>();

            CreateMap<SubjectStudentRest, ISubjectStudentDto>().As<SubjectStudentDto>();
            CreateMap<SubjectStudentRest, SubjectStudentDto>();

            CreateMap<QuestionRest, IQuestionDto>().As<QuestionDto>();
            CreateMap<QuestionRest, QuestionDto>();

            CreateMap<SolvedExamRest, ISolvedExamDto>().As<SolvedExamDto>();
            CreateMap<SolvedExamRest, SolvedExamDto>();

            CreateMap<SubjectTeacherRest, ISubjectTeacherDto>().As<SubjectTeacherDto>();
            CreateMap<SubjectTeacherRest, SubjectTeacherDto>();

            CreateMap<SubjectGradingCriteriaRest, ISubjectGradingCriteriaDto>().As<SubjectGradingCriteriaDto>();
            CreateMap<SubjectGradingCriteriaRest, SubjectGradingCriteriaDto>();

            CreateMap<SolvedExamSubmitRest, ISolvedExamSubmitDto>().As<SolvedExamSubmitDto>();
            CreateMap<SolvedExamSubmitRest, SolvedExamSubmitDto>();

            CreateMap<SolvedExamAnswerRest, ISolvedExamAnswerDto>().As<SolvedExamAnswerDto>();
            CreateMap<SolvedExamAnswerRest, SolvedExamAnswerDto>();

            CreateMap<UserSubjectJoinRequestRest, IUserSubjectJoinRequestDto>().As<UserSubjectJoinRequestDto>();
            CreateMap<UserSubjectJoinRequestRest, UserSubjectJoinRequestDto>();
        }

        #endregion Constructors
    }
}