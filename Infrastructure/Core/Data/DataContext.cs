using NHibernate;
using SimpleAPI.Framework.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Core.Data
{
    public class DataContext : Disposable, IDataContext
    {
        protected readonly ISessionProvider sessionProvider;
        public ISession Session { get { return sessionProvider.Session; } }
        public IStatelessSession StatelessSession { get { return sessionProvider.StatelessSession; } }

        public DataContext(ISessionProvider sessionProvider)
        {
            this.sessionProvider = sessionProvider;
        }

        protected override void OnDispose()
        {
            sessionProvider.Dispose();
        }

        public IQueryable<T> Query<T>()
        {
            return Session.Query<T>();
        }

        public T Get<T, TId>(TId id)
        {
            return Session.Get<T>(id);
        }

        public void Create<T>(T entity)
        {
            Session.Save(entity);
        }

        public void Update<T>(T entity)
        {
            Session.Update(entity);
        }

        public void Delete<T>(T entity)
        {
            Session.Delete(entity);
        }

        public Task<T> GetAsync<T, TId>(TId id)
        {
            return Session.GetAsync<T>(id);
        }

        public Task CreateAsync<T>(T entity, CancellationToken cancellationToken = default)
        {
            return Session.SaveAsync(entity, cancellationToken);
        }

        public Task UpdateAsync<T>(T entity, CancellationToken cancellationToken = default)
        {
            return Session.UpdateAsync(entity, cancellationToken);
        }

        public Task DeleteAsync<T>(T entity, CancellationToken cancellationToken = default)
        {
            return Session.DeleteAsync(entity, cancellationToken);
        }
    }
}
