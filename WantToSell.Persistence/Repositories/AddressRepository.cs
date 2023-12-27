using Microsoft.EntityFrameworkCore;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Features.Address.Models;
using WantToSell.Domain;
using WantToSell.Persistence.DbContexts;

namespace WantToSell.Persistence.Repositories
{
	public class AddressRepository : GenericRepository<Address>, IAddressRepository
	{
		public AddressRepository(WantToSellContext context) : base(context)
		{
		}

		public async Task<Address> GetAddressByUserId(Guid userId)
		{
			return await _context.Addresses
				.Where(s => s.CreatedBy == userId)
				.Select(s => new Address()
				{
					Id = s.Id,
					City = s.City,
					Street = s.Street,
					ApartmentNumber = s.ApartmentNumber,
					PostalCode = s.PostalCode
				})
				.FirstOrDefaultAsync();
		}

		public bool IsExists(Guid userId)
		{
			return _context.Addresses
				.Any(s => s.CreatedBy == userId);
		}
	}
}
