namespace WAZOI.Backend.Repository.Common
{
    public interface IUnitOfWork : IDisposable
    {
        #region Properties

        ISubjectRepository SubjectRepository
        {
            get;
        }

        IQuestionRepository QuestionRepository
        {
            get;
        }

        IExamQuestionRepository ExamQuestionRepository
        {
            get;
        }

        ISubjectNoticeRepository SubjectNoticeRepository
        {
            get;
        }

        IExamRepository ExamRepository
        {
            get;
        }

        IExamStudentRepository ExamStudentRepository
        {
            get;
        }

        ISubjectGradingCriteriaRepository SubjectGradingCriteriaRepository
        {
            get;
        }

        IUserSubjectJoinRequestRepository UserSubjectJoinRequestRepository { get; }
        ISubjectStudentRepository SubjectStudentRepository { get; }
        ISubjectTeacherRepository SubjectTeacherRepository { get; }
        ISolvedExamRepository SolvedExamRepository { get; }

        #endregion Properties

        #region Methods

        Task<int> SaveAsync();

        #endregion Methods
    }
}