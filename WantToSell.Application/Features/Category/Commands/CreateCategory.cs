using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Features.Category.Models;

namespace WantToSell.Application.Features.Category.Commands
{
    public class CreateCategory
    {
        public record Command(CategoryCreateModel Model) : IRequest<bool>;

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly IMapper _mapper;
            private readonly ICategoryRepository _categoryRepository;

            public Handler(IMapper mapper, ICategoryRepository categoryRepository)
            {
                _mapper = mapper;
                _categoryRepository = categoryRepository;
            }
            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<Domain.Category>(request.Model);
                entity.Id = Guid.NewGuid();

                await _categoryRepository.CreateAsync(entity);

                return true;
            }
        }
    }
}
