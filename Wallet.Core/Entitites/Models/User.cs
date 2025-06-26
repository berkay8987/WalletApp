using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Core.Entitites.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public decimal Balance { get; set; }

        public ICollection<UserInventory> Inventories { get; set; } = new List<UserInventory>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
