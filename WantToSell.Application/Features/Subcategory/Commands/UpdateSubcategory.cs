using MediatR;
using AutoMapper;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Subcategory.Models;
using WantToSell.Application.Features.Subcategory.Validators;

namespace WantToSell.Application.Features.Subcategory.Commands
{
	public class UpdateSubcategory
	{
		public record Command(SubcategoryUpdateModel Model) : IRequest<bool>;

		public class Handler : IRequestHandler<Command, bool>
		{
			private readonly IMapper _mapper;
			private readonly ISubcategoryRepository _subcategoryRepository;

			public Handler(ISubcategoryRepository subcategoryRepository,
				IMapper mapper)
			{
				_subcategoryRepository = subcategoryRepository;
				_mapper = mapper;
			}
			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{
				var updateModel = await _subcategoryRepository.GetByIdAsync(request.Model.Id);

				if (updateModel == null)
					throw new NotFoundException("Subcategory can not be found!");

				_mapper.Map(request.Model, updateModel);

				await _subcategoryRepository.UpdateAsync(updateModel);

				return true;
			}
		}
	}
}
