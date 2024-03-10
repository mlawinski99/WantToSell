using Microsoft.AspNetCore.Http;
using WantToSell.Application.Contracts.Logging;
using WantToSell.Application.Contracts.Storage;
using WantToSell.Application.Extensions;
using WantToSell.Application.Helpers;
using WantToSell.Application.Models.Files;
using WantToSell.Domain.Settings;

namespace WantToSell.Storage.Services;

public class LocalStorageService :  IFilesService
{
    private static readonly string _storagePath = ConfigurationSettings.LocalStoragePath;
    private readonly IApplicationLogger<LocalStorageService> _logger;

    public LocalStorageService(IApplicationLogger<LocalStorageService> logger)
    {
        _logger = logger;
    }
    
    public async Task<FileDetailModel> ReadFileAsync(string fileName)
    {
        var filePath = Path.Combine(_storagePath, fileName);
        if (!File.Exists(filePath))
        {
            _logger.LogError($"File not found: {filePath}");
            return null;
        }

        var memoryStream = new MemoryStream();
        using (var stream = new FileStream(filePath, FileMode.Open, 
                   FileAccess.Read, FileShare.Read, 
                   bufferSize: 4096, useAsync: true))
        {
            await stream.CopyToAsync(memoryStream);
        }
        memoryStream.Position = 0;
        IFormFile formFile = new FormFile(memoryStream, 0,
            memoryStream.Length, fileName, filePath)
        {
            Headers = new HeaderDictionary(),
            ContentType = MimeTypeHelper.GetMimeType(fileName)
        };
        
        return new FileDetailModel
        {
            FileName = formFile.FileName,
            FilePath = filePath,
            MimeType = MimeTypeHelper.GetMimeType(formFile.FileName),
            Size = formFile.Length,
            Hash = await formFile.CalculateSHA256Async(),
            FormFile = formFile
        };
    }
    
    public async Task<IEnumerable<FileDetailModel>> ReadFilesAsync(IEnumerable<string> fileNames)
    {
        var tasks = fileNames.Select(async fileName => await ReadFileAsync(fileName));
        var files = await Task.WhenAll(tasks);

        return files.Where(file => file != null);
    }
    
    public async Task<FileDetailModel> UploadFileAsync(IFormFile file)
    {
        if (file.Length == 0) 
            return null;
        
        var uniqueFileName = file.FileName.AppendGuid();
        var filePath = Path.Combine(_storagePath, uniqueFileName);
        
        try
        {
            using (var stream = new FileStream(filePath, FileMode.Create,
                       FileAccess.Write, FileShare.None,
                       4096, useAsync: true))
            {
                await file.CopyToAsync(stream);
            }
            
            return new FileDetailModel
            {
                FileName = uniqueFileName,
                FilePath = filePath,
                MimeType = MimeTypeHelper.GetMimeType(file.FileName),
                Size = file.Length,
                Hash = await file.CalculateSHA256Async(),
                FormFile = file
            };
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occurred while uploading file", ex);
            throw;
        }
    }
    
    public async Task<IEnumerable<FileDetailModel>> UploadFilesAsync(IEnumerable<IFormFile> formFiles)
    {
        var tasks = formFiles.Select(async formFile => await UploadFileAsync(formFile));
        var files = await Task.WhenAll(tasks);

        return files.ToList();
    }
    public void DeleteFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred while deleting file at {filePath}", ex);
        }
    }
    
    public void DeleteFiles(IEnumerable<string> filePaths)
    {
        foreach(var path in filePaths)
            DeleteFile(path);
    }
    
    public async Task UpdateFilesAsync(IEnumerable<FileDetailModel> existingFiles, IEnumerable<FileDetailModel> incomingFiles)
    {
        var incomingFilesList = incomingFiles.ToList();

        var incomingFilesHashesTasks = incomingFilesList
            .Select(file => new
            {
                File = file.FormFile,
                HashTask = file.FormFile.CalculateSHA256Async()
            }).ToList();

        await Task.WhenAll(incomingFilesHashesTasks.Select(x => x.HashTask));

        var incomingFilesHashes = incomingFilesHashesTasks
            .ToDictionary(x => x.HashTask.Result, x => x.File);

        var filesToDelete = existingFiles
            .Where(ef => !incomingFilesHashes.ContainsKey(ef.Hash))
            .ToList();

        var filesToAdd = incomingFilesHashes.Where(
                ifh => 
                    !existingFiles.Any(ef => ef.Hash == ifh.Key))
            .Select(x => x.Value)
            .ToList();

        DeleteFiles(filesToDelete.Select(s => s.FormFile.FileName).ToList());
        await UploadFilesAsync(filesToAdd);
    }
}
