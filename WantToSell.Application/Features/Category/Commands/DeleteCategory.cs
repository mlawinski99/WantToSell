using MediatR;
using WantToSell.Application.Contracts.Logging;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;

namespace WantToSell.Application.Features.Category.Commands
{
	public class DeleteCategory
	{
		public record Command(Guid Id) : IRequest<bool>;

		public class Handler : IRequestHandler<Command, bool>
		{
			private readonly ICategoryRepository _categoryRepository;
			private readonly IApplicationLogger<DeleteCategory> _logger;

			public Handler(ICategoryRepository categoryRepository, IApplicationLogger<DeleteCategory> logger)
			{
				_categoryRepository = categoryRepository;
				_logger = logger;
			}
			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{
				try
				{
					var entity = await _categoryRepository.GetByIdAsync(request.Id);

					if (entity == null)
						throw new NotFoundException($"Category does not exist!");

					await _categoryRepository.DeleteAsync(entity);

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
