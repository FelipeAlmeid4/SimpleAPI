using Application.Models.Users;
using Domain.Users;
using SimpleAPI.Framework.AutoMapper;

namespace Api.Core
{
    public class ApplicationMapperProfile : MapperProfile
    {
        public ApplicationMapperProfile()
        {
            CreateMapReverse<User, UserEditingDto, UserDto>();
        }
    }
}
