using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using WantToSell.Application.Contracts.Identity;
using WantToSell.Application.Models.Identity;
using WantToSell.Identity.Models;

namespace WantToSell.Identity.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
		{
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
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
			var userId = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(claim => claim.Type == "jti")?.Value;
			return Guid.Parse(userId);
		}
	}
}
