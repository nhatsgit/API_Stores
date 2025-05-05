using Microsoft.EntityFrameworkCore;
using API_Stores.Models;
using API_Stores.Models.Request;
using API_Stores.Models.Respone;

namespace API_Stores.Services
{
    public class S_Store : IStoreService
    {
        private readonly StoresDbContext _context;

        public S_Store(StoresDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> GetStoresAsync()
        {
            return await _context.Stores
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    s.Address,
                    s.PhoneNumber,
                    s.Description,
                    s.CreatedAt,
                    s.UpdateAt
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<MRes_Store>> GetStoresCustomResponseAsync()
        {
            return await _context.Stores
                .Select(s => new MRes_Store
                {
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address,
                    PhoneNumber = s.PhoneNumber,
                    Description = s.Description
                })
                .ToListAsync();
        }

        public async Task<object?> GetStoreAsync(int id)
        {
            return await _context.Stores
                .Where(s => s.Id == id)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    s.Address,
                    s.PhoneNumber,
                    s.Description,
                    s.CreatedAt,
                    s.UpdateAt
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Store> CreateStoreAsync(MReq_Store reqStore)
        {
            var store = new Store
            {
                Name = reqStore.Name!,
                Address = reqStore.Address!,
                PhoneNumber = reqStore.PhoneNumber!,
                Description = reqStore.Description,
                CreatedAt = DateTime.UtcNow
            };

            _context.Stores.Add(store);
            await _context.SaveChangesAsync();

            return store;
        }

        public async Task<bool> UpdateStoreAsync(int id, Store store)
        {
            if (id != store.Id) return false;

            _context.Entry(store).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Stores.Any(e => e.Id == id))
                    return false;
                else
                    throw;
            }
        }

        public async Task<Store?> UpdatePartialAsync(int id, MReq_Store reqStore)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null) return null;

            if (!string.IsNullOrEmpty(reqStore.Name))
                store.Name = reqStore.Name;
            if (!string.IsNullOrEmpty(reqStore.Address))
                store.Address = reqStore.Address;
            if (!string.IsNullOrEmpty(reqStore.PhoneNumber))
                store.PhoneNumber = reqStore.PhoneNumber;
            if (!string.IsNullOrEmpty(reqStore.Description))
                store.Description = reqStore.Description;

            store.UpdateAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return store;
        }

        public async Task<bool> DeleteStoreAsync(int id)
        {
            var store = await _context.Stores
                .Include(s => s.Employees)
                .Include(s => s.Invoices)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (store == null) return false;

            if (store.Employees.Any() || store.Invoices.Any())
                throw new InvalidOperationException("Store is referenced by Employees or Invoices.");

            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
