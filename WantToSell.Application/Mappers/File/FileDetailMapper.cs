using WantToSell.Application.Contracts.Storage;
using WantToSell.Application.Extensions;
using WantToSell.Application.Models.Files;
using WantToSell.Domain;
using WantToSell.Domain.Mappers;

namespace WantToSell.Application.Mappers.Files;

public class FileDetailMapper :
    IEnumerableMapper<StorageFile, FileDetailModel>,
    IMapper<StorageFile, FileDetailModel>
{
    private readonly IFilesService _filesService;

    public FileDetailMapper(IFilesService filesService)
    {
        _filesService = filesService;
    }

    public async Task<IEnumerable<FileDetailModel>> Map(IEnumerable<StorageFile> entities)
    {
        if (entities is null || entities.IsEmpty())
            return Enumerable.Empty<FileDetailModel>();

        var tasks = entities.Select(m => Map(m));
        var models = await Task.WhenAll(tasks);

        return models;
    }

    public async Task<FileDetailModel> Map(StorageFile entity, FileDetailModel? resultModel = null)
    {
        var image = await _filesService.ReadFileAsync(entity.FileName);

        return new FileDetailModel
        {
            Id = entity.Id,
            FileName = entity.FileName,
            FilePath = entity.FilePath,
            MimeType = entity.MimeType,
            Size = entity.Size,
            Hash = entity.Hash,
            FormFile = image.FormFile
        };
    }
}