using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Service.ViewModels;

namespace AutenticacionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProduct _product;

        public ProductController(IProduct product)
        {
            _product = product;
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> createProduct([FromBody] CreateProductViewModel productView)
        {
            var result = await _product.CreateProductAsync(productView);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }

        [Authorize]
        [HttpGet("GetProductsAll")]
        public async Task<IActionResult> getProductsAll(int limit)
        {
            var listProducts = await _product.GetProductsAll(limit);
            return Ok(listProducts);
        }

        [Authorize]
        [HttpGet("GetProductsByUser")]
        public async Task<IActionResult> getProductsByUser(string userId)
        {
            var listProducts = await _product.GetProductsByUserId(userId);
            return Ok(listProducts);
        }
    }
}
