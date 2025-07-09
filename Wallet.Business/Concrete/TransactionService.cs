using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Business.Abstract;
using Wallet.Core.Entitites.Enums;
using Wallet.Core.Entitites.Models;
using Wallet.DataAccess.Abstract;

namespace Wallet.Business.Concrete
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepo;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(ITransactionRepository transactionRepo, 
                                  IUnitOfWork unitOfWork)
        {
            _transactionRepo = transactionRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Transaction>> GetAllForUserAsync(string userId)
        {
            var transactions = await _transactionRepo.GetAllForUserAsync(userId)
                ?? throw new NullReferenceException("No transaction found.");

            return transactions;
        }

        public async Task AddAsync(string userId, decimal value, TransactionType transactionType)
        {
            Transaction transaction = new Transaction
            {
                UserId = userId,
                TotalPrice = value,
                TransactionType = transactionType,
                Date = DateTime.UtcNow
            };

            await _transactionRepo.AddAsync(transaction);
            await _unitOfWork.CommitAsync();
        }
    }
}
