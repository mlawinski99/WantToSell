using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WantToSell.Application.Contracts.Logging;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Address.Models;
using WantToSell.Application.Models.Identity;

namespace WantToSell.Application.Features.Address.Queries
{
	public class GetAddress
	{
		public record Query(Guid id, Guid userId) : IRequest<AddressDetailModel>;

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
					var result = await _addressRepository.GetByIdAsync(request.id);

					if (result.CreatedBy != request.userId)
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
