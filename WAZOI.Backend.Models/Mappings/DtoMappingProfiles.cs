using AutoMapper;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Models.Mappings
{
    public class DtoMappingProfiles : Profile
    {
        #region Constructors

        public DtoMappingProfiles()
        {
            CreateMap<Exam, IExamDto>().As<ExamDto>();
            CreateMap<Exam, ExamDto>();
            CreateMap<IExamDto, Exam>();

            CreateMap<ExamQuestion, IExamQuestionDto>().As<ExamQuestionDto>();
            CreateMap<ExamQuestion, ExamQuestionDto>();
            CreateMap<IExamQuestionDto, ExamQuestion>();

            CreateMap<ExamStudent, IExamStudentDto>().As<ExamStudentDto>();
            CreateMap<ExamStudent, ExamStudentDto>();
            CreateMap<IExamStudentDto, ExamStudent>();

            CreateMap<Question, IQuestionDto>().As<QuestionDto>();
            CreateMap<Question, QuestionDto>();
            CreateMap<IQuestionDto, Question>();

            CreateMap<SolvedExam, ISolvedExamDto>().As<SolvedExamDto>();
            CreateMap<SolvedExam, SolvedExamDto>();
            CreateMap<ISolvedExamDto, SolvedExam>();

            CreateMap<Subject, ISubjectDto>().As<SubjectDto>();
            CreateMap<Subject, SubjectDto>();
            CreateMap<ISubjectDto, Subject>();

            CreateMap<SubjectNotice, ISubjectNoticeDto>().As<SubjectNoticeDto>();
            CreateMap<SubjectNotice, SubjectNoticeDto>();
            CreateMap<ISubjectNoticeDto, SubjectNotice>();

            CreateMap<SubjectStudent, ISubjectStudentDto>().As<SubjectStudentDto>();
            CreateMap<SubjectStudent, SubjectStudentDto>();
            CreateMap<ISubjectStudentDto, SubjectStudent>();

            CreateMap<SubjectGradingCriteria, ISubjectGradingCriteriaDto>().As<SubjectGradingCriteriaDto>();
            CreateMap<SubjectGradingCriteria, SubjectGradingCriteriaDto>();
            CreateMap<ISubjectGradingCriteriaDto, SubjectGradingCriteria>();

            CreateMap<SubjectTeacher, ISubjectTeacherDto>().As<SubjectTeacherDto>();
            CreateMap<SubjectTeacher, SubjectTeacherDto>();
            CreateMap<ISubjectTeacherDto, SubjectTeacher>();

            CreateMap<User, IUserDto>().As<UserDto>();
            CreateMap<User, UserDto>();
            CreateMap<IUserDto, User>();

            CreateMap<UserSubjectJoinRequest, IUserSubjectJoinRequestDto>().As<UserSubjectJoinRequestDto>();
            CreateMap<UserSubjectJoinRequest, UserSubjectJoinRequestDto>();
            CreateMap<IUserSubjectJoinRequestDto, UserSubjectJoinRequest>();
        }

        #endregion Constructors
    }
}