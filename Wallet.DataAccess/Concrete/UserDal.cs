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

        public decimal AddBalanceByUserId(string userId, decimal value)
        {
            var user = _context.Users.Find(userId);
            user.Balance += value;
            _context.SaveChanges();
            return user.Balance;
        }

        public decimal GetBalanceByUserId(string userId)
        {
            var user = _context.Users.Find(userId);
            return user.Balance;
        }

        public decimal RemoveBalanceByUserId(string userId, decimal value)
        {
            var user = _context.Users.Find(userId);
            if ((user.Balance - value) < 0) {
                user.Balance = 0;
                _context.SaveChanges();
            }
            else
            {
                user.Balance -= value;
                _context.SaveChanges();
            }
            return user.Balance;
        }
    }
}
