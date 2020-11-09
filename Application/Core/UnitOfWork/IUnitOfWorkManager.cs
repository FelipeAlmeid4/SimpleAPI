using System;

namespace Application.Core.UnitOfWork
{
    public interface IUnitOfWorkManager : IDisposable
    {
        IUnitOfWork Begin();
        bool HasActiveUnitOfWork();
    }
}
