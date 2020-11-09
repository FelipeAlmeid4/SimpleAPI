using Application.Core.Models;

namespace Application.Models.Users
{
    public class UserEditingDto : IEntityEditingDto<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
