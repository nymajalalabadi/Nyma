using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Domain.ViewModels.User
{
    public class LoginUserViewModel
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "The {0} Is Required")]
        [MaxLength(200, ErrorMessage = "{0} Cannot Be Longer Than {1} Characters")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "The {0} Is Required")]
        [MaxLength(200, ErrorMessage = "{0} Cannot Be Longer Than {1} Characters")]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}
