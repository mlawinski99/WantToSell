using WantToSell.Domain;

namespace WantToSell.Application.Contracts.Persistence;

public interface IAddressRepository : IGenericRepository<Address>
{
    Task<Address> GetAddressByUserId(Guid userId);
    bool IsExists(Guid userId);
}