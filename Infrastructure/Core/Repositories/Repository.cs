using Domain.Common.Entities;
using Domain.Core.Repositories;
using Infrastructure.Core.Data;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Core.Repositories
{
    public abstract class Repository : IRepository
    {
        protected readonly IDataContext context;

        protected Repository(IDataContext context)
        {
            this.context = context;
        }
    }

    public class Repository<TEntity, TId> : Repository, IRepository<TEntity, TId> where TEntity : class, IEntity<TId>
    {
        public Repository(IDataContext context) : base(context)
        { }

        public TEntity Get(TId id)
        {
            return context.Get<TEntity, TId>(id);
        }

        public TEntity GetFull(TId id)
        {
            return context.Get<TEntity, TId>(id);
        }

        public IList<TEntity> GetAll()
        {
            return context.Query<TEntity>().ToList();
        }

        public void Create(TEntity entity)
        {
            context.Create(entity);
        }

        public void Update(TEntity entity)
        {
            context.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            context.Delete(entity);
        }

        public void Delete(TId id)
        {
            context.Delete(Get(id));
        }

        public Task<TEntity> GetAsync(TId id)
        {
            return context.GetAsync<TEntity, TId>(id);
        }

        public Task<TEntity> GetFullAsync(TId id)
        {
            return context.GetAsync<TEntity, TId>(id);
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return context.Query<TEntity>().ToListAsync();
        }

        public Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return context.CreateAsync(entity, cancellationToken);
        }

        public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return context.UpdateAsync(entity, cancellationToken);
        }

        public Task DeleteAsync(TEntity entity)
        {
            return context.DeleteAsync(entity);
        }

        public Task DeleteAsync(TId id)
        {
            return context.DeleteAsync(Get(id));
        }
    }
}
