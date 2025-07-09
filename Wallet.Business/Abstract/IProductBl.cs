using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Core.Entitites.Models;

namespace Wallet.Business.Abstract
{
    public interface IProductBl
    {
        Product UpdateProductPrice(Product product, decimal price);
    }
}
