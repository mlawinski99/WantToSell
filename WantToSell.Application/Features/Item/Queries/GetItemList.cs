using MediatR;
using WantToSell.Application.Contracts.Cache;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Features.Item.Filters;
using WantToSell.Application.Features.Items.Models;
using WantToSell.Application.Mappers.Items;
using WantToSell.Application.Models.Paging;

namespace WantToSell.Application.Features.Items.Queries;

public class GetItemList
{
    public record Query(ItemFilter Filter, Pager Pager) : IRequest<PagedList<ItemListModel>>;

    public class Handler : IRequestHandler<Query, PagedList<ItemListModel>>
    {
        private readonly ICacheHelper _cacheHelper;
        private readonly ItemListModelMapper _itemListModelMapper;
        private readonly IItemRepository _itemRepository;

        public Handler(ItemListModelMapper itemListModelMapper,
            IItemRepository itemRepository, ICacheHelper cacheHelper)
        {
            _itemListModelMapper = itemListModelMapper;
            _itemRepository = itemRepository;
            _cacheHelper = cacheHelper;
        }

        public async Task<PagedList<ItemListModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            var list = await _cacheHelper.GetOrSet("item-list",
                async () => { return await _itemRepository.GetFilteredListAsync(request.Filter, request.Pager); },
                TimeSpan.FromSeconds(10));

            var mappedList = (await _itemListModelMapper.Map(list.Items)).ToList();

            return new PagedList<ItemListModel>(mappedList, list.PageIndex, list.PageSize, list.SortColumn,
                list.Ascending, list.TotalRecords);
        }
    }
}