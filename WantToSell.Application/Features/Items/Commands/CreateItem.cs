using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WantToSell.Application.Contracts.Logging;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Category.Commands;
using WantToSell.Application.Features.Category.Models;
using WantToSell.Application.Features.Category.Validators;
using WantToSell.Application.Features.Items.Models;
using WantToSell.Application.Features.Items.Validators;

namespace WantToSell.Application.Features.Items.Commands
{
	public class CreateItem
	{
		public record Command(ItemCreateModel model) : IRequest<bool>;

		public class Handler : IRequestHandler<Command, bool>
		{
			private readonly IMapper _mapper;
			private readonly IItemRepository _itemRepository;
			private readonly IApplicationLogger<CreateItem> _logger;

			public Handler(IMapper mapper, IItemRepository itemRepository, IApplicationLogger<CreateItem> logger)
			{
				_mapper = mapper;
				_itemRepository = itemRepository;
				_logger = logger;
			}
			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{
				try
				{
					var validator = new ItemCreateModelValidator();
					var validationResult = await validator.ValidateAsync(request.model, cancellationToken);

					if (validationResult.Errors.Any())
						throw new BadRequestException("Invalid request!", validationResult);

					var entity = _mapper.Map<Domain.Item>(request.model);
					entity.Id = Guid.NewGuid();

					await _itemRepository.CreateAsync(entity);

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
