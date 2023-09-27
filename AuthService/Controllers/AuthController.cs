using AuthService.Models;
using AuthService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Login user, string role)
        {
            var loginResult = _authRepository.GenerateAuthToken(user, role);

            return loginResult is null ? Unauthorized() : Ok(loginResult);
        }
    }
}
