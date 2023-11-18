using MediatR;
using AutoMapper;
using WantToSell.Application.Contracts.Logging;
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
			private readonly IApplicationLogger<UpdateSubcategory> _logger;
			private readonly IMapper _mapper;
			private readonly ISubcategoryRepository _subcategoryRepository;
			private readonly ICategoryRepository _categoryRepository;

			public Handler(ISubcategoryRepository subcategoryRepository,
				ICategoryRepository categoryRepository,
				IApplicationLogger<UpdateSubcategory> logger,
				IMapper mapper)
			{
				_subcategoryRepository = subcategoryRepository;
				_categoryRepository = categoryRepository;
				_logger = logger;
				_mapper = mapper;
			}
			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{
				try
				{
					var validator = new SubcategoryUpdateModelValidator(_categoryRepository);
					var validationResult = await validator.ValidateAsync(request.Model, cancellationToken);

					if (!validationResult.IsValid)
						throw new BadRequestException("Invalid request!", validationResult);

					var updateModel = await _subcategoryRepository.GetByIdAsync(request.Model.Id);

					if (updateModel == null)
						throw new NotFoundException("Subcategory can not be found!");

					_mapper.Map(request.Model, updateModel);

					await _subcategoryRepository.UpdateAsync(updateModel);

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
