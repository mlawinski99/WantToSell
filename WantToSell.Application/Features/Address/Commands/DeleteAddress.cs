using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Domain.Interfaces;

namespace WantToSell.Application.Features.Address.Commands
{
	public static class DeleteAddress
	{
		public record Command(Guid Id) : IRequest<bool>;

		public class Handler : IRequestHandler<Command, bool>
		{
			private readonly IAddressRepository _addressRepository;
			private readonly IUserContext _userContext;

			public Handler(IAddressRepository addressRepository, 
				IUserContext userContext)
			{
				_addressRepository = addressRepository;
				_userContext = userContext;
			}
			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{
				var entity = await _addressRepository.GetByIdAsync(request.Id);

				if (entity == null)
					throw new NotFoundException($"Address does not exist!");
				
				var userId = _userContext.UserId;
				
				if (entity.CreatedBy != userId)
					throw new AccessDeniedException();

				await _addressRepository.DeleteAsync(entity);
				
				return true;
			}
		}
	}
}
