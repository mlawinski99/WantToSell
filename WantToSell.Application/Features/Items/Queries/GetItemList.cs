using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Persistence;
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

			public Handler(IMapper mapper,
				IItemRepository itemRepository)
			{
				_mapper = mapper;
				_itemRepository = itemRepository;
			}
			public async Task<List<ItemListModel>> Handle(Query request, CancellationToken cancellationToken)
			{
				var result = await _itemRepository.GetListAsync();

				return _mapper.Map<List<ItemListModel>>(result);
			}
		}
	}
}
