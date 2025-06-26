using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Core.Entitites.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ICollection<TransactionDetails> TransactionDetails { get; set; } = new List<TransactionDetails>();
        public ICollection<UserInventory> UserInventories { get; set; } = new List<UserInventory>();
    }
}
