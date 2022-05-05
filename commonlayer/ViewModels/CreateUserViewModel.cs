using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGallery.Areas.Admin.Models.ViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage ="First Name Can't be Empty!")]

        public string FirstName { get; set; }
        [Required(ErrorMessage="Last Name Can't be Empty!")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email is not found")]
        public string Email { get; set; }


        [Required]
        [RegularExpression("^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8}$", ErrorMessage="Please enter strong Password!")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage ="Confirm Password not matched!")]
        public string ConfirmPassword { get; set; }
        
        public string Gender { get; set; }
        [Required(ErrorMessage ="Phone number can't be empty!")]
        public string Phone { get; set; }
    }
}
