using Microsoft.EntityFrameworkCore;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Domain;
using WantToSell.Persistence.DbContexts;

namespace WantToSell.Persistence.Repositories;

public class StorageFileRepository : GenericRepository<StorageFile>, IStorageFileRepository
{
    public StorageFileRepository(WantToSellContext context) : base(context)
    {
    }
    
    public async Task CreateRangeAsync(IEnumerable<StorageFile> entities)
    {
        await _context.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<string>> GetItemFilesPaths(Guid itemId)
    {
        return await _context.StorageFiles
            .Where(x => x.ItemId == itemId)
            .Select(x => x.FilePath)
            .ToListAsync();
    }

    public async Task<IEnumerable<StorageFile>> GetItemFiles(Guid itemId)
    {
        return await _context.StorageFiles
            .Where(x => x.ItemId == itemId)
            .ToListAsync();
    }

    public async Task DeleteRangeAsync(IEnumerable<Guid> entitiesIds)
    {
        await _context.StorageFiles
            .Where(s => entitiesIds.Contains(s.Id))
            .ExecuteDeleteAsync();
    }
}