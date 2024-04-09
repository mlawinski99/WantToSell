using WantToSell.Application.Features.Subcategory.Models;
using WantToSell.Domain.Mappers;

namespace WantToSell.Application.Mappers.Subcategory;

public class SubcategoryViewModelMapper :
    IMapper<Domain.Subcategory, SubcategoryViewModel>,
    IEnumerableMapper<Domain.Subcategory, SubcategoryViewModel>
{
    public async Task<SubcategoryViewModel> Map(Domain.Subcategory model, SubcategoryViewModel? result = default)
    {
        return new SubcategoryViewModel
        {
            Id = model.Id,
            Name = model.Name,
            CategoryName = model.Category.Name
        };
    }

    public async Task<IEnumerable<SubcategoryViewModel>> Map(IEnumerable<Domain.Subcategory> entities)
    {
        var tasks = entities.Select(m => Map(m));
        var models = await Task.WhenAll(tasks);
        
        return models;
    }
}