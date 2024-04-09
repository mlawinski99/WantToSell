using MediatR;
using WantToSell.Application.Contracts.Identity;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;

namespace WantToSell.Application.Features.Address.Commands;

public static class DeleteAddress
{
    public record Command(Guid Id) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IUserContext _userContext;

        public Handler(IAddressRepository addressRepository,
            IUserContext userContext)
        {
            _addressRepository = addressRepository;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await _addressRepository.GetByIdAsync(request.Id);

            if (entity is null)
                throw new NotFoundException("Address does not exist!");

            var userId = _userContext.UserId;

            if (entity.CreatedBy != userId)
                throw new AccessDeniedException();

            await _addressRepository.DeleteAsync(entity);

            return Unit.Value;
        }
    }
}