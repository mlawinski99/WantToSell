using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Category.Models;
using WantToSell.Application.Mappers.Category;

namespace WantToSell.Application.Features.Category.Queries;

public static class GetCategory
{
    public record Query(Guid Id) : IRequest<CategoryViewModel>;

    public class Handler : IRequestHandler<Query, CategoryViewModel>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryViewModelMapper _categoryViewModelMapper;

        public Handler(CategoryViewModelMapper categoryViewModelMapper,
            ICategoryRepository categoryRepository)
        {
            _categoryViewModelMapper = categoryViewModelMapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryViewModel> Handle(Query request, CancellationToken cancellationToken)
        {
            var entity = await _categoryRepository.GetByIdAsync(request.Id);

            if (entity is null)
                throw new NotFoundException("Category can not be found!");

            return await _categoryViewModelMapper.Map(entity);
        }
    }
}