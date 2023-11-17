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
using WantToSell.Application.Features.Category.Commands;
using WantToSell.Application.Features.Category.Models;
using WantToSell.Application.Features.Category.Validators;
using WantToSell.Application.Features.Subcategory.Models;
using WantToSell.Application.Features.Subcategory.Validators;

namespace WantToSell.Application.Features.Subcategory.Commands
{
	internal class CreateSubcategory
	{
		public record Command(SubcategoryCreateModel model) : IRequest<bool>;

		public class Handler : IRequestHandler<Command, bool>
		{
			private readonly IMapper _mapper;
			private readonly ISubcategoryRepository _subcategoryRepository;
			private readonly IApplicationLogger<CreateSubcategory> _logger;

			public Handler(IMapper mapper, ISubcategoryRepository subcategoryRepository, IApplicationLogger<CreateSubcategory> logger)
			{
				_mapper = mapper;
				_subcategoryRepository = subcategoryRepository;
				_logger = logger;
			}
			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{
				try
				{
					var validator = new SubcategoryCreateModelValidator();
					var validationResult = await validator.ValidateAsync(request.model, cancellationToken);

					if (validationResult.Errors.Any())
						throw new BadRequestException("Invalid request!", validationResult);

					var entity = _mapper.Map<Domain.Subcategory>(request.model);
					entity.Id = Guid.NewGuid();

					await _subcategoryRepository.CreateAsync(entity);

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
