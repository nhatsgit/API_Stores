using System.Threading.Tasks;
using API_Stores.Models;
using API_Stores.Models.Request;
using API_Stores.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Stores.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] MReq_Register request)
        {
            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email
            };

            var result = await _accountService.RegisterAsync(user, request.Password);
            if (result == "User registered successfully.")
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Mreq_Login request)
        {
            var token = await _accountService.LoginAsync(request.UserName, request.Password);
            if (token == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(token);
        }
    }

   
}
