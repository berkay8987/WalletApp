using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wallet.Business.Abstract;
using Wallet.Core.Entitites.Models;
using Wallet.DataAccess.Abstract;

namespace Wallet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        ///     Creates a product with specified name and price.
        /// </summary>
        /// <param name="name">Name of the product</param>
        /// <param name="price">Price of the product</param>
        /// <returns>Returns the product created.</returns>
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(string name, decimal price)
        {
            var product = new Product { 
                Name = name, Price = price 
            };

            var productSuccess = await _productService.CreateProductAsync(product);
            return productSuccess != null
                ? Ok(productSuccess)
                : BadRequest("Failed to create a product");
        }

        /// <summary>
        ///     Returns all products in db
        /// </summary>
        /// <returns>List of products</returns>
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return products != null 
                ? Ok(products) 
                : BadRequest("Failed to get products");
        }

        /// <summary>
        ///     Update a product's price by id
        /// </summary>
        /// <param name="id">Id of the product to update</param>
        /// <param name="price">New price for the product</param>
        /// <returns>Updated product</returns>
        [HttpPut("UpdateProductById")]
        public async Task<IActionResult> UpdateProductById(int id, decimal price)
        {
            var product = await _productService.UpdateProduct(id, price);
            return product != null
                ? Ok(product)
                : BadRequest("Failed to update product.");
        }

        /// <summary>
        ///     Deletes a product by id.
        /// </summary>
        /// <param name="id">Id of the product to delete</param>
        /// <returns>Http.Success or Http.BadRequest</returns>
        [HttpDelete("DeleteProductById")]
        public async Task<IActionResult> DeleteProductById(int id)
        {
            var productSuccess = await _productService.DeleteProduct(id);
            return productSuccess != null
                ? Ok(new
                {
                    response = "Successfuly deleted product",
                    product = productSuccess
                })
                : BadRequest("Failed to delete product");
        }
    }
}
