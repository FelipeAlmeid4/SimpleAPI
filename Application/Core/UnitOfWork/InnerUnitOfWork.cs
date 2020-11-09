using SimpleAPI.Framework.Common;
using System.Threading.Tasks;

namespace Application.Core.UnitOfWork
{
    public class InnerUnitOfWork : Disposable, IUnitOfWork
    {
        public void Begin()
        { }

        public Task CompleteAsync()
        {
            return Task.FromResult(true);
        }
    }
}
