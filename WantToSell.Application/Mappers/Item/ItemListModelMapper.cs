using WantToSell.Application.Features.Items.Models;
using WantToSell.Domain.Mappers;

namespace WantToSell.Application.Mappers.Items;

public class ItemListModelMapper : 
    IEnumerableMapper<Domain.Item, ItemListModel>,
    IMapper<Domain.Item, ItemListModel>
{
    
    public async Task<IEnumerable<ItemListModel>> Map(IEnumerable<Domain.Item> models)
    {
        var tasks = models.Select(m => Map(m));
        var entities = await Task.WhenAll(tasks);
        
        return entities;
    }
    
    public async Task<ItemListModel> Map(Domain.Item model, ItemListModel? listModel = null)
    {
        return new ItemListModel
        {
            Id = model.Id,
            Name = model.Name
        };
    }
}