using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Subcategory.Models;

namespace WantToSell.Application.Features.Subcategory.Commands;

public class CreateSubcategory
{
    public record Command(SubcategoryCreateModel Model) : IRequest<SubcategoryViewModel>;

    public class Handler : IRequestHandler<Command, SubcategoryViewModel>
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

        public async Task<SubcategoryViewModel> Handle(Command request, CancellationToken cancellationToken)
        {
            if (!_categoryRepository.IsCategoryExists(request.Model.CategoryId))
                throw new NotFoundException("Category can not be found!");

            if (_subcategoryRepository.IsSubcategoryNameExists(request.Model.Name))
                throw new BadRequestException("Subcategory name already exists!");

            var entity = _mapper.Map<Domain.Subcategory>(request.Model);
            entity.Id = Guid.NewGuid();

            await _subcategoryRepository.CreateAsync(entity);

            return _mapper.Map<SubcategoryViewModel>(entity);
        }
    }
}