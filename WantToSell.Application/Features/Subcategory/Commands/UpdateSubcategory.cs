using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WantToSell.Application.Contracts.Logging;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Subcategory.Models;

namespace WantToSell.Application.Features.Subcategory.Commands
{
	internal class UpdateSubcategory
	{
		public record Command(SubcategoryUpdateModel model) : IRequest<bool>;

		public class Handler : IRequestHandler<Command, bool>
		{
			private readonly IApplicationLogger<UpdateSubcategory> _logger;
			private readonly ISubcategoryRepository _subcategoryRepository;

			public Handler(ISubcategoryRepository subcategoryRepository, IApplicationLogger<UpdateSubcategory> logger)
			{
				_subcategoryRepository = subcategoryRepository;
				_logger = logger;
			}
			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{
				try
				{
					//var validator = new SubcategoryUpdateModelValidator();
					//var validationResult = await validator.ValidateAsync(request.model, cancellationToken);

					//if (!validationResult.IsValid)
					//	throw new BadRequestException("Invalid request!");

					var updateModel = await _subcategoryRepository.GetByIdAsync(request.model.Id);//_mapper.Map<Domain.Category>(request.model);

					if (updateModel == null)
						throw new NotFoundException("Subcategory can not be found!");

					await _subcategoryRepository.UpdateAsync(updateModel);

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
