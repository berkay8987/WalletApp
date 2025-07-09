using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Business.Abstract;
using Wallet.Core.Entitites.Models;

namespace Wallet.Business.Concrete
{
    public class ProductBl : IProductBl
    {
        public Product UpdateProductPrice(Product product, decimal price)
        {
            product.Price = price;
            return product;
        }
    }
}
