using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Items.Models;

namespace WantToSell.Application.Features.Items.Queries;

public class GetItem
{
    public record Query(Guid Id) : IRequest<ItemDetailModel>;

    public class Handler : IRequestHandler<Query, ItemDetailModel>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public Handler(IMapper mapper,
            IItemRepository itemRepository)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
        }

        public async Task<ItemDetailModel> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _itemRepository.GetByIdAsync(request.Id);

            if (result == null)
                throw new NotFoundException("Item can not be found!");

            return _mapper.Map<ItemDetailModel>(result);
        }
    }
}