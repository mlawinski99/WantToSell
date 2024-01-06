using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Category.Models;

namespace WantToSell.Application.Features.Category.Commands;

public class CreateCategory
{
    public record Command(CategoryCreateModel Model) : IRequest<bool>;

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public Handler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            if (_categoryRepository.IsCategoryNameExists(request.Model.Name))
                throw new BadRequestException("Category name already exists!");

            var entity = _mapper.Map<Domain.Category>(request.Model);
            entity.Id = Guid.NewGuid();

            await _categoryRepository.CreateAsync(entity);

            return true;
        }
    }
}