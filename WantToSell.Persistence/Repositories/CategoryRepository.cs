using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WantToSell.Application.Contracts.DataAccess;
using WantToSell.Domain;
using WantToSell.Persistence.DbContext;

namespace WantToSell.Persistence.Repositories
{
	public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
	{
		public CategoryRepository(WantToSellContext context) : base(context)
		{
		}
	}
}
