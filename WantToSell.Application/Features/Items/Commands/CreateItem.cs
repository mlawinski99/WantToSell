using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Contracts.Storage;
using WantToSell.Application.Features.Items.Models;
using WantToSell.Application.Mappers.Files;
using WantToSell.Application.Mappers.Items;
using WantToSell.Domain;

namespace WantToSell.Application.Features.Items.Commands;

public static class CreateItem
{
    public record Command(ItemCreateModel Model) : IRequest<ItemDetailModel>;

    public class Handler : IRequestHandler<Command, ItemDetailModel>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly IFilesService _filesService;
        private readonly StorageFileMapper _storageFilesMapper;
        private readonly ItemDetailModelMapper _itemDetailModelMapper;

        public Handler(IMapper mapper, IItemRepository itemRepository, IFilesService filesService)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
            _filesService = filesService;
            _storageFilesMapper = new StorageFileMapper();
            _itemDetailModelMapper = new ItemDetailModelMapper(_filesService);
        }

        public async Task<ItemDetailModel> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Item>(request.Model);
            entity.Id = Guid.NewGuid();

            var files = await _filesService.UploadFilesAsync(request.Model.Images);
            var storageFiles = await _storageFilesMapper.Map(files);

            entity.StorageFiles = storageFiles.ToList();
            await _itemRepository.CreateAsync(entity);

            return await _itemDetailModelMapper.Map(entity);
        }
    }
}