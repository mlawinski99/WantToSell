using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Category.Models;

namespace WantToSell.Application.Features.Category.Commands
{
    public class UpdateCategory
    {
        public record Command(CategoryUpdateModel Model) : IRequest<bool>;

        public class Handler : IRequestHandler<Command, bool>
        {
	        private readonly ICategoryRepository _categoryRepository;
	        private readonly IMapper _mapper;
            public Handler(ICategoryRepository categoryRepository,
	            IMapper mapper)
            {
	            _mapper = mapper;
	            _categoryRepository = categoryRepository;
            }
            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
				var updateModel = await _categoryRepository.GetByIdAsync(request.Model.Id);

                if (updateModel == null)
                    throw new NotFoundException("Category can not be found!");

                _mapper.Map(request.Model, updateModel);

                await _categoryRepository.UpdateAsync(updateModel);

                return true;
            }
        }
    }
}
