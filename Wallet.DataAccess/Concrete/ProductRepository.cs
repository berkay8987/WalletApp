using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Wallet.Core.Entitites.Models;
using Wallet.DataAccess.Abstract;
using Wallet.DataAccess.Context;

namespace Wallet.DataAccess.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<Product> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            return product;
        }

        public async Task<Product> GetByIdAsync(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
            return product;
        }

        public void Remove(Product product) 
        {
            _context.Products.Remove(product);
        }
    }
}
