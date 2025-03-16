using HR.LeaveManager.Application.Contracts.Identity;
using HR.LeaveManager.Application.Models.Identities;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("login")]
		public async Task<ActionResult<AuthResponse>> Login([FromBody] AuthRequest request)
		{
			return Ok(await _authService.Login(request));
		}

		[HttpPost("register")]
		public async Task<ActionResult<RegisterReponse>> Register([FromBody] RegisterRequest request)
		{
			return Ok(await _authService.Register(request));
		}
	}
}
