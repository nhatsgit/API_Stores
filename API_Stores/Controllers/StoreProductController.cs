using Microsoft.AspNetCore.Mvc;
using API_Stores.Models.Request;
using API_Stores.Services;

namespace API_Stores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreProductController : ControllerBase
    {
        private readonly IStoreProductService _storeProductService;

        public StoreProductController(IStoreProductService storeProductService)
        {
            _storeProductService = storeProductService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStoreProducts()
        {
            var storeProducts = await _storeProductService.GetStoreProductsAsync();
            return Ok(storeProducts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStoreProductById(int id)
        {
            var storeProduct = await _storeProductService.GetStoreProductByIdAsync(id);
            if (storeProduct == null)
            {
                return NotFound(new { Message = "StoreProduct not found" });
            }
            return Ok(storeProduct);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStoreProduct([FromBody] MReq_StoreProduct reqStoreProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var storeProduct = await _storeProductService.CreateStoreProductAsync(reqStoreProduct);
            return CreatedAtAction(nameof(GetStoreProductById), new { id = storeProduct.Id }, storeProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStoreProduct(int id, [FromBody] MReq_StoreProduct reqStoreProduct)
        {
            var result = await _storeProductService.UpdateStoreProductAsync(id, reqStoreProduct);
            if (!result)
            {
                return NotFound(new { Message = "StoreProduct not found" });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoreProduct(int id)
        {
            var result = await _storeProductService.DeleteStoreProductAsync(id);
            if (!result)
            {
                return NotFound(new { Message = "StoreProduct not found" });
            }

            return NoContent();
        }
    }
}
