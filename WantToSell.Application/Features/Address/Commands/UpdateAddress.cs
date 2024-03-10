using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Identity;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Address.Models;

namespace WantToSell.Application.Features.Address.Commands;

public class UpdateAddress
{
    public record Command(AddressUpdateModel Model) : IRequest<AddressDetailModel>;

    public class Handler : IRequestHandler<Command, AddressDetailModel>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public Handler(IAddressRepository addressRepository,
            IUserContext userContext,
            IMapper mapper)
        {
            _addressRepository = addressRepository;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<AddressDetailModel> Handle(Command request, CancellationToken cancellationToken)
        {
            var updateModel = await _addressRepository.GetByIdAsync(request.Model.Id);

            if (updateModel == null)
                throw new NotFoundException("Address can not be found!");

            var userId = _userContext.UserId;

            if (updateModel.CreatedBy != userId)
                throw new AccessDeniedException();

            _mapper.Map(request.Model, updateModel);

            await _addressRepository.UpdateAsync(updateModel);

            return _mapper.Map<AddressDetailModel>(updateModel);
        }
    }
}