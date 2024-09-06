using Microsoft.AspNetCore.Http;
using WantToSell.Application.Features.Items.Models;
using WantToSell.Application.Mappers.Files;
using WantToSell.Domain.Mappers;

namespace WantToSell.Application.Mappers.Items;

public class ItemListModelMapper :
    IEnumerableMapper<Domain.Item, ItemListModel>,
    IMapper<Domain.Item, ItemListModel>
{
    private readonly FileDetailMapper _fileDetailMapper;

    public ItemListModelMapper(FileDetailMapper fileDetailMapper)
    {
        _fileDetailMapper = fileDetailMapper;
    }

    public async Task<IEnumerable<ItemListModel>> Map(IEnumerable<Domain.Item> models)
    {
        var tasks = models.Select(m => Map(m));
        var entities = await Task.WhenAll(tasks);

        return entities;
    }

    public async Task<ItemListModel> Map(Domain.Item model, ItemListModel? listModel = null)
    {
        var fileDetailsList = await _fileDetailMapper.Map(model.StorageFiles);
        return new ItemListModel
        {
            Id = model.Id,
            Name = model.Name,
            Price = model.Price,
            Images = fileDetailsList.Any() ? fileDetailsList.Select(x => x.FormFile).ToList() : new List<IFormFile>()
        };
    }
}