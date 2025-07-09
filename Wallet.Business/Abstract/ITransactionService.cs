using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Core.Entitites.Enums;
using Wallet.Core.Entitites.Models;

namespace Wallet.Business.Abstract
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAllForUserAsync(string userId);

        Task AddAsync(string userId, decimal value, TransactionType transactionType);
    }
}
