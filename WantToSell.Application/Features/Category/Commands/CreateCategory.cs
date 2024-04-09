using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Category.Models;
using WantToSell.Application.Mappers.Category;

namespace WantToSell.Application.Features.Category.Commands;

public static class CreateCategory
{
    public record Command(CategoryCreateModel Model) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryMapper _categoryMapper;

        public Handler(CategoryMapper categoryMapper, ICategoryRepository categoryRepository)
        {
            _categoryMapper = categoryMapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            if (_categoryRepository.IsCategoryNameExists(request.Model.Name))
                throw new BadRequestException("Category name already exists!");

            var entity = await _categoryMapper.Map(request.Model);

            await _categoryRepository.CreateAsync(entity);

            return Unit.Value;
        }
    }
}