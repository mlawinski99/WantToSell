using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WantToSell.Application.Contracts.Identity;
using WantToSell.Application.Models.Identity;

namespace WantToSell.Api.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IAuthService _authenticationService;

		public AuthenticationController(IAuthService authenticationService)
		{
			_authenticationService = authenticationService;
		}

		[HttpPost("login")]
		public async Task<ActionResult<LoginResponse>> Login(LoginRequest loginRequest)
		{
			return Ok(await _authenticationService.Login(loginRequest));
		}

		[HttpPost("register")]
		public async Task<ActionResult<LoginResponse>> Register(RegistrationRequest registrationRequest)
		{
			return Ok(await _authenticationService.Register(registrationRequest));
		}
	}
}
