using AutoMapper;
using WAZOI.Backend.DAL;
using WAZOI.Backend.Repository.Common;

namespace WAZOI.Backend.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields

        private AppDataContext _context;

        #endregion Fields

        #region Constructors

        public UnitOfWork(AppDataContext context, IMapper mapper)
        {
            this._context = context;
            SubjectRepository = new SubjectRepository(this._context, mapper);
            QuestionRepository = new QuestionRepository(this._context, mapper);
            ExamQuestionRepository = new ExamQuestionRepository(this._context, mapper);
            SubjectNoticeRepository = new SubjectNoticeRepository(this._context, mapper);
            SubjectStudentRepository = new SubjectStudentRepository(this._context, mapper);
            SubjectTeacherRepository = new SubjectTeacherRepository(this._context, mapper);
            SubjectGradingCriteriaRepository = new SubjectGradingCriteriaRepository(this._context, mapper);
            ExamRepository = new ExamRepository(this._context, mapper);
            SolvedExamRepository = new SolvedExamRepository(this._context, mapper);
            ExamStudentRepository = new ExamStudentRepository(this._context, mapper);
            UserSubjectJoinRequestRepository = new UserSubjectJoinRequestRepository(this._context, mapper);
        }

        #endregion Constructors

        #region Properties

        public ISubjectRepository SubjectRepository
        {
            get;
            private set;
        }

        public IExamRepository ExamRepository
        {
            get;
            private set;
        }

        public ISolvedExamRepository SolvedExamRepository
        {
            get;
            private set;
        }

        public IUserSubjectJoinRequestRepository UserSubjectJoinRequestRepository
        {
            get;
            private set;
        }

        public ISubjectNoticeRepository SubjectNoticeRepository
        {
            get;
            private set;
        }

        public ISubjectStudentRepository SubjectStudentRepository
        {
            get;
            private set;
        }

        public ISubjectTeacherRepository SubjectTeacherRepository
        {
            get;
            private set;
        }

        public IQuestionRepository QuestionRepository
        {
            get;
            private set;
        }

        public IExamStudentRepository ExamStudentRepository
        {
            get;
            private set;
        }

        public IExamQuestionRepository ExamQuestionRepository
        {
            get;
            private set;
        }

        public ISubjectGradingCriteriaRepository SubjectGradingCriteriaRepository
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result;
        }

        #endregion Methods
    }
}