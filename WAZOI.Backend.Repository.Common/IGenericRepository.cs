using System.Linq.Expressions;

namespace WAZOI.Backend.Repository.Common
{
    public interface IGenericRepository<TDto, TEntity> where TDto : class where TEntity : class
    {
        #region Methods

        void Insert(TDto entity);

        void InsertRange(IEnumerable<TDto> entities);

        Task<IEnumerable<TDto>> GetAllAsync();

        void PutAsync(TDto entity);

        Task<TDto> GetByIdAsync(Guid id);

        void Delete(TDto entity);

        void DeleteRange(IEnumerable<TDto> entities);

        Task<int> GetCountAsync();

        #endregion Methods
    }
}