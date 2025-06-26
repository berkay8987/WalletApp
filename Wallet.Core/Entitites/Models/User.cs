using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Core.Entitites.Models
{
    public class User : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public decimal Balance { get; set; }

        public ICollection<UserInventory> Inventories { get; set; } = new List<UserInventory>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
