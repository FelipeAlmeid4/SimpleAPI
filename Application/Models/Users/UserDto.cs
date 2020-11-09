using Application.Core.Models;

namespace Application.Models.Users
{
    public class UserDto : IEntityDto<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
