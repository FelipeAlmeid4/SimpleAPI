using Domain.Users;
using Infrastructure.Core.Data;
using Infrastructure.Core.Repositories;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        public UserRepository(IDataContext context) : base(context)
        { }
    }
}
