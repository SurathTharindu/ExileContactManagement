using ExileContactManagement.Models;
using FluentNHibernate.Mapping;

namespace ExileContactManagement.Mappers
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.UId);
            Map(x=>x.UserName);
            Map(x=>x.Password);
            HasMany<Contact>(x => x.ContactList).Cascade.AllDeleteOrphan().Not.LazyLoad();
        }
    }
}