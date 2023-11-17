using AutoMapper;
using WantToSell.Application.Features.Address.Models;
using WantToSell.Domain;

namespace WantToSell.Application.Mappings
{
	public class AddressProfile : Profile
	{
		public AddressProfile()
		{
			CreateMap<Address, AddressDetailModel>();
			CreateMap<AddressCreateModel, Address>();
			CreateMap<AddressUpdateModel, Address>();
		}
	}
}
