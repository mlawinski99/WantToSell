using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Logging;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Address.Models;
using WantToSell.Application.Features.Address.Validators;
using WantToSell.Application.Models.Identity;

namespace WantToSell.Application.Features.Address.Commands
{
	public class CreateAddress
	{
		public record Command(AddressCreateModel model) : IRequest<bool>;

		public class Handler : IRequestHandler<Command, bool>
		{
			private readonly IMapper _mapper;
			private readonly IAddressRepository _addressRepository;
			private readonly IApplicationLogger<CreateAddress> _logger;

			public Handler(IMapper mapper, 
				IAddressRepository addressRepository, 
				IApplicationLogger<CreateAddress> logger)
			{
				_mapper = mapper;
				_addressRepository = addressRepository;
				_logger = logger;
			}
			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{
				try
				{
					var validator = new AddressCreateModelValidator();
					var validationResult = await validator.ValidateAsync(request.model, cancellationToken);

					//@todo
					//var isUserAddressExists = await _addressRepository.IsExists(UserId);
					//if(isUserAddressExists)
					//	throw BadRequest

					if (validationResult.Errors.Any())
						throw new BadRequestException("Invalid request!", validationResult);

					var entity = _mapper.Map<Domain.Address>(request.model);
					entity.Id = Guid.NewGuid();

					await _addressRepository.CreateAsync(entity);

					return true;
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
