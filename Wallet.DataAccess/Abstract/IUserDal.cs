using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.DataAccess.Abstract
{
    public interface IUserDal
    {
        public decimal GetBalanceByUserId(string UserId);
    }
}
