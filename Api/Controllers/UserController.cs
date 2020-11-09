using Api.Core.Controllers;
using Application.Contracts;
using Application.Models.Users;
using Domain.Users;

namespace Api.Controllers
{
    public class UserController : CrudApiController<User, int, UserEditingDto, UserDto, IUserAppService>
    {
        public UserController(IUserAppService appService) : base(appService)
        { }
    }
}
