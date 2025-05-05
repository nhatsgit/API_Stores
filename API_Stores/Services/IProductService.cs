using Microsoft.AspNetCore.Mvc;
using API_Stores.Models;
using API_Stores.Models.Request;
using API_Stores.Models.Respone;

namespace API_Stores.Services
{
    public interface IProductService
    {
        Task<IEnumerable<object>> GetProductsAsync();
        Task<IEnumerable<MRes_Product>> GetProductsCustomResponeAsync();
        Task<IEnumerable<object>> GetListAsync(string? keyword, int? categoryId);
        Task<object> GetListPagingAsync(int page, int pageSize);
        Task<object?> GetProductAsync(int id);
        Task<bool> UpdateProductAsync(int id, Product product);
        Task<Product?> UpdatePartialAsync(int id, MReq_Product reqProduct);
        Task<List<Product>> UpdateRangeAsync(List<MReq_Product> reqProducts);
        Task<Product> CreateProductAsync(MReq_Product reqProduct);
        Task<List<Product>> AddRangeAsync(List<MReq_Product> reqProducts);
        Task<bool> DeleteAsync(int id);
    }
}
