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
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API_Stores.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        
        public ProductController(IProductService service)
        {
            _productService = service;
            
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("GetProductsCustomRespone")]
        public async Task<IActionResult> GetProductsCustomRespone()
        {
            var products = await _productService.GetProductsCustomResponeAsync();
            return Ok(products);
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetList([FromQuery] string? keyword, [FromQuery] int? categoryId)
        {
            var products = await _productService.GetListAsync(keyword, categoryId);
            return Ok(products);
        }

        [HttpGet("ListPaging")]
        public async Task<IActionResult> GetListPaging([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _productService.GetListPagingAsync(page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProductAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePartial(int id, MReq_Product data)
        {
            var product = await _productService.UpdatePartialAsync(id, data);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPut("UpdateRange")]
        public async Task<IActionResult> UpdateRange(List<MReq_Product> products)
        {
            var updatedProducts = await _productService.UpdateRangeAsync(products);
            return Ok(updatedProducts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(MReq_Product product)
        {
            var createdProduct = await _productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPost("AddRange")]
        public async Task<IActionResult> AddRange(List<MReq_Product> products)
        {
            var addedProducts = await _productService.AddRangeAsync(products);
            return Ok(addedProducts);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _productService.DeleteAsync(id);
                if (!deleted)
                    return NotFound();
                return Ok("Deleted");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ByStore/{storeId}")]
        public async Task<IActionResult> GetProductsByStore(int storeId)
        {
            var products = await _productService.GetProductsByStoreAsync(storeId);
            if (!products.Any())
                return NotFound($"No products found for store with ID {storeId}.");
            return Ok(products);
        }

        [HttpGet("sp_ProductAndCategory")]
        public async Task<IActionResult> GetProductAndCategory()
        {
            var productsAndCategories=await _productService.GetProductAndCategoryAsync();
            return Ok(productsAndCategories);
        }
        [HttpGet("sp_SearchProduct")]
        public async Task<IActionResult> SearchProduct(string keyword)
        {
            var productsAndCategories = await _productService.SearchProductAsync(keyword);
            return Ok(productsAndCategories);
        }


    }
}
