using WantToSell.Application.Contracts.Persistence;
using WantToSell.Domain;
using WantToSell.Persistence.DbContext;

namespace WantToSell.Persistence.Repositories
{
	public class SubcategoryRepository : GenericRepository<Subcategory>, ISubcategoryRepository
	{
		public SubcategoryRepository(WantToSellContext context) : base(context)
		{
		}
	}
}