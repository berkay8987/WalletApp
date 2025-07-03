using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Core.Entitites.Models;

namespace Wallet.DataAccess.Abstract
{
    public interface ITransactionDal
    {
        List<Transaction> GetAllTransactions(string userId);
    }
}
