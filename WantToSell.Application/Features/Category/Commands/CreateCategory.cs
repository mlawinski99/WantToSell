using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WantToSell.Application.Contracts.Logging;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Category.Models;
using WantToSell.Application.Features.Category.Validators;

namespace WantToSell.Application.Features.Category.Commands
{
    public class CreateCategory
    {
        public record Command(CategoryCreateModel model) : IRequest<bool>;

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly IMapper _mapper;
            private readonly ICategoryRepository _categoryRepository;
            private readonly IApplicationLogger<CreateCategory> _logger;

            public Handler(IMapper mapper, ICategoryRepository categoryRepository, IApplicationLogger<CreateCategory> logger)
            {
                _mapper = mapper;
                _categoryRepository = categoryRepository;
                _logger = logger;
            }
            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var validator = new CategoryCreateModelValidator();
                    var validationResult = await validator.ValidateAsync(request.model, cancellationToken);

                    if (validationResult.Errors.Any())
                        throw new BadRequestException("Invalid request!", validationResult);

                    var entity = _mapper.Map<Domain.Category>(request.model);
                    entity.Id = Guid.NewGuid();

                    await _categoryRepository.CreateAsync(entity);

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
