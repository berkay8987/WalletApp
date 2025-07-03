using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Core.Entitites.Models;

namespace Wallet.Core.Entitites.ViewModels
{
    public class WalletDashboardViewModel
    {
        public User? User { get; set; }
        public List<Transaction>? Transactions { get; set; }
    }
}