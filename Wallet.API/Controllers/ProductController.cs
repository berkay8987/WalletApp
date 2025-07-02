using Microsoft.AspNetCore.Mvc;
using Wallet.Core.Entitites.Models;
using Wallet.DataAccess.Abstract;

namespace Wallet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductDal _productDal;

        public ProductController(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct(string name, decimal price)
        {
            var product = new Product { 
                Name = name, Price = price 
            };

            var success = _productDal.CreateProduct(product);
            return success
                ? Ok(product)
                : BadRequest("Failed to create a product");
        }

        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            var products = _productDal.GetAllProducts();
            return products != null 
                ? Ok(products) 
                : BadRequest("Failed to get products");
        }

        [HttpPost("UpdateProductById")]
        public IActionResult UpdateProductById()
        {
            return BadRequest("Not implemented");
        }

        [HttpPost("DeleteProductById")]
        public IActionResult DeleteProductById(int id)
        {
            var success = _productDal.DeleteProduct(id);

            return success
                ? Ok("Successfuly deleted the product")
                : BadRequest("Failed to delete product");
        }
    }
}
