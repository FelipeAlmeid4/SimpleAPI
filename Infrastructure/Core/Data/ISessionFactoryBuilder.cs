using NHibernate;

namespace Infrastructure.Core.Data
{
    public interface ISessionFactoryBuilder
    {
        ISessionFactory BuildSessionFactory();
    }
}
