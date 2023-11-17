using MediatR;
using WantToSell.Application.Contracts.Logging;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;

namespace WantToSell.Application.Features.Subcategory.Commands
{
	public class DeleteSubcategory
	{
		public record Command(Guid id) : IRequest<bool>;

		public class Handler : IRequestHandler<Command, bool>
		{
			private readonly ISubcategoryRepository _subcategoryRepository;
			private readonly IApplicationLogger<DeleteSubcategory> _logger;

			public Handler(ISubcategoryRepository subcategoryRepository, IApplicationLogger<DeleteSubcategory> logger)
			{
				_subcategoryRepository = subcategoryRepository;
				_logger = logger;
			}
			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{
				try
				{
					var entity = await _subcategoryRepository.GetByIdAsync(request.id);

					if (entity == null)
						throw new NotFoundException($"Subcategory does not exist!");

					await _subcategoryRepository.DeleteAsync(entity);

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
