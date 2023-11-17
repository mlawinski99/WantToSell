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

namespace WantToSell.Application.Features.Items.Commands
{
	public class DeleteItem
	{
		public record Command(Guid id) : IRequest<bool>;

		public class Handler : IRequestHandler<Command, bool>
		{
			private readonly IItemRepository _itemRepository;
			private readonly IApplicationLogger<DeleteItem> _logger;

			public Handler(IItemRepository itemRepository, IApplicationLogger<DeleteItem> logger)
			{
				_itemRepository = itemRepository;
				_logger = logger;
			}
			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{
				try
				{
					var entity = await _itemRepository.GetByIdAsync(request.id);

					if (entity == null)
						throw new NotFoundException($"Item does not exist!");

					await _itemRepository.DeleteAsync(entity);

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
