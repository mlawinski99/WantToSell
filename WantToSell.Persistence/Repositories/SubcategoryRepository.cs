using Microsoft.EntityFrameworkCore;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Domain;
using WantToSell.Persistence.DbContexts;

namespace WantToSell.Persistence.Repositories
{
	public class SubcategoryRepository : GenericRepository<Subcategory>, ISubcategoryRepository
	{
		public SubcategoryRepository(WantToSellContext context) : base(context)
		{
		}

		public async Task<List<Subcategory>> GetListByCategoryIdAsync(Guid categoryId)
		{
			return await _context.Subcategories
				.AsNoTracking()
				.Select(s => new Subcategory()
				{
					Id = s.Id,
					Name = s.Name,
					Category = new Category()
					{
						Name = s.Category.Name,
					}
				})
				.Where(s => s.CategoryId == categoryId)
				.ToListAsync();
		}
	}
}