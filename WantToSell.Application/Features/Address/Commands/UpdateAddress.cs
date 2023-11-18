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
	public class UpdateAddress
	{
		public record Command(AddressUpdateModel Model) : IRequest<bool>;

		public class Handler : IRequestHandler<Command, bool>
		{
			private readonly IApplicationLogger<UpdateAddress> _logger;
			private readonly IUserService _userService;
			private readonly IAddressRepository _addressRepository;
			private readonly IMapper _mapper;

			public Handler(IAddressRepository addressRepository, 
				IApplicationLogger<UpdateAddress> logger,
				IUserService userService,
				IMapper mapper)
			{
				_addressRepository = addressRepository;
				_logger = logger;
				_userService = userService;
				_mapper = mapper;
			}
			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{
				try
				{
					var validator = new AddressUpdateModelValidator();
					var validationResult = await validator.ValidateAsync(request.Model, cancellationToken);

					if (!validationResult.IsValid)
						throw new BadRequestException("Invalid request!");

					var updateModel = await _addressRepository.GetByIdAsync(request.Model.Id);

					if (updateModel == null)
						throw new NotFoundException("Address can not be found!");

					var userId = _userService.GetCurrentUserId();

					if (updateModel.CreatedBy != userId)
						throw new AccessDeniedException();

					_mapper.Map(request.Model, updateModel);
					
					await _addressRepository.UpdateAsync(updateModel);

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
