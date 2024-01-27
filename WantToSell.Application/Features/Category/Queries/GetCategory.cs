using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Category.Models;

namespace WantToSell.Application.Features.Category.Queries;

public class GetCategory
{
    public record Query(Guid Id) : IRequest<CategoryViewModel>;

    public class Handler : IRequestHandler<Query, CategoryViewModel>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public Handler(IMapper mapper,
            ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryViewModel> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.GetByIdAsync(request.Id);

            if (result == null)
                throw new NotFoundException("Category can not be found!");

            return _mapper.Map<CategoryViewModel>(result);
        }
    }
}