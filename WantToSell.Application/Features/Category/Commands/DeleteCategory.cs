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
	public class DeleteCategory
	{
		public record Command(Guid id) : IRequest<bool>;

		public class Handler : IRequestHandler<Command, bool>
		{
			private readonly ICategoryRepository _categoryRepository;
			private readonly IApplicationLogger<DeleteCategory> _logger;

			public Handler(ICategoryRepository categoryRepository, IApplicationLogger<DeleteCategory> logger)
			{
				_categoryRepository = categoryRepository;
				_logger = logger;
			}
			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{
				try
				{
					var entity = await _categoryRepository.GetByIdAsync(request.id);

					if (entity == null)
						throw new NotFoundException($"Category does not exist!");

					await _categoryRepository.DeleteAsync(entity);

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
