using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Category.Models;

namespace WantToSell.Application.Features.Category.Commands;

public class UpdateCategory
{
    public record Command(CategoryUpdateModel Model) : IRequest<CategoryViewModel>;

    public class Handler : IRequestHandler<Command, CategoryViewModel>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public Handler(ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryViewModel> Handle(Command request, CancellationToken cancellationToken)
        {
            var updateModel = await _categoryRepository.GetByIdAsync(request.Model.Id);

            if (updateModel == null)
                throw new NotFoundException("Category can not be found!");

            if (_categoryRepository.IsCategoryNameExists(request.Model.Name))
                throw new BadRequestException("Category name already exists!");

            _mapper.Map(request.Model, updateModel);

            await _categoryRepository.UpdateAsync(updateModel);

            return _mapper.Map<CategoryViewModel>(updateModel);
        }
    }
}