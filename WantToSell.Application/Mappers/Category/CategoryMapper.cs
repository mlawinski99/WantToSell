using WantToSell.Application.Features.Category.Models;
using WantToSell.Domain.Mappers;

namespace WantToSell.Application.Mappers.Category;

public class CategoryMapper :
    IMapper<CategoryCreateModel, Domain.Category>,
    IMapper<CategoryUpdateModel, Domain.Category>
{
    public async Task<Domain.Category> Map(CategoryCreateModel model, Domain.Category category = null)
    {
        return new Domain.Category
        {
            Name = model.Name
        };
    }

    public async Task<Domain.Category> Map(CategoryUpdateModel model, Domain.Category category)
    {
        category.Id = category.Id;
        category.Name = model.Name;

        return category;
    }
}