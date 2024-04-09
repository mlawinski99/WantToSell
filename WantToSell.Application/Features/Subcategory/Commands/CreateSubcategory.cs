using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Subcategory.Models;
using WantToSell.Application.Mappers.Subcategory;

namespace WantToSell.Application.Features.Subcategory.Commands;

public class CreateSubcategory
{
    public record Command(SubcategoryCreateModel Model) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly SubcategoryMapper _subcategoryMapper;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public Handler(SubcategoryMapper subcategoryMapper,
            ISubcategoryRepository subcategoryRepository,
            ICategoryRepository categoryRepository)
        {
            _subcategoryMapper = subcategoryMapper;
            _subcategoryRepository = subcategoryRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            if (!_categoryRepository.IsCategoryExists(request.Model.CategoryId))
                throw new NotFoundException("Category can not be found!");

            if (_subcategoryRepository.IsSubcategoryNameExists(request.Model.Name))
                throw new BadRequestException("Subcategory name already exists!");

            var entity = await _subcategoryMapper.Map(request.Model, new Domain.Subcategory());

            await _subcategoryRepository.CreateAsync(entity);

            return Unit.Value;
        }
    }
}