using NHibernate;

namespace SimpleAPI.Framework.Extensions
{
    public static class NhibernateExtension
    {
        public static bool IsNull(this ISession session)
        {
            return session == null;
        }

        public static bool TransactionIsNullOrNotActive(this ISession session)
        {
            return session.GetCurrentTransaction().IsNullOrNotActive();
        }

        public static bool IsNull(this IStatelessSession statelessSession)
        {
            return statelessSession == null;
        }

        public static bool IsNullOrNotActive(this ITransaction transaction)
        {
            return transaction == null || !transaction.IsActive;
        }
    }
}
