using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Features.Subcategory.Models;

namespace WantToSell.Application.Features.Subcategory.Commands
{
	public class CreateSubcategory
	{
		public record Command(SubcategoryCreateModel Model) : IRequest<bool>;

		public class Handler : IRequestHandler<Command, bool>
		{
			private readonly IMapper _mapper;
			private readonly ISubcategoryRepository _subcategoryRepository;

			public Handler(IMapper mapper,
				ISubcategoryRepository subcategoryRepository)
			{
				_mapper = mapper;
				_subcategoryRepository = subcategoryRepository;
			}
			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{
				var entity = _mapper.Map<Domain.Subcategory>(request.Model);
				entity.Id = Guid.NewGuid();

				await _subcategoryRepository.CreateAsync(entity);

				return true;
			}
		}
	}
}
