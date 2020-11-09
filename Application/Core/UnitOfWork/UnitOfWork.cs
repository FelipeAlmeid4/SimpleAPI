using Infrastructure.Core.Data;
using SimpleAPI.Framework.Common;
using System.Threading.Tasks;

namespace Application.Core.UnitOfWork
{
    public class UnitOfWork : Disposable, IUnitOfWork
    {
        private readonly ITransactionManager transactionManager;

        public UnitOfWork(ITransactionManager transactionManager)
        {
            this.transactionManager = transactionManager;
        }

        public void Begin()
        {
            transactionManager.BeginTransaction();
        }

        public async Task CompleteAsync()
        {
            if (transactionManager.TransactionActive)
            {
                await transactionManager.CommitAsync();
            }
        }
    }
}
