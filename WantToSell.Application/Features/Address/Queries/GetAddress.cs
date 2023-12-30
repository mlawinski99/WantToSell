using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Features.Address.Models;
using WantToSell.Domain.Interfaces;

namespace WantToSell.Application.Features.Address.Queries
{
	public class GetAddress
	{
		public record Query() : IRequest<AddressDetailModel>;

		public class Handler : IRequestHandler<Query, AddressDetailModel>
		{
			private readonly IMapper _mapper;
			private readonly IAddressRepository _addressRepository;
			private readonly IUserContext _userContext;

			public Handler(IMapper mapper,
				IAddressRepository addressRepository,
				IUserContext userContext)
			{
				_mapper = mapper;
				_addressRepository = addressRepository;
				_userContext = userContext;
			}
			public async Task<AddressDetailModel> Handle(Query request, CancellationToken cancellationToken)
			{
				var userId = _userContext.UserId;
				var result = await _addressRepository.GetAddressByUserId(userId);

				if (result == null)
					return null;
				
				return _mapper.Map<AddressDetailModel>(result);
			}
		}
	}
}
