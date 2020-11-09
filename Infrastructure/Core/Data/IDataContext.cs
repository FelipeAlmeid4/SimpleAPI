using NHibernate;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Core.Data
{
    public interface IDataContext : IDisposable
    {
        ISession Session { get; }
        IStatelessSession StatelessSession { get; }

        IQueryable<T> Query<T>();

        T Get<T, TId>(TId id);
        void Create<T>(T entity);
        void Update<T>(T entity);
        void Delete<T>(T entity);

        Task<T> GetAsync<T, TId>(TId id);
        Task CreateAsync<T>(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync<T>(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync<T>(T entity, CancellationToken cancellationToken = default);
    }
}
