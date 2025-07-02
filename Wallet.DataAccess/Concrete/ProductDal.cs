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
    public class ProductDal : IProductDal
    {
        private readonly ApplicationDbContext _context;

        public ProductDal(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateProduct(Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public List<Product> GetAllProducts()
        {
            var products = _context.Products.ToList();
            if (products == null)
            {
                return null;
            }

            return products;
        }

        public Product UpdateProductById(int id, decimal price)
        {
            var product = _context.Products.FirstOrDefault(u => u.ProductId == id);
            if (product == null)
            {
                return null;
            }

            product.Price = price;
            _context.SaveChanges();
            return product;
        }

        public bool DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(u => u.ProductId == id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }
    }
}
