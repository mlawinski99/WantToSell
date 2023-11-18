using WantToSell.Application.Contracts.Persistence;
using WantToSell.Domain;
using WantToSell.Persistence.DbContext;

namespace WantToSell.Persistence.Repositories
{
	public class AddressRepository : GenericRepository<Address>, IAddressRepository
	{
		public AddressRepository(WantToSellContext context) : base(context)
		{
		}
	}
}
