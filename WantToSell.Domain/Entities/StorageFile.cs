using WantToSell.Domain.Shared;

namespace WantToSell.Domain;

public class StorageFile : Entity
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public string MimeType { get; set; }
    public long Size { get; set; }
    public string Hash { get; set; }
    public virtual Item Item { get; set; }
    public virtual Guid? ItemId { get; set; }
}