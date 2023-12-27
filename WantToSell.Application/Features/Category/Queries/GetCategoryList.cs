using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Features.Category.Models;

namespace WantToSell.Application.Features.Category.Queries
{
    public class GetCategoryList
    {
        public record Query : IRequest<List<CategoryListModel>>;

        public class Handler : IRequestHandler<Query, List<CategoryListModel>>
        {
            private readonly IMapper _mapper;
            private readonly ICategoryRepository _categoryRepository;

			public Handler(IMapper mapper, 
				ICategoryRepository categoryRepository)
            {
                _mapper = mapper;
                _categoryRepository = categoryRepository;
            }
            public async Task<List<CategoryListModel>> Handle(Query request, CancellationToken cancellationToken)
            {
    		    var result = await _categoryRepository.GetListAsync();

		        return _mapper.Map<List<CategoryListModel>>(result);
            }
        }
    }
}
