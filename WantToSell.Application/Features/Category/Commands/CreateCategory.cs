using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Category.Models;

namespace WantToSell.Application.Features.Category.Commands;

public class CreateCategory
{
    public record Command(CategoryCreateModel Model) : IRequest<CategoryViewModel>;

    public class Handler : IRequestHandler<Command, CategoryViewModel>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public Handler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryViewModel> Handle(Command request, CancellationToken cancellationToken)
        {
            if (_categoryRepository.IsCategoryNameExists(request.Model.Name))
                throw new BadRequestException("Category name already exists!");

            var entity = _mapper.Map<Domain.Category>(request.Model);
            entity.Id = Guid.NewGuid();

            await _categoryRepository.CreateAsync(entity);

            return _mapper.Map<CategoryViewModel>(entity);
        }
    }
}