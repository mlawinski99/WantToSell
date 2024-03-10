using WantToSell.Application.Models.Files;
using WantToSell.Domain;
using WantToSell.Domain.Mappers;

namespace WantToSell.Application.Mappers.Files;

public class StorageFileMapper : 
    IEnumerableMapper<FileDetailModel, StorageFile>,
    IMapper<FileDetailModel, StorageFile>
{
    
    public async Task<IEnumerable<StorageFile>> Map(IEnumerable<FileDetailModel> models)
    {
        var tasks = models.Select(Map);
        var entities = await Task.WhenAll(tasks);
        
        return entities;
    }
    
    public async Task<StorageFile> Map(FileDetailModel model)
    {
        return new StorageFile()
        {
            FileName = model.FileName,
            FilePath = model.FilePath,
            MimeType = model.MimeType,
            Size = model.Size,
            Hash = model.Hash,
        };
    }
}