using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Core.Entitites.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username must be provided.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password must be provided.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
