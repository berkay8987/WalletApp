using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Business.Abstract;
using Wallet.Core.Entitites.Models;
using Wallet.DataAccess.Abstract;

namespace Wallet.Business.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepo, 
                              IUnitOfWork unitOfWork)
        {
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            product = await _productRepo.AddAsync(product);
            await _unitOfWork.CommitAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await _productRepo.GetAllAsync();
            return products;
        }

        public async Task<Product> UpdateProduct(int productId, decimal price)
        {
            var product = await _productRepo.GetByIdAsync(productId)
                ?? throw new NullReferenceException("Product not found");

            product.Price = price;
            await _unitOfWork.CommitAsync();
            return product;
        }

        public async Task<Product> DeleteProduct(int productId)
        {
            var product = await _productRepo.GetByIdAsync(productId)
                ?? throw new NullReferenceException("Product not found");

            _productRepo.Remove(product);
            await _unitOfWork.CommitAsync();
            return product;
        }
    }
}
