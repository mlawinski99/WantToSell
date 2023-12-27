using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Address.Models;
using WantToSell.Domain.Interfaces;

namespace WantToSell.Application.Features.Address.Commands
{
	public class UpdateAddress
	{
		public record Command(AddressUpdateModel Model) : IRequest<bool>;

		public class Handler : IRequestHandler<Command, bool>
		{
			private readonly IAddressRepository _addressRepository;
			private readonly IUserContext _userContext;
			private readonly IMapper _mapper;

			public Handler(IAddressRepository addressRepository, 
				IUserContext userContext,
				IMapper mapper)
			{
				_addressRepository = addressRepository;
				_userContext = userContext;
				_mapper = mapper;
			}
			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{
				var updateModel = await _addressRepository.GetByIdAsync(request.Model.Id);

				if (updateModel == null)
					throw new NotFoundException("Address can not be found!");

				var userId = _userContext.UserId;

				if (updateModel.CreatedBy != userId)
					throw new AccessDeniedException();

				_mapper.Map(request.Model, updateModel);
				
				await _addressRepository.UpdateAsync(updateModel);

				return true;
			}
		}
	}
}
