using Lab09_MuñozHerrera.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab09_MuñozHerrera.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products/price-greater-than?price=20
        [HttpGet("price-greater-than")]
        public async Task<IActionResult> GetProductsByPrice([FromQuery] decimal price)
        {
            var products = await _productService.GetProductsByPriceGreaterThanAsync(price);
            return Ok(products);
        }

        // GET: api/products/most-expensive
        [HttpGet("most-expensive")]
        public async Task<IActionResult> GetMostExpensiveProduct()
        {
            var product = await _productService.GetMostExpensiveProductAsync();
            if (product == null)
            {
                return NotFound("No se encontraron productos en la base de datos.");
            }
            return Ok(product);
        }

        // GET: api/products/average-price
        [HttpGet("average-price")]
        public async Task<IActionResult> GetAverageProductPrice()
        {
            var averagePrice = await _productService.GetAverageProductPriceAsync();
            return Ok(new { averagePrice = averagePrice });
        }

        // GET: api/products/without-description
        [HttpGet("without-description")]
        public async Task<IActionResult> GetProductsWithoutDescription()
        {
            var products = await _productService.GetProductsWithoutDescriptionAsync();
            return Ok(products);
        }

        // GET: api/products/by-client/1
        [HttpGet("by-client/{clientId}")]
        public async Task<IActionResult> GetProductsByClient(int clientId)
        {
            var products = await _productService.GetProductsSoldToClientAsync(clientId);
            return Ok(products);
        }
    }
}