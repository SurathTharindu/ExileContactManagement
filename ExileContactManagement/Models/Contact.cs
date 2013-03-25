using System;

namespace ExileContactManagement.Models
{
    public class Contact
    {
        public Contact()
        {
            Id = 0;
            Name = "";
            Location = "";
        }
        public Contact(int id, string name, string location)
        {
            Id = id;
            Name = name;
            Location = location;
        }

        public virtual String Name { get; set; }
        public virtual String Location { get; set; }
        public virtual int Id { get; set; }
        public virtual User User { get; set; }

    }
}