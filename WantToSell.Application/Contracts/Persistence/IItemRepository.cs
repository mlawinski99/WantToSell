using WantToSell.Domain;

namespace WantToSell.Application.Contracts.Persistence;

public interface IItemRepository : IGenericRepository<Item>
{
    Task<Item> GetByIdWithImages(Guid id);
}