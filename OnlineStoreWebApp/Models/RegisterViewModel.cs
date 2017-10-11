using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreWebApp.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter First Name")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter First Name")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }


        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Please ener Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }


        [DisplayName("Notify Me")]
        public bool NotifyMe { get; set; }
    }
}
