using WantToSell.Domain;

namespace WantToSell.Application.Contracts.Persistence;

public interface IStorageFileRepository : IGenericRepository<StorageFile>
{
    Task CreateRangeAsync(IEnumerable<StorageFile> entities);
    Task<IEnumerable<string>> GetItemFilesPaths(Guid itemId);
    Task<IEnumerable<StorageFile>> GetItemFiles(Guid itemId);
    Task DeleteRangeAsync(IEnumerable<Guid> entitiesIds);
}