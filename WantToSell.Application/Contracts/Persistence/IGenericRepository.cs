using WantToSell.Domain.Shared;

namespace WantToSell.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : Entity
    {
	    Task<T> GetByIdAsync(Guid id);
	    Task<List<T>> GetListAsync();
		Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}