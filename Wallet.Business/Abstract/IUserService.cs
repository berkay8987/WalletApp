using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Core.Entitites.Models;

namespace Wallet.Business.Abstract
{
    public interface IUserService
    {
        Task<decimal> GetBalanceAsync(string userId);

        Task<DateTime> GetLastUpdatedAsync(string userId);

        Task<User?> GetUserAsync(string userId);

        Task<decimal> AddBalanceAsync(string userId, decimal value);

        Task<decimal> RemoveBalanceAsync(string userId, decimal value);
    }
}
