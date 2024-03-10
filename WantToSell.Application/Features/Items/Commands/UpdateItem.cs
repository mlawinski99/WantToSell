using System.Net;
using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Contracts.Storage;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Extensions;
using WantToSell.Application.Features.Items.Models;
using WantToSell.Application.Mappers;
using WantToSell.Application.Mappers.Files;
using WantToSell.Application.Mappers.Items;
using WantToSell.Application.Services;
using WantToSell.Domain;

namespace WantToSell.Application.Features.Items.Commands;

public static class UpdateItem
{
    public record Command(ItemUpdateModel Model) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IItemRepository _itemRepository;
        private readonly ItemUpdateModelMapper _itemUpdateMapper;
        private readonly IFilesService _filesService;
        private readonly IItemImagesService _itemImagesService;
        private readonly FileDetailMapper _fileDetailMapper;
        
        public Handler(IItemRepository itemRepository, 
            IFilesService filesService, IItemImagesService itemImagesService)
        {
            _itemRepository = itemRepository;
            _itemUpdateMapper = new ItemUpdateModelMapper();
            _filesService = filesService;
            _itemImagesService = itemImagesService;
            _fileDetailMapper = new FileDetailMapper(filesService);
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await _itemRepository.GetByIdWithImages(request.Model.Id);
            
            if (entity is null)
                throw new NotFoundException("Item can not be found!");

            var existingFiles = await _fileDetailMapper.Map(entity.StorageFiles);

            var fileHashDict = await _itemImagesService.GetFormFileHashPairs(request.Model.Images);
            var filesToAdd = _itemImagesService.GetImagesToAdd(existingFiles, fileHashDict);
            var filesToDelete = _itemImagesService.GetImagesToDelete(existingFiles, fileHashDict);
            
            var uploadedFiles = await _filesService.UploadFilesAsync(filesToAdd);
            
            //@todo background job delete from db + from storage
            _filesService.DeleteFiles(filesToDelete.Select(s => s.FilePath));
            await _itemImagesService.UpdateDatabase(filesToDelete, uploadedFiles, entity.Id);
            
            entity = await _itemUpdateMapper.Map(request.Model);
            await _itemRepository.UpdateAsync(entity);

            return Unit.Value;
        }
    }
}