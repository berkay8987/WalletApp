using Microsoft.AspNetCore.Mvc;

namespace Wallet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct()
        {
            return BadRequest("Not implemented");
        }

        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            return BadRequest("Not implemented");
        }

        [HttpPost("UpdateProductById")]
        public IActionResult UpdateProductById()
        {
            return BadRequest("Not implemented");
        }

        [HttpPost("DeleteProductById")]
        public IActionResult DeleteProductById()
        {
            return BadRequest("Not implemented");
        }
    }
}
