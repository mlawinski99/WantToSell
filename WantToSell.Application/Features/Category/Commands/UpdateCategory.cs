using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Category.Models;
using WantToSell.Application.Mappers.Category;

namespace WantToSell.Application.Features.Category.Commands;

public static class UpdateCategory
{
    public record Command(CategoryUpdateModel Model) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryMapper _categoryMapper;

        public Handler(ICategoryRepository categoryRepository,
            CategoryMapper categoryMapper)
        {
            _categoryMapper = categoryMapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var updateModel = await _categoryRepository.GetByIdAsync(request.Model.Id);

            if (updateModel is null)
                throw new NotFoundException("Category can not be found!");

            if (_categoryRepository.IsCategoryNameExists(request.Model.Name))
                throw new BadRequestException("Category name already exists!");

            await _categoryMapper.Map(request.Model, updateModel);
            await _categoryRepository.UpdateAsync(updateModel);

            return Unit.Value;
        }
    }
}