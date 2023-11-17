using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WantToSell.Application.Contracts.Logging;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Address.Models;
using WantToSell.Application.Features.Address.Validators;

namespace WantToSell.Application.Features.Address.Commands
{
	public class UpdateAddress
	{
		public record Command(AddressUpdateModel model) : IRequest<bool>;

		public class Handler : IRequestHandler<Command, bool>
		{
			private readonly IApplicationLogger<UpdateAddress> _logger;
			private readonly IAddressRepository _addressRepository;

			public Handler(IAddressRepository addressRepository, IApplicationLogger<UpdateAddress> logger)
			{
				_addressRepository = addressRepository;
				_logger = logger;
			}
			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{
				try
				{
					var validator = new AddressUpdateModelValidator();
					var validationResult = await validator.ValidateAsync(request.model, cancellationToken);

					if (!validationResult.IsValid)
						throw new BadRequestException("Invalid request!");

					var updateModel = await _addressRepository.GetByIdAsync(request.model.Id);//_mapper.Map<Domain.Category>(request.model);

					if (updateModel == null)
						throw new NotFoundException("Address can not be found!");

					//@todo
					//if (updateModel.CreatedBy != UserId)
					//	throw BadRequest()

					await _addressRepository.UpdateAsync(updateModel);

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
