using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExileContactManagement.Models
{
    public class User
    {
        public  String UserName ;
        public int UId;
        public List<Contact> ContactList;

        public User()
        {
            ContactList = new List<Contact>();
        }
    }
}