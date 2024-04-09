using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Subcategory.Models;
using WantToSell.Application.Mappers.Subcategory;

namespace WantToSell.Application.Features.Subcategory.Queries;

public static class GetSubcategory
{
    public record Query(Guid Id) : IRequest<SubcategoryViewModel>;

    public class Handler : IRequestHandler<Query, SubcategoryViewModel>
    {
        private readonly SubcategoryViewModelMapper _subcategoryViewModelMapper;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public Handler(SubcategoryViewModelMapper subcategoryViewModelMapper,
            ISubcategoryRepository categoryRepository)
        {
            _subcategoryViewModelMapper = subcategoryViewModelMapper;
            _subcategoryRepository = categoryRepository;
        }

        public async Task<SubcategoryViewModel> Handle(Query request, CancellationToken cancellationToken)
        {
            var entity = await _subcategoryRepository.GetByIdAsync(request.Id);

            if (entity is null)
                throw new NotFoundException("Subcategory can not be found!");

            return await _subcategoryViewModelMapper.Map(entity);
        }
    }
}