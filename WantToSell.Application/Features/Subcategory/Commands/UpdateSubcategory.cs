using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Subcategory.Models;

namespace WantToSell.Application.Features.Subcategory.Commands;

public class UpdateSubcategory
{
    public record Command(SubcategoryUpdateModel Model) : IRequest<SubcategoryViewModel>;

    public class Handler : IRequestHandler<Command, SubcategoryViewModel>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public Handler(ISubcategoryRepository subcategoryRepository,
            IMapper mapper,
            ICategoryRepository categoryRepository)
        {
            _subcategoryRepository = subcategoryRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<SubcategoryViewModel> Handle(Command request, CancellationToken cancellationToken)
        {
            var updateModel = await _subcategoryRepository.GetByIdAsync(request.Model.Id);

            if (updateModel == null)
                throw new NotFoundException("Subcategory can not be found!");

            if (!_categoryRepository.IsCategoryExists(request.Model.CategoryId))
                throw new NotFoundException("Category can not be found!");

            if (_subcategoryRepository.IsSubcategoryNameExists(request.Model.Name))
                throw new BadRequestException("Subcategory name already exists!");

            _mapper.Map(request.Model, updateModel);

            await _subcategoryRepository.UpdateAsync(updateModel);

            return _mapper.Map<SubcategoryViewModel>(updateModel);
        }
    }
}