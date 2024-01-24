using WantToSell.Domain;

namespace WantToSell.Application.Contracts.Persistence;

public interface ISubcategoryRepository : IGenericRepository<Subcategory>
{
    Task<List<Subcategory>> GetListByCategoryIdAsync(Guid categoryId);
    bool IsSubcategoryNameExists(string name);
    bool IsSubcategoryExists(Guid subcategoryId);
}