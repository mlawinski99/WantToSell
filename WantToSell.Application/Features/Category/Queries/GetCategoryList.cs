using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Features.Category.Models;
using WantToSell.Application.Mappers.Category;

namespace WantToSell.Application.Features.Category.Queries;

public static class GetCategoryList
{
    public record Query : IRequest<List<CategoryViewModel>>;

    public class Handler : IRequestHandler<Query, List<CategoryViewModel>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryViewModelMapper _categoryViewModelMapper;

        public Handler(CategoryViewModelMapper categoryViewModelMapper,
            ICategoryRepository categoryRepository)
        {
            _categoryViewModelMapper = categoryViewModelMapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryViewModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            var entities = await _categoryRepository.GetListAsync();
            var result = await _categoryViewModelMapper.Map(entities);
            
            return result.ToList();
        }
    }
}