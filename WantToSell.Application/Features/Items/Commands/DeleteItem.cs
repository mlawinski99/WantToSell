using MediatR;
using WantToSell.Application.Contracts.Logging;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;

namespace WantToSell.Application.Features.Items.Commands
{
	public class DeleteItem
	{
		public record Command(Guid Id) : IRequest<bool>;

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
					var entity = await _itemRepository.GetByIdAsync(request.Id);

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
