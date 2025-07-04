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
            try
            {
                var products = _context.Products.ToList();
                return products;

            } 
            catch ( Exception ex )
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public Product UpdateProductById(int id, decimal price)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(u => u.ProductId == id);
                product.Price = price;
                _context.SaveChanges();
                return product;
            }
            catch ( Exception ex )
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(u => u.ProductId == id);
                _context.Products.Remove(product);
                _context.SaveChanges();
                return true;
            }
            catch 
            { 
                return false;
            }
        }
    }
}
