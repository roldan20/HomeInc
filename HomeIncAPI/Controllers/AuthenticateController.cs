using HomeInc.Domain.DTOS;
using HomeInc.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HomeInc.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserService _authService;
        public AuthenticateController(IUserService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.Authenticate(request.UserName, request.Password);

            if (token == null)

                return Unauthorized();

            return Ok(new { token = token });
        }
    }
}
