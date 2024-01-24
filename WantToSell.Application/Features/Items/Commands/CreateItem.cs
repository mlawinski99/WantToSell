using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Features.Items.Models;
using WantToSell.Domain;

namespace WantToSell.Application.Features.Items.Commands;

public class CreateItem
{
    public record Command(ItemCreateModel Model) : IRequest<bool>;

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public Handler(IMapper mapper, IItemRepository itemRepository)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Item>(request.Model);
            entity.Id = Guid.NewGuid();

            await _itemRepository.CreateAsync(entity);

            return true;
        }
    }
}