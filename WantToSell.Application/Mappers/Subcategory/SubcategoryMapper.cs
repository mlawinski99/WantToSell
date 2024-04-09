using WantToSell.Application.Features.Subcategory.Models;
using WantToSell.Domain.Mappers;

namespace WantToSell.Application.Mappers.Subcategory;

public class SubcategoryMapper :
    IMapper<SubcategoryCreateModel, Domain.Subcategory>,
    IMapper<SubcategoryUpdateModel, Domain.Subcategory>
{
    public async Task<Domain.Subcategory> Map(SubcategoryCreateModel model, Domain.Subcategory entity)
    {
        entity.CategoryId = model.CategoryId;
        entity.Name = model.Name;

        return entity;
    }

    public async Task<Domain.Subcategory> Map(SubcategoryUpdateModel model, Domain.Subcategory entity)
    {
        entity.Id = model.Id;
        entity.Name = model.Name;
        entity.CategoryId = model.CategoryId;

        return entity;
    }
}