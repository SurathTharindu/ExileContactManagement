using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExileContactManagement.Models
{
    public class User
    {

        public virtual int UId { get; set; }
        public virtual IList<Contact> ContactList { get; set; }

        [Required]
        [Display(Name = "User name")]
        public virtual string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password*")]
        public virtual string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password*")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public User()
        {
            UId = 0;
            UserName = "";
            Password = "";
        }

        public User(string username, string password)
        {
            ContactList = new List<Contact>();
            UserName = username;
            Password = password;
        }

    }
}