using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Domain.Shared;
using WantToSell.Persistence.DbContext;

namespace WantToSell.Persistence.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : Entity
	{
		private readonly WantToSellContext _context;

		public GenericRepository(WantToSellContext context)
		{
			_context = context;
		}
		public async Task<T> GetByIdAsync(Guid id)
		{
			return _context.Set<T>().AsNoTracking().FirstOrDefault(x => x.Id == id); 
		}

		public async Task<List<T>> GetListAsync()
		{
			return await _context.Set<T>().AsNoTracking().ToListAsync();
		}

		public async Task<T> CreateAsync(T entity)
		{
			await _context.AddAsync(entity);
			await _context.SaveChangesAsync();

			return entity;
		}

		public async Task<T> UpdateAsync(T entity)
		{
			_context.Update(entity);
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return entity;
		}

		public async Task DeleteAsync(T entity)
		{
			_context.Remove(entity);
			await _context.SaveChangesAsync();
		}
	}
}
