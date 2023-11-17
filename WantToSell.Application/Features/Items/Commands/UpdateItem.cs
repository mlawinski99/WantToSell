using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WantToSell.Application.Contracts.Logging;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Items.Models;
using WantToSell.Application.Features.Items.Validators;

namespace WantToSell.Application.Features.Items.Commands
{
	public class UpdateItem
	{
		public record Command(ItemUpdateModel model) : IRequest<bool>;

		public class Handler : IRequestHandler<Command, bool>
		{
			private readonly IApplicationLogger<UpdateItem> _logger;
			private readonly IItemRepository _itemRepository;

			public Handler(IItemRepository itemRepository, IApplicationLogger<UpdateItem> logger)
			{
				_itemRepository = itemRepository;
				_logger = logger;
			}
			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{
				try
				{
					var validator = new ItemUpdateModelValidator();
					var validationResult = await validator.ValidateAsync(request.model, cancellationToken);

					if (!validationResult.IsValid)
						throw new BadRequestException("Invalid request!");

					var updateModel = await _itemRepository.GetByIdAsync(request.model.Id);//_mapper.Map<Domain.Category>(request.model);

					if (updateModel == null)
						throw new NotFoundException("Item can not be found!");

					await _itemRepository.UpdateAsync(updateModel);

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
