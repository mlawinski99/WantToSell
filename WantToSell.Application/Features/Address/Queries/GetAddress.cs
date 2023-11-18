using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Logging;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Address.Models;

namespace WantToSell.Application.Features.Address.Queries
{
	public class GetAddress
	{
		public record Query(Guid Id, Guid UserId) : IRequest<AddressDetailModel>;

		public class Handler : IRequestHandler<Query, AddressDetailModel>
		{
			private readonly IMapper _mapper;
			private readonly IAddressRepository _addressRepository;
			private readonly IApplicationLogger<GetAddress> _logger;

			public Handler(IMapper mapper,
				IAddressRepository addressRepository,
				IApplicationLogger<GetAddress> logger)
			{
				_mapper = mapper;
				_addressRepository = addressRepository;
				_logger = logger;
			}
			public async Task<AddressDetailModel> Handle(Query request, CancellationToken cancellationToken)
			{
				try
				{
					var result = await _addressRepository.GetByIdAsync(request.Id);

					if (result.CreatedBy != request.UserId)
						throw new BadRequestException("Invalid request!"); 

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
