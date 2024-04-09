using Microsoft.EntityFrameworkCore;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Features.Item.Filters;
using WantToSell.Application.Models.Paging;
using WantToSell.Domain;
using WantToSell.Persistence.DbContexts;

namespace WantToSell.Persistence.Repositories;

public class ItemRepository : GenericRepository<Item>, IItemRepository
{
    public ItemRepository(WantToSellContext context) : base(context)
    {
    }

    public async Task<Item> GetByIdWithImages(Guid id)
    {
        return await _context.Items
            .AsNoTracking()
            .Include(s => s.StorageFiles)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<PagedList<Item>> GetFilteredListAsync(ItemFilter filter, Pager pager)
    {
        var query = _context.Items
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Subcategory)
            .AsQueryable();

        if (filter.MinPrice.HasValue)
            query = query.Where(x => x.Price >= filter.MinPrice);

        if (filter.MaxPrice.HasValue)
            query = query.Where(x => x.Price <= filter.MaxPrice.Value);

        if (!string.IsNullOrWhiteSpace(filter.Condition))
            query = query.Where(x => x.Condition.Contains(filter.Condition));

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(x => x.Name.Contains(filter.Name));

        if (!string.IsNullOrWhiteSpace(filter.CategoryName))
            query = query.Where(x => x.Category.Name.Contains(filter.CategoryName));

        if (!string.IsNullOrWhiteSpace(filter.SubcategoryName))
            query = query.Where(x => x.Subcategory.Name.Contains(filter.SubcategoryName));

        return await query
            .Sort(pager)
            .ToPagedListAsync(pager);
    }
}