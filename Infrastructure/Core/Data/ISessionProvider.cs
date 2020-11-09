using NHibernate;
using System;

namespace Infrastructure.Core.Data
{
    public interface ISessionProvider : IDisposable
    {
        ISession Session { get; }
        IStatelessSession StatelessSession { get; }
    }
}
