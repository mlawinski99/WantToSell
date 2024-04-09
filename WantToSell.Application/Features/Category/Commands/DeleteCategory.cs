using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;

namespace WantToSell.Application.Features.Category.Commands
{
	public static class DeleteCategory
	{
		public record Command(Guid Id) : IRequest<bool>;

		public class Handler : IRequestHandler<Command, bool>
		{
			private readonly ICategoryRepository _categoryRepository;

			public Handler(ICategoryRepository categoryRepository)
			{
				_categoryRepository = categoryRepository;
			}
			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{
				var entity = await _categoryRepository.GetByIdAsync(request.Id);

				if (entity is null)
					throw new NotFoundException($"Category does not exist!");

				await _categoryRepository.DeleteAsync(entity);

				return true;
			}
		}
	}
}
