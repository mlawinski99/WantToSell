using MediatR;
using WantToSell.Application.Contracts.Identity;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Address.Models;
using WantToSell.Application.Mappers.Address;

namespace WantToSell.Application.Features.Address.Commands;

public static class UpdateAddress
{
    public record Command(AddressUpdateModel Model) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly AddressMapper _addressMapper;
        private readonly IUserContext _userContext;

        public Handler(IAddressRepository addressRepository,
            IUserContext userContext,
            AddressMapper addressMapper)
        {
            _addressRepository = addressRepository;
            _userContext = userContext;
            _addressMapper = addressMapper;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await _addressRepository.GetByIdAsync(request.Model.Id);

            if (entity is null)
                throw new NotFoundException("Address can not be found!");

            var userId = _userContext.UserId;

            if (entity.CreatedBy != userId)
                throw new AccessDeniedException();

            await _addressMapper.Map(request.Model, entity);

            await _addressRepository.UpdateAsync(entity);

            return Unit.Value;
        }
    }
}