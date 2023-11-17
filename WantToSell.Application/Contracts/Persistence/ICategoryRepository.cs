using WantToSell.Domain;

namespace WantToSell.Application.Contracts.Persistence;

public interface ICategoryRepository : IGenericRepository<Category>
{
	bool IsCategoryExists(Guid id);
}