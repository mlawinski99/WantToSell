using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Features.Category.Models;

namespace WantToSell.Application.Features.Category.Queries;

public class GetCategoryList
{
    public record Query : IRequest<List<CategoryViewModel>>;

    public class Handler : IRequestHandler<Query, List<CategoryViewModel>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public Handler(IMapper mapper,
            ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryViewModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.GetListAsync();

            return _mapper.Map<List<CategoryViewModel>>(result);
        }
    }
}