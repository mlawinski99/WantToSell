using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Subcategory.Models;
using WantToSell.Application.Mappers.Subcategory;

namespace WantToSell.Application.Features.Subcategory.Queries;

public static class GetSubcategoryListByCategoryId
{
    public record Query(Guid CategoryId) : IRequest<List<SubcategoryViewModel>>;

    public class Handler : IRequestHandler<Query, List<SubcategoryViewModel>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly SubcategoryViewModelMapper _subcategoryViewModelMapper;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public Handler(SubcategoryViewModelMapper subcategoryViewModelMapper,
            ISubcategoryRepository subcategoryRepository,
            ICategoryRepository categoryRepository)
        {
            _subcategoryViewModelMapper = subcategoryViewModelMapper;
            _subcategoryRepository = subcategoryRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<SubcategoryViewModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            if (!_categoryRepository.IsCategoryExists(request.CategoryId))
                throw new BadRequestException("Category does not exist!");

            var list = await _subcategoryRepository.GetListByCategoryIdAsync(request.CategoryId);
            var result = await _subcategoryViewModelMapper.Map(list);
            
            return result.ToList();
        }
    }
}