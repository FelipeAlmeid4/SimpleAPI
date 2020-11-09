using System;
using System.Threading.Tasks;

namespace Infrastructure.Core.Data
{
    public interface ITransactionManager : IDisposable
    {
        bool TransactionActive { get; }

        void BeginTransaction();

        void Commit();
        void Rollback();

        Task CommitAsync();
        Task RollbackAsync();
    }
}
