using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.DataAccess;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Category.Models;
using WantToSell.Application.Features.Category.Validators;

namespace WantToSell.Application.Features.Category.Commands
{
    public static class UpdateCategory
    {
        public record Command(CategoryUpdateModel model) : IRequest<bool>;

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
                try
                {
                    var validator = new CategoryUpdateModelValidator();
                    var validationResult = await validator.ValidateAsync(request.model);

                    if (!validationResult.IsValid)
                        throw new BadRequestException("Invalid request!");

                    var updateModel = await _categoryRepository.GetByIdAsync(request.model.Id);//_mapper.Map<Domain.Category>(request.model);

                    await _categoryRepository.UpdateAsync(updateModel);

                    return true;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
