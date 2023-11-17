using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
