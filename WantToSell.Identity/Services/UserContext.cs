using WantToSell.Domain.Interfaces;

namespace WantToSell.Identity.Services;

public class UserContext : IUserContext
{
    public Guid UserId { get; set; }
}