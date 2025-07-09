using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Core.Entitites.Models;

namespace Wallet.Business.Abstract
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(Product product);

        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product> UpdateProduct(int productId, decimal price);

        Task<Product> DeleteProduct(int productId);
    }
}
