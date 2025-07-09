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
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetAsync(string userId)
        {
            var user  = await _context.Users.FindAsync(userId);
            return user;
        }

    }
}
