using Microsoft.EntityFrameworkCore;
using API_Stores.Models.Request;
using API_Stores.Models.Respone;
using API_Stores.Models;
using API_Stores.ExtensionMethods;
using Azure.Core;

namespace API_Stores.Services
{
    public class S_Product : IProductService
    {
        private readonly StoresDbContext _context;

        public S_Product(StoresDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> GetProductsAsync()
        {
            return await _context.Products
                
                .Include(p => p.Category)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    
                    p.Description,
                    p.ImgUrl,
                    
                    Category = new { p.Category.Name }
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<MRes_Product>> GetProductsCustomResponeAsync()
        {
            return await _context.Products
                
                .Include(p => p.Category)
                .Select(p => new MRes_Product
                {
                    Id = p.Id,
                    Ten = p.Name,
                    
                    MoTa = p.Description,
                    
                    Loai = p.Category.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> GetListAsync(string? keyword, int? categoryId)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(p => p.Name.Contains(keyword));
            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId);

            return await query
                .Include(p => p.Category)
                
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Description,
                    p.ImgUrl,
                    
                    Category = new { p.Category.Name }
                })
                .ToListAsync();
        }

        public async Task<object> GetListPagingAsync(int page, int pageSize)
        {
            var products = await _context.Products
                .OrderByDescending(p => p.CreatedAt)
                .Paginate(page,pageSize)
                .Select(p => new MRes_Product
                {
                    Id = p.Id,
                    Ten = p.Name,

                    MoTa = p.Description,

                    Loai = p.Category.Name
                })
                .ToListAsync();

            return new
            {
                Page = page,
                PageSize = pageSize,
                Items = products
            };
        }

        public async Task<object?> GetProductAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    p.Id,
                    p.Name,

                    p.Description,
                    
                    
                    Category = new { p.Category.Name }
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateProductAsync(int id, Product product)
        {
            if (id != product.Id) return false;

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(e => e.Id == id))
                    return false;
                else
                    throw;
            }
        }

        public async Task<Product?> UpdatePartialAsync(int id, MReq_Product reqProduct)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;


            if (!string.IsNullOrEmpty(reqProduct.Name))
                product.Name = reqProduct.Name;
            if (!string.IsNullOrEmpty(reqProduct.Description))
                product.Description = reqProduct.Description;
            if (!string.IsNullOrEmpty(reqProduct.ImgUrl))
                product.ImgUrl = reqProduct.ImgUrl;

            product.UpdateAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> UpdateRangeAsync(List<MReq_Product> reqProducts)
        {
            var updatedProducts = new List<Product>();

            foreach (var req in reqProducts)
            {
                var product = await _context.Products.FindAsync(req.Id);
                if (product == null)
                    continue; 

                if (!string.IsNullOrEmpty(req.Name))
                    product.Name = req.Name;
                if (!string.IsNullOrEmpty(req.Description))
                    product.Description = req.Description;
                if (!string.IsNullOrEmpty(req.ImgUrl))
                    product.ImgUrl = req.ImgUrl;

                product.UpdateAt = DateTime.UtcNow;
                updatedProducts.Add(product);
            }

            await _context.SaveChangesAsync();
            return updatedProducts;
        }


        public async Task<Product> CreateProductAsync(MReq_Product reqProduct)
        {
            var product = new Product
            {
                Name = reqProduct.Name!,
                Description = reqProduct.Description,
                ImgUrl = reqProduct.ImgUrl,
                CategoryId = reqProduct.CategoryId,
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<List<Product>> AddRangeAsync(List<MReq_Product> reqProducts)
        {
            var newProducts = new List<Product>();

            foreach (var req in reqProducts)
            {               
                var product = new Product
                {
                    Name = req.Name,
                    Description = req.Description,
                    ImgUrl = req.ImgUrl,
                    CategoryId = req.CategoryId,
                };

                newProducts.Add(product);
            }

            _context.Products.AddRange(newProducts);
            await _context.SaveChangesAsync();
            return newProducts;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.InvoiceDetails)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return false;

            bool isReferenced = await _context.InvoiceDetails.AnyAsync(i => i.ProductId == id);
            if (isReferenced)
                throw new InvalidOperationException("InvoiceDetails Foreign Key");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
