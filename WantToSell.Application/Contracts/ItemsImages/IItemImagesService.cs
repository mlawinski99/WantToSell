using Microsoft.AspNetCore.Http;
using WantToSell.Application.Models.Files;

namespace WantToSell.Application.Services;

public interface IItemImagesService
{
    Task<IEnumerable<FileDetailModel>> UpdateStorage(IEnumerable<FileDetailModel> filesToDelete, IEnumerable<IFormFile> filesToAdd);

    Task UpdateDatabase(IEnumerable<FileDetailModel> filesToDelete, IEnumerable<FileDetailModel> filesToAdd, Guid itemId);

    IEnumerable<FileDetailModel> GetImagesToDelete(IEnumerable<FileDetailModel> existingFiles, Dictionary<IFormFile, string> newFiles);

    IEnumerable<IFormFile> GetImagesToAdd(IEnumerable<FileDetailModel> existingFiles, Dictionary<IFormFile, string> newFiles);

    Task<Dictionary<IFormFile, string>> GetFormFileHashPairs(IEnumerable<IFormFile> files);
}