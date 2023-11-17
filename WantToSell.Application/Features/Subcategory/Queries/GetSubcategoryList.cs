using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WantToSell.Application.Contracts.Logging;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Features.Category.Models;
using WantToSell.Application.Features.Category.Queries;
using WantToSell.Application.Features.Subcategory.Models;

namespace WantToSell.Application.Features.Subcategory.Queries
{
	public class GetSubcategoryList
	{
		public record Query : IRequest<List<SubcategoryListModel>>;

		public class Handler : IRequestHandler<Query, List<SubcategoryListModel>>
		{
			private readonly IMapper _mapper;
			private readonly ISubcategoryRepository _subcategoryRepository;
			private readonly IApplicationLogger<GetSubcategoryList> _logger;

			public Handler(IMapper mapper,
				ISubcategoryRepository subcategoryRepository,
				IApplicationLogger<GetSubcategoryList> logger)
			{
				_mapper = mapper;
				_subcategoryRepository = subcategoryRepository;
				_logger = logger;
			}
			public async Task<List<SubcategoryListModel>> Handle(Query request, CancellationToken cancellationToken)
			{
				try
				{
					var result = await _subcategoryRepository.GetListAsync();

					return _mapper.Map<List<SubcategoryListModel>>(result);
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
