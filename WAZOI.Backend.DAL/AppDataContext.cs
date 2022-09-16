using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Models;

namespace WAZOI.Backend.DAL
{
    public class AppDataContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        #region Constructors

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        { }

        #endregion Constructors

        #region Properties

        public DbSet<Subject> Subject
        {
            get;
            set;
        }

        public DbSet<SubjectTeacher> SubjectTeacher
        {
            get;
            set;
        }

        public DbSet<SubjectStudent> SubjectStudent
        {
            get;
            set;
        }

        public DbSet<Exam> Exam
        {
            get;
            set;
        }

        public DbSet<SolvedExam> SolvedExam
        {
            get;
            set;
        }

        public DbSet<ExamQuestion> ExamQuestion
        {
            get;
            set;
        }

        public DbSet<Question> Question
        {
            get;
            set;
        }

        public DbSet<ExamStudent> ExamStudent
        {
            get;
            set;
        }

        public DbSet<SubjectNotice> SubjectNotice
        {
            get;
            set;
        }

        public DbSet<UserSubjectJoinRequest> UserSubjectJoinRequest
        {
            get;
            set;
        }

        public DbSet<SubjectGradingCriteria> SubjectGradingCriteria
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()

                .Ignore(c => c.AccessFailedCount)
                .Ignore(c => c.LockoutEnabled)
                .Ignore(c => c.LockoutEnd)
                .Ignore(c => c.TwoFactorEnabled)
                .Ignore(c => c.PhoneNumber)
                .Ignore(c => c.PhoneNumberConfirmed)
                .Ignore(c => c.ConcurrencyStamp)
                .Ignore(c => c.AccessFailedCount);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole<Guid>>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");

            modelBuilder.Entity<SubjectNotice>().Property(t => t.DateCreated).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<Exam>().Property(t => t.DateCreated).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<Exam>().Property(e => e.IsLocked).HasDefaultValue(false);
            modelBuilder.Entity<SubjectTeacher>().Property(e => e.IsSubjectAdmin).HasDefaultValue(false);
            modelBuilder.Entity<ExamStudent>().Property(x => x.HasExamAccess).HasDefaultValue(true);
        }

        #endregion Methods
    }
}