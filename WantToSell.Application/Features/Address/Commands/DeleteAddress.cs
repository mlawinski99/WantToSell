using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WantToSell.Application.Contracts.Logging;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;

namespace WantToSell.Application.Features.Address.Commands
{
	public class DeleteAddress
	{
		public record Command(Guid id) : IRequest<bool>;

		public class Handler : IRequestHandler<Command, bool>
		{
			private readonly IAddressRepository _addressRepository;
			private readonly IApplicationLogger<DeleteAddress> _logger;

			public Handler(IAddressRepository addressRepository, IApplicationLogger<DeleteAddress> logger)
			{
				_addressRepository = addressRepository;
				_logger = logger;
			}
			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{
				try
				{
					var entity = await _addressRepository.GetByIdAsync(request.id);

					if (entity == null)
						throw new NotFoundException($"Address does not exist!");
					
					//@todo
					//if(entity.CreatedBy != UserId)
					// return BadRequest

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
