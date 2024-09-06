using MediatR;
using WantToSell.Application.Contracts.Identity;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Address.Models;
using WantToSell.Application.Mappers.Address;

namespace WantToSell.Application.Features.Address.Commands;

public static class
    CreateAddress
{
    public record Command(AddressCreateModel Model) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly AddressMapper _addressMapper;
        private readonly IAddressRepository _addressRepository;
        private readonly IUserContext _userContext;

        public Handler(AddressMapper addressMapper,
            IAddressRepository addressRepository,
            IUserContext userContext)
        {
            _addressMapper = addressMapper;
            _addressRepository = addressRepository;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;
            var isUserAddressExists = _addressRepository.IsExists(userId);

            if (isUserAddressExists)
                throw new BadRequestException("Address already exists!");

            var entity = await _addressMapper.Map(request.Model);

            await _addressRepository.CreateAsync(entity);

            return Unit.Value;
        }
    }
}