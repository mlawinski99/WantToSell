using Microsoft.AspNetCore.Http;

namespace WantToSell.Application.Models.Files;

public class FileDetailModel
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public string MimeType { get; set; }
    public long Size { get; set; }
    public string Hash { get; set; }
    public IFormFile FormFile { get; set; }
}