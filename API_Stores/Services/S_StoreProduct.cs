using Microsoft.EntityFrameworkCore;
using API_Stores.Models;
using API_Stores.Models.Request;
using API_Stores.Models.Respone;

namespace API_Stores.Services
{
    public class S_StoreProduct : IStoreProductService
    {
        private readonly StoresDbContext _context;

        public S_StoreProduct(StoresDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MRes_StoreProduct>> GetStoreProductsAsync()
        {
            return await _context.StoreProducts
                .Include(sp => sp.Store)
                .Include(sp => sp.Product)
                .Select(sp => new MRes_StoreProduct
                {
                    Id = sp.Id,
                    StoreId = sp.StoreId,
                    StoreName = sp.Store!.Name,
                    ProductId = sp.ProductId,
                    ProductName = sp.Product!.Name,
                    Price = sp.Price,
                    Quantity = sp.Quantity,
                    CreatedAt = sp.CreatedAt,
                    UpdateAt = sp.UpdateAt
                })
                .ToListAsync();
        }

        public async Task<MRes_StoreProduct?> GetStoreProductByIdAsync(int id)
        {
            return await _context.StoreProducts
                .Include(sp => sp.Store)
                .Include(sp => sp.Product)
                .Where(sp => sp.Id == id)
                .Select(sp => new MRes_StoreProduct
                {
                    Id = sp.Id,
                    StoreId = sp.StoreId,
                    StoreName = sp.Store!.Name,
                    ProductId = sp.ProductId,
                    ProductName = sp.Product!.Name,
                    Price = sp.Price,
                    Quantity = sp.Quantity,
                    CreatedAt = sp.CreatedAt,
                    UpdateAt = sp.UpdateAt
                })
                .FirstOrDefaultAsync();
        }

        public async Task<StoreProduct> CreateStoreProductAsync(MReq_StoreProduct reqStoreProduct)
        {
            var storeProduct = new StoreProduct
            {
                StoreId = reqStoreProduct.StoreId,
                ProductId = reqStoreProduct.ProductId,
                Price = reqStoreProduct.Price,
                Quantity = reqStoreProduct.Quantity,
                CreatedAt = DateTime.UtcNow
            };

            _context.StoreProducts.Add(storeProduct);
            await _context.SaveChangesAsync();

            return storeProduct;
        }

        public async Task<bool> UpdateStoreProductAsync(int id, MReq_StoreProduct reqStoreProduct)
        {
            var storeProduct = await _context.StoreProducts.FindAsync(id);
            if (storeProduct == null) return false;

            storeProduct.StoreId = reqStoreProduct.StoreId ?? storeProduct.StoreId;
            storeProduct.ProductId = reqStoreProduct.ProductId ?? storeProduct.ProductId;
            storeProduct.Price = reqStoreProduct.Price ?? storeProduct.Price;
            storeProduct.Quantity = reqStoreProduct.Quantity ?? storeProduct.Quantity;
            storeProduct.UpdateAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteStoreProductAsync(int id)
        {
            var storeProduct = await _context.StoreProducts.FindAsync(id);
            if (storeProduct == null) return false;

            _context.StoreProducts.Remove(storeProduct);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
