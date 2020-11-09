using NHibernate;
using SimpleAPI.Framework.Common;
using SimpleAPI.Framework.Extensions;

namespace Infrastructure.Core.Data
{
    public class SessionProvider : Disposable, ISessionProvider
    {
        private ISession session;
        private IStatelessSession statelessSession;
        private readonly ISessionFactory sessionFactory;

        public ISession Session
        {
            get
            {
                if (session.IsNull())
                {
                    session = sessionFactory.OpenSession();
                }

                return session;
            }
        }

        public IStatelessSession StatelessSession
        {
            get
            {
                if (statelessSession.IsNull())
                {
                    statelessSession = sessionFactory.OpenStatelessSession();
                }

                return statelessSession;
            }
        }

        public SessionProvider(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        protected override void OnDispose()
        {
            session?.Dispose();
            statelessSession?.Dispose();
        }
    }
}
