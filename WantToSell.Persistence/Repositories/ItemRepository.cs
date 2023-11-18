using WantToSell.Application.Contracts.Persistence;
using WantToSell.Domain;
using WantToSell.Persistence.DbContext;

namespace WantToSell.Persistence.Repositories
{
	public class ItemRepository : GenericRepository<Item>, IItemRepository
	{
		public ItemRepository(WantToSellContext context) : base(context)
		{
		}
	}
}
