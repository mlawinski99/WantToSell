using WantToSell.Application.Features.Address.Models;
using WantToSell.Domain;

namespace WantToSell.Application.Contracts.Persistence;

public interface IAddressRepository : IGenericRepository<Address>
{
    Task<Address> GetAddressForUser(Guid userId);
    bool IsExists(Guid userId);
}