using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WantToSell.Application.Contracts.DataAccess;
using WantToSell.Application.Contracts.Logging;
using WantToSell.Application.Features.Category.Commands;
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
            private readonly IApplicationLogger<GetCategoryList> _logger;

			public Handler(IMapper mapper, 
				ICategoryRepository categoryRepository, 
				IApplicationLogger<GetCategoryList> logger)
            {
                _mapper = mapper;
                _categoryRepository = categoryRepository;
                _logger = logger;
            }
            public async Task<List<CategoryListModel>> Handle(Query request, CancellationToken cancellationToken)
            {
	            try
	            {
		            var result = await _categoryRepository.GetListAsync();

		            return _mapper.Map<List<CategoryListModel>>(result);
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
