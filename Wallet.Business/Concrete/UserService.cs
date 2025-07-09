using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Business.Abstract;
using Wallet.Core.Entitites.Models;
using Wallet.DataAccess.Abstract;
using Wallet.Core.Entitites.Enums;

namespace Wallet.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IUnitOfWork _unitOfWork;

        public UserService (IUserRepository userRepo, 
                            IUnitOfWork unitOfWork)
        {
            _userRepo = userRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<decimal> GetBalanceAsync(string userId)
        {
            var user = await _userRepo.GetAsync(userId) 
                ?? throw new KeyNotFoundException();
            
            return user.Balance;
        }

        public async Task<DateTime> GetLastUpdatedAsync(string userId)
        {
            var user = await _userRepo.GetAsync(userId)
                ?? throw new KeyNotFoundException();

            return user.LastUpdated;
        }

        public async Task<User?> GetUserAsync(string userId)
        {
            var user = await _userRepo.GetAsync(userId)
                ?? throw new KeyNotFoundException();

            return user;
        }
        public async Task<User?> AddBalanceAsync(string userId, decimal value)
        {
            var user = await _userRepo.GetAsync(userId)
                ?? throw new KeyNotFoundException();

            user.Balance += value;
            await _unitOfWork.CommitAsync();
            return user;
        }

        public async Task<User?> RemoveBalanceAsync(string userId, decimal value)
        {
            var user = await _userRepo.GetAsync(userId)
                ?? throw new KeyNotFoundException();

            user.Balance -= value;
            await _unitOfWork.CommitAsync();
            return user;
        }
    }
}
