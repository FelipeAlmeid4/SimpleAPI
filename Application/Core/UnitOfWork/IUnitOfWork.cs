using System;
using System.Threading.Tasks;

namespace Application.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task CompleteAsync();
        void Begin();
    }
}
