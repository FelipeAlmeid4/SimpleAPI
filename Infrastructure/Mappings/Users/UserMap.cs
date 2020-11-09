using Domain.Users;
using FluentNHibernate.Mapping;
using SimpleAPI.Framework.Extensions;

namespace Infrastructure.Mappings.Users
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("TBUSER");

            Id(c => c.Id).GeneratedBy.Native();
            Map(c => c.Name).Length(100).Indexable().Not.Nullable();
            Map(c => c.Email).Length(100).Indexable().Not.Nullable();
        }
    }
}
