using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Logging;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Subcategory.Models;
using WantToSell.Application.Features.Subcategory.Validators;

namespace WantToSell.Application.Features.Subcategory.Commands
{
	public class CreateSubcategory
	{
		public record Command(SubcategoryCreateModel model) : IRequest<bool>;

		public class Handler : IRequestHandler<Command, bool>
		{
			private readonly IMapper _mapper;
			private readonly ISubcategoryRepository _subcategoryRepository;
			private readonly ICategoryRepository _categoryRepository;
			private readonly IApplicationLogger<CreateSubcategory> _logger;

			public Handler(IMapper mapper,
				ISubcategoryRepository subcategoryRepository,
				ICategoryRepository categoryRepository,
				IApplicationLogger<CreateSubcategory> logger)
			{
				_mapper = mapper;
				_subcategoryRepository = subcategoryRepository;
				_categoryRepository = categoryRepository;
				_logger = logger;
			}
			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{
				try
				{
					var validator = new SubcategoryCreateModelValidator(_categoryRepository);
					var validationResult = await validator.ValidateAsync(request.model, cancellationToken);

					if (validationResult.Errors.Any())
						throw new BadRequestException("Invalid request!", validationResult);

					var entity = _mapper.Map<Domain.Subcategory>(request.model);
					entity.Id = Guid.NewGuid();

					await _subcategoryRepository.CreateAsync(entity);

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
