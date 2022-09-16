using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Models.Common;

namespace WAZOI.Backend.Services.Common
{
    public interface ISubjectService
    {
        #region Methods

        void Insert(ISubjectDto dto);

        void InsertRange(IEnumerable<ISubjectDto> dtos);

        Task<IEnumerable<ISubjectDto>> GetAllAsync();

        Task<ISubjectDto> GetByIdAsync(Guid id);

        Task<(List<ISubjectDto> Subjects, int TotalCount)> FindSubjectsAsync(int page, int rpp, string? searchQuery);

        Task<(List<ISubjectDto> Subjects, int TotalCount)> FindSubjectsWithJoinedStatusAsync(Guid userId, string userRole, int page, int rpp, string? searchQuery);

        Task<(List<ISubjectDto> Subjects, int TotalCount)> FindSubjectsForUserAsync(Guid userId, string role, int page, int rpp, string? searchQuery);

        Task<bool> CreateSubjectAsync(ISubjectDto subject, Guid userId);

        Task<int> DeleteAsync(ISubjectDto dto);

        void DeleteRange(IEnumerable<ISubjectDto> dtos);

        Task<int> UpdateAsync(ISubjectDto dto);

        #endregion Methods
    }
}