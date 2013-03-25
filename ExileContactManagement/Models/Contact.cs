using System;
using System.ComponentModel.DataAnnotations;

namespace ExileContactManagement.Models
{
    public class Contact
    {        
        [Required]
        public virtual String Name { get; set; }
        [Required]
        public virtual String Location { get; set; }

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

        public int Id { get; set; }
    
        public User Owner;

    }
}