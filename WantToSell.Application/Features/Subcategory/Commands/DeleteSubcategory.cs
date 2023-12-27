using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;

namespace WantToSell.Application.Features.Subcategory.Commands
{
	public class DeleteSubcategory
	{
		public record Command(Guid Id) : IRequest<bool>;

		public class Handler : IRequestHandler<Command, bool>
		{
			private readonly ISubcategoryRepository _subcategoryRepository;

			public Handler(ISubcategoryRepository subcategoryRepository)
			{
				_subcategoryRepository = subcategoryRepository;
			}
			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{
				var entity = await _subcategoryRepository.GetByIdAsync(request.Id);

				if (entity == null)
					throw new NotFoundException($"Subcategory does not exist!");

				await _subcategoryRepository.DeleteAsync(entity);

				return true;
			}
		}
	}
}
