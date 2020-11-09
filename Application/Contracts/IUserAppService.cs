using Application.Models.Users;
using Domain.Users;
using static Application.Core.Application.ICrudApplicationService;

namespace Application.Contracts
{
    public interface IUserAppService : ICrudApplicationService<User, int, UserEditingDto, UserDto>
    { }
}
