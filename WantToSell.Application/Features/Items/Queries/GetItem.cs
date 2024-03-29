﻿using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Contracts.Storage;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Items.Models;
using WantToSell.Application.Mappers.Items;

namespace WantToSell.Application.Features.Items.Queries;

public class GetItem
{
    public record Query(Guid Id) : IRequest<ItemDetailModel>;

    public class Handler : IRequestHandler<Query, ItemDetailModel>
    {
        private readonly IItemRepository _itemRepository;
        private readonly ItemDetailModelMapper _itemDetailModelMapper;

        public Handler(IItemRepository itemRepository, IFilesService service)
        {
             _itemDetailModelMapper = new ItemDetailModelMapper(service);
            _itemRepository = itemRepository;
        }

        public async Task<ItemDetailModel> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _itemRepository.GetByIdWithImages(request.Id);

            if (result == null)
                throw new NotFoundException("Item can not be found!");

            var mappedResult = await _itemDetailModelMapper.Map(result);
            
            return mappedResult;
        }
    }
}