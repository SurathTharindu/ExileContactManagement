﻿using System.Collections.Generic;

namespace ExileContactManagement.Models
{
    public class User
    {
        public User()
        {
            UId = 0;
            UserName = "";
            Password = "";
            ContactList = new List<Contact>();
        }

        public User(string username, string password)
        {
            ContactList = new List<Contact>();
            UserName = username;
            Password = password;
        }

        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual int UId { get; set; }
        public virtual IList<Contact> ContactList { get; set; }
    }
}