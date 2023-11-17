using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Logging;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Subcategory.Models;

namespace WantToSell.Application.Features.Subcategory.Queries
{
	public class GetSubcategoryList
	{
		public record Query(Guid categoryId) : IRequest<List<SubcategoryListModel>>;

		public class Handler : IRequestHandler<Query, List<SubcategoryListModel>>
		{
			private readonly IMapper _mapper;
			private readonly ISubcategoryRepository _subcategoryRepository;
			private readonly ICategoryRepository _categoryRepository;
			private readonly IApplicationLogger<GetSubcategoryList> _logger;

			public Handler(IMapper mapper,
				ISubcategoryRepository subcategoryRepository,
				ICategoryRepository categoryRepository,
				IApplicationLogger<GetSubcategoryList> logger)
			{
				_mapper = mapper;
				_subcategoryRepository = subcategoryRepository;
				_categoryRepository = categoryRepository;
				_logger = logger;
			}
			public async Task<List<SubcategoryListModel>> Handle(Query request, CancellationToken cancellationToken)
			{
				try
				{
					if (!_categoryRepository.IsCategoryExists(request.categoryId))
						throw new BadRequestException("Category does not exist!");

					var result = await _subcategoryRepository.GetListByCategoryIdAsync(request.categoryId);

					return _mapper.Map<List<SubcategoryListModel>>(result);
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
