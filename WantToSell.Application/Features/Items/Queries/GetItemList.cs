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
using WantToSell.Application.Features.Items.Models;

namespace WantToSell.Application.Features.Items.Queries
{
	public class GetItemList
	{
		public record Query : IRequest<List<ItemListModel>>;

		public class Handler : IRequestHandler<Query, List<ItemListModel>>
		{
			private readonly IMapper _mapper;
			private readonly IItemRepository _itemRepository;
			private readonly IApplicationLogger<GetItemList> _logger;

			public Handler(IMapper mapper,
				IItemRepository itemRepository,
				IApplicationLogger<GetItemList> logger)
			{
				_mapper = mapper;
				_itemRepository = itemRepository;
				_logger = logger;
			}
			public async Task<List<ItemListModel>> Handle(Query request, CancellationToken cancellationToken)
			{
				try
				{
					var result = await _itemRepository.GetListAsync();

					return _mapper.Map<List<ItemListModel>>(result);
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
