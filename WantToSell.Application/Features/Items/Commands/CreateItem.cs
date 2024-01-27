using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Features.Items.Models;
using WantToSell.Domain;

namespace WantToSell.Application.Features.Items.Commands;

public class CreateItem
{
    public record Command(ItemCreateModel Model) : IRequest<ItemDetailModel>;

    public class Handler : IRequestHandler<Command, ItemDetailModel>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public Handler(IMapper mapper, IItemRepository itemRepository)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
        }

        public async Task<ItemDetailModel> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Item>(request.Model);
            entity.Id = Guid.NewGuid();

            await _itemRepository.CreateAsync(entity);

            return _mapper.Map<ItemDetailModel>(entity);
        }
    }
}