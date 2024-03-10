using WantToSell.Application.Contracts.Storage;
using WantToSell.Application.Features.Items.Models;
using WantToSell.Application.Mappers.Files;
using WantToSell.Domain;
using WantToSell.Domain.Mappers;

namespace WantToSell.Application.Mappers.Items;

public class ItemUpdateModelMapper : IMapper<ItemUpdateModel, Item>
{
    public async Task<Item> Map(ItemUpdateModel model)
    {
        return new Item()
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            DateExpiredUtc = model.DateExpiredUtc,
            Condition = model.Condition,
            CategoryId = model.CategoryId,
            SubcategoryId = model.SubcategoryId,
        };
    }
}