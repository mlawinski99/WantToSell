using WantToSell.Application.Contracts.Persistence;
using WantToSell.Domain;
using WantToSell.Persistence.DbContexts;

namespace WantToSell.Persistence.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(WantToSellContext context) : base(context)
    {
    }

    public bool IsCategoryExists(Guid id)
    {
        return _context.Categories
            .Any(s => s.Id == id);
    }

    public bool IsCategoryNameExists(string name)
    {
        return _context.Categories
            .Any(s => s.Name == name);
    }
}