using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Subcategory.Models;

namespace WantToSell.Application.Features.Subcategory.Queries;

public class GetSubcategory
{
    public record Query(Guid Id) : IRequest<SubcategoryViewModel>;

    public class Handler : IRequestHandler<Query, SubcategoryViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public Handler(IMapper mapper,
            ISubcategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _subcategoryRepository = categoryRepository;
        }

        public async Task<SubcategoryViewModel> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _subcategoryRepository.GetByIdAsync(request.Id);

            if (result == null)
                throw new NotFoundException("Subcategory can not be found!");

            return _mapper.Map<SubcategoryViewModel>(result);
        }
    }
}