using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using WantToSell.Application.Contracts.Identity;
using WantToSell.Application.Models.Identity;
using WantToSell.Identity.Models;

namespace WantToSell.Identity.Services
{
	public class UserService
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public UserService(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}	
		public async Task<List<UserModel>> GetUsers()
		{
			var users = await _userManager.GetUsersInRoleAsync("user");
			return users.Select(x => new UserModel
			{
				Id = x.Id,
				Name = $"{x.FirstName} {x.LastName}",
				Email = x.Email,
				UserName = x.UserName
			}).ToList();
		}

		public async Task<UserModel> GetUser(Guid id)
		{
			var user = await _userManager.FindByIdAsync(id.ToString());
			return new UserModel
			{
				Id = user.Id,
				Name = $"{user.FirstName} {user.LastName}",
				Email = user.Email,
				UserName = user.UserName
			};
		}

		public Guid GetCurrentUserId()
		{
			var user = ClaimsPrincipal.Current;
			
			if (user != null && user.Identity.IsAuthenticated)
			{
				var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

				if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var guidUserId))
				{
					return guidUserId;
				}
			}

			return Guid.Empty;
		}
	}
}
