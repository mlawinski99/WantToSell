using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Subcategory.Models;

namespace WantToSell.Application.Features.Subcategory.Queries;

public class GetSubcategoryListByCategoryId
{
    public record Query(Guid CategoryId) : IRequest<List<SubcategoryViewModel>>;

    public class Handler : IRequestHandler<Query, List<SubcategoryViewModel>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public Handler(IMapper mapper,
            ISubcategoryRepository subcategoryRepository,
            ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _subcategoryRepository = subcategoryRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<SubcategoryViewModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            if (!_categoryRepository.IsCategoryExists(request.CategoryId))
                throw new BadRequestException("Category does not exist!");

            var result = await _subcategoryRepository.GetListByCategoryIdAsync(request.CategoryId);

            return _mapper.Map<List<SubcategoryViewModel>>(result);
        }
    }
}