using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_Stores.Models;
using API_Stores.Models.Request;
using API_Stores.Services;

namespace API_Stores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStores()
        {
            var stores = await _storeService.GetStoresAsync();
            return Ok(stores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStore(int id)
        {
            var store = await _storeService.GetStoreAsync(id);
            if (store == null)
            {
                return NotFound(new { Message = "Store not found" });
            }
            return Ok(store);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStore([FromBody] MReq_Store reqStore)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var store = await _storeService.CreateStoreAsync(reqStore);
            return CreatedAtAction(nameof(GetStore), new { id = store.Id }, store);
        }

        

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePartialStore(int id, [FromBody] MReq_Store reqStore)
        {
            var store = await _storeService.UpdatePartialAsync(id, reqStore);
            if (store == null)
            {
                return NotFound(new { Message = "Store not found" });
            }

            return Ok(store);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            try
            {
                var result = await _storeService.DeleteStoreAsync(id);
                if (!result)
                {
                    return NotFound(new { Message = "Store not found" });
                }

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
