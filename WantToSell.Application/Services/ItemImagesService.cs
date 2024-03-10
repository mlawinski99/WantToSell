using Microsoft.AspNetCore.Http;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Contracts.Storage;
using WantToSell.Application.Extensions;
using WantToSell.Application.Mappers.Files;
using WantToSell.Application.Models.Files;

namespace WantToSell.Application.Services;

public class ItemImagesService : IItemImagesService
{
    private readonly IFilesService _filesService;
    private readonly IStorageFileRepository _storageFileRepository;
    private readonly StorageFileMapper _storageFileMapper;

    public ItemImagesService(IFilesService filesService,
        IStorageFileRepository storageFileRepository)
    {
        _storageFileMapper = new StorageFileMapper();
        _filesService = filesService;
        _storageFileRepository = storageFileRepository;
    }
    public async Task<IEnumerable<FileDetailModel>> UpdateStorage(IEnumerable<FileDetailModel> filesToDelete,
        IEnumerable<IFormFile> filesToAdd)
    {
        _filesService.DeleteFiles(filesToDelete.Select(s => s.FilePath));
        return await _filesService.UploadFilesAsync(filesToAdd);
    }

    public async Task UpdateDatabase(IEnumerable<FileDetailModel> filesToDelete, 
        IEnumerable<FileDetailModel> filesToAdd, Guid itemId)
    {
        var newFiles = await _storageFileMapper.Map(filesToAdd);
        newFiles.SetProperty(s => s.ItemId = itemId);
        
        var deleteTask = _storageFileRepository.DeleteRangeAsync(filesToDelete.Select(s => s.Id));
        var createTask = _storageFileRepository.CreateRangeAsync(newFiles);

        await Task.WhenAll(deleteTask, createTask);
    }

    public IEnumerable<FileDetailModel> GetImagesToDelete(IEnumerable<FileDetailModel> existingFiles, 
        Dictionary<IFormFile, string> newFiles)
    {
        return existingFiles.Where(s => 
            newFiles
                .Select(s => s.Value)
                .NotContains(s.Hash));
    }

    public IEnumerable<IFormFile> GetImagesToAdd(IEnumerable<FileDetailModel> existingFiles, 
        Dictionary<IFormFile, string> newFiles)
    {
        var filesToAdd = newFiles
            .Where(s =>
                existingFiles
                    .Select(s => s.Hash)
                    .NotContains(s.Value));
        
        return filesToAdd
            .Select(s => s.Key);
    }

    public async Task<Dictionary<IFormFile, string>> GetFormFileHashPairs(IEnumerable<IFormFile> files)
    {
        var hashTasks = files
            .Select(async file => new 
            { 
                File = file, 
                Hash = await file.CalculateSHA256Async() 
            }).ToList();

        var results = await Task.WhenAll(hashTasks);
        var fileHashPairs = results.ToDictionary(result => result.File, result => result.Hash);

        return fileHashPairs;
    }
}