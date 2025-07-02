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

        /// <summary>
        ///     Creates a product with specified name and price.
        /// </summary>
        /// <param name="name">Name of the product</param>
        /// <param name="price">Price of the product</param>
        /// <returns>Returns the product created.</returns>

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

        /// <summary>
        ///     Returns all products in db
        /// </summary>
        /// <returns>List of products</returns>

        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            var products = _productDal.GetAllProducts();
            return products != null 
                ? Ok(products) 
                : BadRequest("Failed to get products");
        }

        /// <summary>
        ///     Not implemented yet.
        /// </summary>
        /// <returns></returns>
        [HttpPost("UpdateProductById")]
        public IActionResult UpdateProductById()
        {
            return BadRequest("Not implemented");
        }

        /// <summary>
        ///     Deletes a product by id.
        /// </summary>
        /// <param name="id">Id of the product to delete</param>
        /// <returns>Http.Success or Http.BadRequest</returns>
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
