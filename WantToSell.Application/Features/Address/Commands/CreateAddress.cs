using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Address.Models;
using WantToSell.Domain.Interfaces;

namespace WantToSell.Application.Features.Address.Commands
{
	public class CreateAddress
	{
		public record Command(AddressCreateModel Model) : IRequest<Guid>;

		public class Handler : IRequestHandler<Command, Guid>
		{
			private readonly IMapper _mapper;
			private readonly IAddressRepository _addressRepository;
			private readonly IUserContext _userContext;

			public Handler(IMapper mapper, 
				IAddressRepository addressRepository, 
				IUserContext userContext)
			{
				_mapper = mapper;
				_addressRepository = addressRepository;
				_userContext = userContext;
			}
			public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
			{
				var userId = _userContext.UserId;
				var isUserAddressExists = _addressRepository.IsExists(userId);
				
				if (isUserAddressExists)
					throw new BadRequestException("Address already exists!");
				
				var entity = _mapper.Map<Domain.Address>(request.Model);
				entity.Id = Guid.NewGuid();

				await _addressRepository.CreateAsync(entity);

				return entity.Id;
			}
		}
	}
}
