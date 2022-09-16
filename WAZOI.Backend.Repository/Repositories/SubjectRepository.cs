using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.DAL;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Models.Common;
using WAZOI.Backend.Repository.Common;

namespace WAZOI.Backend.Repository
{
    public class SubjectRepository : GenericRepository<ISubjectDto, Subject>, ISubjectRepository
    {
        #region Constructors

        public SubjectRepository(AppDataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<(List<ISubjectDto> Subjects, int TotalCount)> FindSubjectsAsync(int page, int rpp, string? searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                var result = base._context.Subject.Include(subject => subject.SubjectTeachers).ThenInclude(subjectTeacher => subjectTeacher.User).OrderBy(o => o.Name).Skip((page - 1) * rpp).Take(rpp).ToList();
                var totalCount = base._context.Subject.Count();
                return (base._mapper.Map<List<ISubjectDto>>(result), totalCount);
            }
            else
            {
                var result = base._context.Subject.Include(i => i.SubjectTeachers).ThenInclude(subjectTeacher => subjectTeacher.User).Where(x => EF.Functions.ILike(x.Name, $"%{searchQuery}%")).OrderBy(o => o.Name).Skip((page - 1) * rpp).Take(rpp).ToList();
                var totalCount = base._context.Subject.Where(x => EF.Functions.ILike(x.Name, $"%{searchQuery}%")).Count();
                return (base._mapper.Map<List<ISubjectDto>>(result), totalCount);
            }
        }

        public async Task<(List<ISubjectDto> Subjects, int TotalCount)> FindSubjectsForUserAsync(Guid userId, string role, int page, int rpp, string? searchQuery)
        {
            if (role == "Student")
            {
                if (string.IsNullOrEmpty(searchQuery))
                {
                    var result = base._context.Subject.Where(x => x.SubjectStudents.Any(y => y.UserId == userId)).Include(subject => subject.SubjectTeachers).ThenInclude(subjectTeacher => subjectTeacher.User).OrderBy(o => o.Name).Skip((page - 1) * rpp).Take(rpp).ToList();
                    var totalCount = base._context.Subject.Where(x => x.SubjectStudents.Any(y => y.UserId == userId)).Count();
                    return (base._mapper.Map<List<ISubjectDto>>(result), totalCount);
                }
                else
                {
                    var result = base._context.Subject.Where(x => x.SubjectStudents.Any(y => y.UserId == userId)).Include(i => i.SubjectTeachers).ThenInclude(subjectTeacher => subjectTeacher.User).Where(x => EF.Functions.ILike(x.Name, $"%{searchQuery}%")).OrderBy(o => o.Name).Skip((page - 1) * rpp).Take(rpp).ToList();
                    var totalCount = base._context.Subject.Where(x => x.SubjectStudents.Any(y => y.UserId == userId)).Where(x => EF.Functions.ILike(x.Name, $"%{searchQuery}%")).Count();
                    return (base._mapper.Map<List<ISubjectDto>>(result), totalCount);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(searchQuery))
                {
                    var result = base._context.Subject.Where(x => x.SubjectTeachers.Any(y => y.UserId == userId)).Include(subject => subject.SubjectTeachers).ThenInclude(subjectTeacher => subjectTeacher.User).OrderBy(o => o.Name).Skip((page - 1) * rpp).Take(rpp).ToList();
                    var totalCount = base._context.Subject.Where(x => x.SubjectTeachers.Any(y => y.UserId == userId)).Count();
                    return (base._mapper.Map<List<ISubjectDto>>(result), totalCount);
                }
                else
                {
                    var result = base._context.Subject.Where(x => x.SubjectTeachers.Any(y => y.UserId == userId)).Include(i => i.SubjectTeachers).ThenInclude(subjectTeacher => subjectTeacher.User).Where(x => EF.Functions.ILike(x.Name, $"%{searchQuery}%")).OrderBy(o => o.Name).Skip((page - 1) * rpp).Take(rpp).ToList();
                    var totalCount = base._context.Subject.Where(x => x.SubjectTeachers.Any(y => y.UserId == userId)).Where(x => EF.Functions.ILike(x.Name, $"%{searchQuery}%")).Count();
                    return (base._mapper.Map<List<ISubjectDto>>(result), totalCount);
                }
            }
        }

        public async void CreateSubject(ISubjectTeacherDto teacherWithSubject)
        {
            base._context.SubjectTeacher.AddAsync(base._mapper.Map<SubjectTeacher>(teacherWithSubject));
        }

        #endregion Constructors
    }
}