using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Items.Models;

namespace WantToSell.Application.Features.Items.Commands;

public class UpdateItem
{
    public record Command(ItemUpdateModel Model) : IRequest<ItemDetailModel>;

    public class Handler : IRequestHandler<Command, ItemDetailModel>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public Handler(IItemRepository itemRepository,
            IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<ItemDetailModel> Handle(Command request, CancellationToken cancellationToken)
        {
            var updateModel = await _itemRepository.GetByIdAsync(request.Model.Id);

            //@todo check itemId == userId or userIsAdmin

            if (updateModel == null)
                throw new NotFoundException("Item can not be found!");

            _mapper.Map(request.Model, updateModel);

            await _itemRepository.UpdateAsync(updateModel);

            return _mapper.Map<ItemDetailModel>(updateModel);
        }
    }
}