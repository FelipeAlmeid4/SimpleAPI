using Application.Contracts;
using Application.Core.Application;
using Application.Models.Users;
using Domain.Users;

namespace Application.Application
{
    public class UserAppService : CrudApplicationService<User, int, UserEditingDto, UserDto, IUserRepository>, IUserAppService
    {
        public UserAppService(IUserRepository repository) : base(repository)
        { }
    }
}
