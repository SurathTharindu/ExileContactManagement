using ExileContactManagement.Models;
using FluentNHibernate.Mapping;

namespace ExileContactManagement.Mappers
{
    public class ContactMap : ClassMap<Contact>
    {
        public ContactMap()
        {
            Id(x => x.Id).Unique();
            Map(x => x.Name);
            Map(x=>x.Location);

            References<User>(x => x.User);
        }
    }
}