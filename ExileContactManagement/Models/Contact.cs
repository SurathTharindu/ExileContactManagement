using System;
using System.ComponentModel.DataAnnotations;

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

        public Contact(string name, string address)
        {
            Name = name;
            Location = address;
        }

        [Required]
        public virtual String Name { get; set; }

        [Required]
        public virtual String Location { get; set; }

        public virtual int Id { get; set; }
        public virtual User User { get; set; }
    }
}