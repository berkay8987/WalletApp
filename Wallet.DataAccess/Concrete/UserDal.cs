using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.DataAccess.Abstract;
using Wallet.DataAccess.Context;

namespace Wallet.DataAccess.Concrete
{
    public class UserDal: IUserDal
    {
        private readonly ApplicationDbContext _context;

        public UserDal(ApplicationDbContext context)
        {
            _context = context;
        }

        public decimal GetBalanceByUserId(string UserId)
        {
            var user = _context.Users.Find(UserId);
            return user.Balance;
        }
    }
}
