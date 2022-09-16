using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WAZOI.Backend.DAL;
using WAZOI.Backend.Repository.Common;

namespace WAZOI.Backend.Repository
{
    public class GenericRepository<TDto, TEntity> : IGenericRepository<TDto, TEntity> where TDto : class where TEntity : class
    {
        #region Fields

        protected readonly AppDataContext _context;
        protected readonly IMapper _mapper;
        private DbSet<TEntity> _table;

        #endregion Fields

        #region Constructors

        public GenericRepository(AppDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _table = context.Set<TEntity>();
        }

        #endregion Constructors

        #region Methods

        public void Insert(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _table.Add(entity);
        }

        public void InsertRange(IEnumerable<TDto> dtos)
        {
            var entities = _mapper.Map<List<TEntity>>(dtos);
            _table.AddRange(entities);
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _table.ToListAsync();
            return _mapper.Map<List<TDto>>(entities);
        }

        public async Task<TDto> GetByIdAsync(Guid id)
        {
            var entity = await _table.FindAsync(id);
            return _mapper.Map<TDto>(entity);
        }

        public void Delete(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _table.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TDto> dtos)
        {
            var entities = _mapper.Map<List<TEntity>>(dtos).ToList();
            _table.RemoveRange(entities);
        }

        public async Task<int> GetCountAsync()
        {
            return await _table.CountAsync();
        }

        public async void PutAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _table.Update(entity);
        }

        #endregion Methods
    }
}