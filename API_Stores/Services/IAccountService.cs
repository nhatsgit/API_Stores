using System.Threading.Tasks;
using API_Stores.Models;

namespace API_Stores.Services
{
    public interface IAccountService
    {
        Task<string> RegisterAsync(User user, string password);
        Task<string> LoginAsync(string email, string password);
    }
}
