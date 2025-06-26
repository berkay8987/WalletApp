using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Core.Entitites.Models
{
    public class UserInventory
    {
        public int InventoryId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public User User { get; set; }

        public Product Product { get; set; }
    }
}
