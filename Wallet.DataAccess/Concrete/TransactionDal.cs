using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Core.Entitites.Models;
using Wallet.DataAccess.Abstract;
using Wallet.DataAccess.Context;

namespace Wallet.DataAccess.Concrete
{
    public class TransactionDal: ITransactionDal
    {
        private readonly ApplicationDbContext _context;

        public TransactionDal(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Transaction> GetAllTransactions(string userId)
        {
            var transactions = _context.Transactions.Where(u => u.UserId == userId).ToList();
            return transactions;
        }
    }
}
