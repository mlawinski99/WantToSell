using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Logging;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Category.Models;
using WantToSell.Application.Features.Category.Validators;

namespace WantToSell.Application.Features.Category.Commands
{
    public class UpdateCategory
    {
        public record Command(CategoryUpdateModel model) : IRequest<bool>;

        public class Handler : IRequestHandler<Command, bool>
        {
	        private readonly IApplicationLogger<UpdateCategory> _logger;
	        private readonly ICategoryRepository _categoryRepository;
	        private readonly IMapper _mapper;
            public Handler(ICategoryRepository categoryRepository, 
	            IApplicationLogger<UpdateCategory> logger,
	            IMapper mapper)
            {
	            _mapper = mapper;
	            _categoryRepository = categoryRepository;
	            _logger = logger;
            }
            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var validator = new CategoryUpdateModelValidator();
                    var validationResult = await validator.ValidateAsync(request.model, cancellationToken);

                    if (!validationResult.IsValid)
	                    throw new BadRequestException("Invalid request!", validationResult);

					var updateModel = await _categoryRepository.GetByIdAsync(request.model.Id);

                    if (updateModel == null)
	                    throw new NotFoundException("Category can not be found!");

	                _mapper.Map(request.model, updateModel);

                    await _categoryRepository.UpdateAsync(updateModel);

                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                    throw;
                }
            }
        }
    }
}
