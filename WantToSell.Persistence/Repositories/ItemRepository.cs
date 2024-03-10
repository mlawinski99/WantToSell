using Microsoft.EntityFrameworkCore;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Domain;
using WantToSell.Persistence.DbContexts;

namespace WantToSell.Persistence.Repositories
{
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
	}
}
