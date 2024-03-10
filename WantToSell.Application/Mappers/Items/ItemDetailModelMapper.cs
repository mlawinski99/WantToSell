using WantToSell.Application.Contracts.Storage;
using WantToSell.Application.Features.Items.Models;
using WantToSell.Domain;
using WantToSell.Domain.Mappers;

namespace WantToSell.Application.Mappers.Items;

public class ItemDetailModelMapper : IMapper<Item, ItemDetailModel>
{
    private readonly IFilesService _filesService;

    public ItemDetailModelMapper(IFilesService filesService)
    {
        _filesService = filesService;
    }
    
    public async Task<ItemDetailModel> Map(Item entity)
    {
        var images = await _filesService.ReadFilesAsync(entity.StorageFiles.Select(s => s.FileName));
        
       return new ItemDetailModel()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            DateExpiredUtc = entity.DateExpiredUtc,
            Condition = entity.Condition,
            CategoryId = entity.CategoryId,
            SubcategoryId = entity.SubcategoryId,
            Images = images.Select(s => s.FormFile).ToList(),
        };
    }
}