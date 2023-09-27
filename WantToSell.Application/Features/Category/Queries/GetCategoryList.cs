using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WantToSell.Application.Contracts.DataAccess;
using WantToSell.Application.Features.Category.Models;

namespace WantToSell.Application.Features.Category.Queries
{
    public static class GetCategoryList
    {
        public record Query : IRequest<List<CategoryListModel>>;

        public class Handler : IRequestHandler<Query, List<CategoryListModel>>
        {
            private readonly IMapper _mapper;
            private readonly ICategoryRepository _categoryRepository;

            public Handler(IMapper mapper, ICategoryRepository categoryRepository)
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
