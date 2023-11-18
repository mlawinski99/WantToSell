using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Identity;
using WantToSell.Application.Contracts.Logging;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Address.Models;
using WantToSell.Application.Features.Address.Validators;

namespace WantToSell.Application.Features.Address.Commands
{
	public class CreateAddress
	{
		public record Command(AddressCreateModel Model) : IRequest<Guid>;

		public class Handler : IRequestHandler<Command, Guid>
		{
			private readonly IMapper _mapper;
			private readonly IAddressRepository _addressRepository;
			private readonly IApplicationLogger<CreateAddress> _logger;
			private readonly IUserService _userService;

			public Handler(IMapper mapper, 
				IAddressRepository addressRepository, 
				IApplicationLogger<CreateAddress> logger,
				IUserService userService)
			{
				_mapper = mapper;
				_addressRepository = addressRepository;
				_logger = logger;
				_userService = userService;
			}
			public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
			{
				try
				{
					var validator = new AddressCreateModelValidator();
					var validationResult = await validator.ValidateAsync(request.Model, cancellationToken);

					if (validationResult.Errors.Any())
						throw new BadRequestException("Invalid request!", validationResult);
					
					var userId = _userService.GetCurrentUserId();
					var isUserAddressExists = _addressRepository.IsExists(userId);
					
					if (isUserAddressExists)
						throw new BadRequestException("Address already exists!");
					
					var entity = _mapper.Map<Domain.Address>(request.Model);
					entity.Id = Guid.NewGuid();

					await _addressRepository.CreateAsync(entity);

					return entity.Id;
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
