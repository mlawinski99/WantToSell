using WantToSell.Application.Features.Category.Models;
using WantToSell.Domain.Mappers;

namespace WantToSell.Application.Mappers.Category;

public class CategoryViewModelMapper :
    IMapper<Domain.Category, CategoryViewModel>,
    IEnumerableMapper<Domain.Category, CategoryViewModel>
{
    public async Task<CategoryViewModel> Map(Domain.Category model, CategoryViewModel? result = default)
    {
        return new CategoryViewModel
        {
            Id = model.Id,
            Name = model.Name
        };
    }

    public async Task<IEnumerable<CategoryViewModel>> Map(IEnumerable<Domain.Category> entities)
    {
        var tasks = entities.Select(m => Map(m));
        var models = await Task.WhenAll(tasks);
        
        return models;
    }
}