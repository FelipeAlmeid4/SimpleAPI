using Domain.Common.Entities;

namespace Domain.Users
{
    public class User : Entity<int>
    {
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
    }
}
