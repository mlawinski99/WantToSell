using Microsoft.AspNetCore.Http;
using WantToSell.Application.Models.Files;
using WantToSell.Domain;

namespace WantToSell.Application.Contracts.Storage;

public interface IFilesService
{
    Task<FileDetailModel> ReadFileAsync(string fileName);
    Task<IEnumerable<FileDetailModel>> ReadFilesAsync(IEnumerable<string> fileNames);
    Task<FileDetailModel> UploadFileAsync(IFormFile file);
    Task<IEnumerable<FileDetailModel>> UploadFilesAsync(IEnumerable<IFormFile> formFiles);
    void DeleteFile(string filePath);
    void DeleteFiles(IEnumerable<string> filePaths);
    Task UpdateFilesAsync(IEnumerable<FileDetailModel> existingFiles, IEnumerable<FileDetailModel> incomingFiles);
}