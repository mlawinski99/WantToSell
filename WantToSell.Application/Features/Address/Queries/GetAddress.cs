using MediatR;
using WantToSell.Application.Contracts.Identity;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Features.Address.Models;
using WantToSell.Application.Mappers.Address;

namespace WantToSell.Application.Features.Address.Queries;

public static class GetAddress
{
    public record Query : IRequest<AddressDetailModel>;

    public class Handler : IRequestHandler<Query, AddressDetailModel>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly AddressDetailModelMapper _addressDetailModelMapper;
        private readonly IUserContext _userContext;

        public Handler(AddressDetailModelMapper addressDetailModelMapper,
            IAddressRepository addressRepository,
            IUserContext userContext)
        {
            _addressDetailModelMapper = addressDetailModelMapper;
            _addressRepository = addressRepository;
            _userContext = userContext;
        }

        public async Task<AddressDetailModel> Handle(Query request, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;
            var address = await _addressRepository.GetAddressByUserId(userId);

            return await _addressDetailModelMapper.Map(address);
        }
    }
}