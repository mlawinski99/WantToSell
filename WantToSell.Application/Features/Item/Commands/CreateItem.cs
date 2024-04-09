using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Contracts.Storage;
using WantToSell.Application.Features.Items.Models;
using WantToSell.Application.Mappers.Files;
using WantToSell.Application.Mappers.Item;
using WantToSell.Application.Mappers.Items;

namespace WantToSell.Application.Features.Items.Commands;

public static class CreateItem
{
    public record Command(ItemCreateModel Model) : IRequest<ItemDetailModel>;

    public class Handler : IRequestHandler<Command, ItemDetailModel>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IFilesService _filesService;
        private readonly StorageFileMapper _storageFilesMapper;
        private readonly ItemDetailModelMapper _itemDetailModelMapper;
        private readonly ItemMapper _itemMapper;

        public Handler(IItemRepository itemRepository,
            IFilesService filesService, StorageFileMapper storageFilesMapper,
            ItemDetailModelMapper itemDetailModelMapper, ItemMapper itemMapper)
        {
            _itemRepository = itemRepository;
            _filesService = filesService;
            _storageFilesMapper = storageFilesMapper;
            _itemDetailModelMapper = itemDetailModelMapper;
            _itemMapper = itemMapper;
        }

        public async Task<ItemDetailModel> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await _itemMapper.Map(request.Model, new Domain.Item());
            var files = await _filesService.UploadFilesAsync(request.Model.Images);
            var storageFiles = await _storageFilesMapper.Map(files);

            entity.StorageFiles = storageFiles.ToList();
            await _itemRepository.CreateAsync(entity);

            return await _itemDetailModelMapper.Map(entity);
        }
    }
}