using NewWebshopAPI.DTOs.UserDTOs;

namespace NewWebshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductAsync()
        {
            try
            {
                List<ProductResponse> products = await _productService.GetAllProductsAsync();

                if (products.Count == 0)
                {
                    return NoContent();
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("Product/{productId}")]
        public async Task<IActionResult> FindProductByIdAsync([FromRoute] int productId)
        {
            try
            {
                ProductResponse product = await _productService.FindProductByIdAsync(productId);

                if (product is null)
                {
                    return NotFound();
                }

                return Ok(product);

            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{productType}")]
        public async Task<IActionResult> FindProductByTypeAsync([FromRoute] string productType)
        {
            try
            {
                List<ProductResponse> productResponse = await _productService.FindProductByTypeAsync(productType);

                if (productResponse == null)
                {
                    return NotFound();
                }

                return Ok(productResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductRequest newProduct)
        {
            try
            {
                ProductResponse productResponse = await _productService.CreateProductAsync(newProduct);
                return Ok(productResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{productId}")]
        public async Task<IActionResult> UpdateProductByIdAsync([FromRoute] int productId, [FromBody] ProductRequest updateProduct)
        {
            try
            {
                var productResponse = await _productService.UpdateProductByIdAsync(productId, updateProduct);

                if (productResponse == null)
                {
                    return NotFound();
                }

                return Ok(productResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{productId}")]
        public async Task<IActionResult> DeleteProductByIdAsync([FromRoute] int productId)
        {
            try
            {
                var product = await _productService.DeleteProductByIdAsync(productId);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

    }
}
