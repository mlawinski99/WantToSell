using WantToSell.Application.Contracts.Identity;

namespace WantToSell.Identity.Services;

public class UserContext : IUserContext
{
    public Guid UserId { get; set; }
}