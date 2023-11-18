using WantToSell.Application.Models.Identity;

namespace WantToSell.Application.Contracts.Identity
{
	public interface IAuthService
	{
		Task<LoginResponse> Login(LoginRequest request);
		Task<RegistrationResponse> Register(RegistrationRequest request);
	}
}
