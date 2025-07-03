using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Core.Entitites.Enums;

namespace Wallet.Core.Entitites.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string UserId { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }

        public User User { get; set; }

        public ICollection<TransactionDetails> TransactionDetails { get; set; } = new List<TransactionDetails>();
    }
}
