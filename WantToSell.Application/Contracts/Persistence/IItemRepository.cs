using WantToSell.Application.Features.Item.Filters;
using WantToSell.Application.Models.Paging;
using WantToSell.Domain;

namespace WantToSell.Application.Contracts.Persistence;

public interface IItemRepository : IGenericRepository<Item>
{
    Task<Item> GetByIdWithDetails(Guid id);
    Task<PagedList<Item>> GetFilteredListAsync(ItemFilter filter, Pager pager);
}