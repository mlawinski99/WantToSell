using WantToSell.Application.Features.Items.Models;
using WantToSell.Domain.Mappers;

namespace WantToSell.Application.Mappers.Items;

public sealed class ItemMapper : 
    IMapper<ItemUpdateModel, Domain.Item>,
    IMapper<ItemCreateModel, Domain.Item>
{
    public async Task<Domain.Item> Map(ItemCreateModel model, Domain.Item item)
    {
        item.Name = model.Name;
        item.Description = model.Description;
        item.DateExpiredUtc = model.DateExpiredUtc;
        item.Condition = model.Condition;
        item.CategoryId = model.CategoryId;
        item.SubcategoryId = model.SubcategoryId;

        return item;
    }
    public async Task<Domain.Item> Map(ItemUpdateModel model, Domain.Item item)
    {
        item.Id = model.Id;
        item.Name = model.Name;
        item.Description = model.Description;
        item.DateExpiredUtc = model.DateExpiredUtc;
        item.Condition = model.Condition;
        item.CategoryId = model.CategoryId;
        item.SubcategoryId = model.SubcategoryId;

        return item;
    }
}