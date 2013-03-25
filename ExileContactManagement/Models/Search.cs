using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExileContactManagement.Models
{
    public class Search
    {
        [Required]
        public String SearchString { get; set; }

        public List<Contact> ResultList
        {
            get { return result; }
            set { result = value; }
        }
        private List<Contact> result;

        public Search()
        {
            SearchString = "";
            result = new List<Contact>();
        }
    }
}