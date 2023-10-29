using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WantToSell.Application.Models.Identity;

namespace WantToSell.Application.Contracts.Identity
{
	public interface IAuthService
	{
		Task<LoginResponse> Login(LoginRequest request);
		Task<RegistrationResponse> Register(RegistrationRequest request);
	}
}
