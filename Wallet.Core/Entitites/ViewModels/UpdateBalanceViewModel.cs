using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Core.Entitites.ViewModels
{
    public class UpdateBalanceViewModel
    {
        public string Username { get; set; }

        [Range(0, double.MaxValue), Required(ErrorMessage = "Balance must be zero or positive.")]
        public decimal Balance { get; set; }
    }
}
