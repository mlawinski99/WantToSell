using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Subcategory.Models;
using WantToSell.Application.Mappers.Subcategory;

namespace WantToSell.Application.Features.Subcategory.Commands;

public class UpdateSubcategory
{
    public record Command(SubcategoryUpdateModel Model) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly SubcategoryMapper _subcategoryMapper;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public Handler(ISubcategoryRepository subcategoryRepository,
            SubcategoryMapper subcategoryMapper,
            ICategoryRepository categoryRepository)
        {
            _subcategoryRepository = subcategoryRepository;
            _subcategoryMapper = subcategoryMapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await _subcategoryRepository.GetByIdAsync(request.Model.Id);

            if (entity is null)
                throw new NotFoundException("Subcategory can not be found!");

            if (!_categoryRepository.IsCategoryExists(request.Model.CategoryId))
                throw new NotFoundException("Category can not be found!");

            if (_subcategoryRepository.IsSubcategoryNameExists(request.Model.Name))
                throw new BadRequestException("Subcategory name already exists!");

            await _subcategoryMapper.Map(request.Model, entity);
            await _subcategoryRepository.UpdateAsync(entity);

            return Unit.Value;
        }
    }
}