
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGallery.Areas.Admin.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Email is required")]

        public string UserEmail { get; set; }
        [Required(ErrorMessage="password is required")]
        
        public string Password { get; set; }
        public bool Rememberme{ get; set; }
    }
}
