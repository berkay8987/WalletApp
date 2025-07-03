using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Core.Entitites.Models;
using Wallet.DataAccess.Abstract;
using Wallet.DataAccess.Context;
using Wallet.Core.Entitites.Enums;

namespace Wallet.DataAccess.Concrete
{
    public class UserDal: IUserDal
    {
        private readonly ApplicationDbContext _context;
        private readonly ITransactionDal _transactionDal;

        public UserDal(ApplicationDbContext context, 
                       ITransactionDal transactionDal)
        {
            _context = context;
            _transactionDal = transactionDal;
        }

        public decimal AddBalanceByUserId(string userId, decimal value)
        {
            var user = _context.Users.Find(userId);
            user.Balance += value;
            user.LastUpdated = DateTime.UtcNow;
            Transaction transaction = new Transaction
            {
                UserId = userId,
                TotalPrice = value,
                Date = DateTime.UtcNow,
                TransactionType = TransactionType.AddBalance,
            };
            _transactionDal.CreateTransaction(transaction);
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
            }
            else
            {
                user.Balance -= value;
            }
            user.LastUpdated = DateTime.UtcNow;
            Transaction transaction = new Transaction
            {
                UserId = userId,
                TotalPrice = value,
                Date = DateTime.UtcNow,
                TransactionType = TransactionType.RemoveBalance,
            };
            _transactionDal.CreateTransaction(transaction);
            _context.SaveChanges();
            return user.Balance;
        }

        public DateTime GetLastUpdatedByUserId(string userId)
        {
            var user = _context.Users.Find(userId);
            return user.LastUpdated;
        }

        public User GetUserbyUserId(string userId)
        {
            var user = _context.Users.Find(userId);
            return user;
        }
    }
}
