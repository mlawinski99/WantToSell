using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WantToSell.Application.Contracts.DataAccess;
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

			public Handler(ICategoryRepository categoryRepository)
			{
				_categoryRepository = categoryRepository;
			}
			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{
				try
				{
					var entity = await _categoryRepository.GetByIdAsync(request.id);
					await _categoryRepository.DeleteAsync(entity);

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
