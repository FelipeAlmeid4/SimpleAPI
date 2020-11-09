using NHibernate;
using SimpleAPI.Framework.Common;
using SimpleAPI.Framework.Extensions;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Core.Data
{
    public class TransactionManager : Disposable, ITransactionManager
    {
        private readonly ISessionProvider sessionProvider;
        private ITransaction transaction;

        public bool TransactionActive
        {
            get
            {
                return transaction?.IsActive ?? false;
            }
        }

        public TransactionManager(ISessionProvider sessionProvider)
        {
            this.sessionProvider = sessionProvider;
        }

        public void BeginTransaction()
        {
            if (!sessionProvider.Session.TransactionIsNullOrNotActive())
            {
                throw new Exception("The transaction has already been opened.");
            }

            sessionProvider.Session.Clear();
            transaction = sessionProvider.Session.BeginTransaction();
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Rollback()
        {
            transaction.Rollback();
        }

        public Task CommitAsync()
        {
            return transaction.CommitAsync();
        }

        public Task RollbackAsync()
        {
            return transaction.RollbackAsync();
        }

        protected override void OnDispose()
        {
            transaction?.Dispose();
            sessionProvider.Dispose();
        }
    }
}
