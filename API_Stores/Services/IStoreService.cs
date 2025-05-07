using API_Stores.Models;
using API_Stores.Models.Request;
using API_Stores.Models.Respone;

namespace API_Stores.Services
{
    public interface IStoreService
    {
        Task<IEnumerable<object>> GetStoresAsync();
        Task<IEnumerable<MRes_Store>> GetStoresCustomResponseAsync();
        Task<object?> GetStoreAsync(int id);
        Task<Store> CreateStoreAsync(MReq_Store reqStore);
        Task<bool> UpdateStoreAsync(int id, Store store);
        Task<Store?> UpdatePartialAsync(int id, MReq_Store reqStore);
        Task<bool> DeleteStoreAsync(int id);
        Task<IEnumerable<object>> GetStoreRevenuesAsync();
    }
}