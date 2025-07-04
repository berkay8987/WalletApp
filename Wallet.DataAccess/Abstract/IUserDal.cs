﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Core.Entitites.Models;

namespace Wallet.DataAccess.Abstract
{
    public interface IUserDal
    {
        public decimal GetBalanceByUserId(string UserId);

        public decimal AddBalanceByUserId(string userId, decimal value);

        public decimal RemoveBalanceByUserId(string userId, decimal value);

        public DateTime GetLastUpdatedByUserId(string userId);

        public User GetUserbyUserId(string userId);
    }
}
