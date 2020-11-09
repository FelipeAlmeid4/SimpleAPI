using Domain.Core.Repositories;

namespace Domain.Users
{
    public interface IUserRepository : IRepository<User, int>
    {
    }
}
