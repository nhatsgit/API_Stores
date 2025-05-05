using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Stores.Models;
using API_Stores.Models.Request;
using API_Stores.Services;

namespace API_Stores.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _service.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("GetProductsCustomRespone")]
        public async Task<IActionResult> GetProductsCustomRespone()
        {
            var products = await _service.GetProductsCustomResponeAsync();
            return Ok(products);
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetList([FromQuery] string? keyword, [FromQuery] int? categoryId)
        {
            var products = await _service.GetListAsync(keyword, categoryId);
            return Ok(products);
        }

        [HttpGet("ListPaging")]
        public async Task<IActionResult> GetListPaging([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetListPagingAsync(page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _service.GetProductAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            var updated = await _service.UpdateProductAsync(id, product);
            if (!updated)
                return BadRequest();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePartial(int id,  MReq_Product data)
        {
            var product = await _service.UpdatePartialAsync(id, data);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPut("UpdateRange")]
        public async Task<IActionResult> UpdateRange(List<Product> products)
        {
            var updatedProducts = await _service.UpdateRangeAsync(products);
            return Ok(updatedProducts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(MReq_Product product)
        {
            var createdProduct = await _service.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPost("AddRange")]
        public async Task<IActionResult> AddRange(List<MReq_Product> products)
        {
            var addedProducts = await _service.AddRangeAsync(products);
            return Ok(addedProducts);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                    return NotFound();
                return Ok("Deleted");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
