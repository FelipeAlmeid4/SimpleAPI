using Domain.Common.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Core.Repositories
{
    public interface IRepository
    { }

    public interface IRepository<TEntity, TId>
        where TEntity : IEntity<TId>
    {
        TEntity Get(TId id);
        TEntity GetFull(TId id);
        IList<TEntity> GetAll();
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TId id);

        Task<TEntity> GetAsync(TId id);
        Task<TEntity> GetFullAsync(TId id);
        Task<List<TEntity>> GetAllAsync();
        Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(TId id);
    }
}
