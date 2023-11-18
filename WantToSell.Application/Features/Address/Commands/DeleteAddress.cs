using MediatR;
using WantToSell.Application.Contracts.Identity;
using WantToSell.Application.Contracts.Logging;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;

namespace WantToSell.Application.Features.Address.Commands
{
	public class DeleteAddress
	{
		public record Command(Guid Id) : IRequest<bool>;

		public class Handler : IRequestHandler<Command, bool>
		{
			private readonly IAddressRepository _addressRepository;
			private readonly IApplicationLogger<DeleteAddress> _logger;
			private readonly IUserService _userService;

			public Handler(IAddressRepository addressRepository, 
				IApplicationLogger<DeleteAddress> logger,
				IUserService userService)
			{
				_addressRepository = addressRepository;
				_logger = logger;
				_userService = userService;
			}
			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{
				try
				{
					var entity = await _addressRepository.GetByIdAsync(request.Id);

					if (entity == null)
						throw new NotFoundException($"Address does not exist!");
					
					var userId = _userService.GetCurrentUserId();

					if (entity.CreatedBy != userId)
						throw new AccessDeniedException();

					await _addressRepository.DeleteAsync(entity);

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
