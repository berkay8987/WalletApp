using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Core.Entitites.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username must be provided.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Firstname must be provided.")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname must be provided.")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Password must be provided.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Must enter password again.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }
    }
}
