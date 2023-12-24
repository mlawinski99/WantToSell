using WantToSell.Application.Models.Identity;

namespace WantToSell.Application.Contracts.Identity
{
	public interface IUserService
	{
		Task<List<UserModel>> GetUsers();
		Task<UserModel> GetUser(Guid id);
	}
}
