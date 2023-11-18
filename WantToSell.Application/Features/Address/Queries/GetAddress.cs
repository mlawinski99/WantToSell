using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Identity;
using WantToSell.Application.Contracts.Logging;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Address.Models;

namespace WantToSell.Application.Features.Address.Queries
{
	public class GetAddress
	{
		public record Query() : IRequest<AddressDetailModel>;

		public class Handler : IRequestHandler<Query, AddressDetailModel>
		{
			private readonly IMapper _mapper;
			private readonly IAddressRepository _addressRepository;
			private readonly IApplicationLogger<GetAddress> _logger;
			private readonly IUserService _userService;

			public Handler(IMapper mapper,
				IAddressRepository addressRepository,
				IApplicationLogger<GetAddress> logger,
				IUserService userService)
			{
				_mapper = mapper;
				_addressRepository = addressRepository;
				_logger = logger;
				_userService = userService;
			}
			public async Task<AddressDetailModel> Handle(Query request, CancellationToken cancellationToken)
			{
				try
				{
					var userId = _userService.GetCurrentUserId();
					var result = await _addressRepository.GetAddressForUser(userId);

					if (result == null)
						return new AddressDetailModel();
					
					return _mapper.Map<AddressDetailModel>(result);
				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message, ex);
					throw;
				}
			}
		}
	}
}
