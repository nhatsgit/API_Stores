using API_Stores.Models;
using API_Stores.Models.Request;
using API_Stores.Models.Respone;

namespace API_Stores.Services
{
    public interface IStoreProductService
    {
        Task<IEnumerable<MRes_StoreProduct>> GetStoreProductsAsync();
        Task<MRes_StoreProduct?> GetStoreProductByIdAsync(int id);
        Task<StoreProduct> CreateStoreProductAsync(MReq_StoreProduct reqStoreProduct);
        Task<bool> UpdateStoreProductAsync(int id, MReq_StoreProduct reqStoreProduct);
        Task<bool> DeleteStoreProductAsync(int id);
        
    }
}
