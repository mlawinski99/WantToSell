using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WantToSell.Application.Contracts.Identity;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Models.Identity;
using WantToSell.Domain.Settings;
using WantToSell.Identity.Models;


namespace WantToSell.Identity.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IOptions<TokenSettings> _settings;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AuthService(UserManager<ApplicationUser> userManager, 
			IOptions<TokenSettings> settings, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_settings = settings;
			_signInManager = signInManager;
		}
		public async Task<LoginResponse> Login(LoginRequest request)
		{
			var user = await _userManager.FindByNameAsync(request.UserName);

			if (user == null)
				throw new NotFoundException($"User with username {request.UserName} does not exists!");

			var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
			if (!result.Succeeded)
				throw new BadRequestException($"UserName or Password is wrong!");

			JwtSecurityToken token = await CreateToken(user);
			
			var response = new LoginResponse
			{
				Id = user.Id,
				Token = new JwtSecurityTokenHandler().WriteToken(token),
				Email = user.Email,
				UserName = user.UserName
			};

			return response;
		}

		public async Task<RegistrationResponse> Register(RegistrationRequest request)
		{
			var user = new ApplicationUser
			{
				UserName = request.UserName,
				Email = request.Email,
				FirstName = request.FirstName,
				LastName = request.LastName
			};

			var result = await _userManager.CreateAsync(user, request.Password);

			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, "User");
				return new RegistrationResponse { UserId = user.Id };
			}

			throw new BadRequestException($"{result.Errors}");
		}

		private async Task<JwtSecurityToken> CreateToken(ApplicationUser user)
		{
			var userClaims = await _userManager.GetClaimsAsync(user);
			var roles = await _userManager.GetRolesAsync(user);

			var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

			var claims = new[]
				{
					new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
					new Claim(JwtRegisteredClaimNames.Email, user.Email),
					new Claim("uid", user.Id),
				}
				.Union(userClaims)
				.Union(roleClaims);

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Value.Key));

			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _settings.Value.Issuer,
				audience: _settings.Value.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(_settings.Value.DurationMinutes),
				signingCredentials: credentials);

			return token;
		}
	}
}
