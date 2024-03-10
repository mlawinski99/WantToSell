using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Contracts.Storage;
using WantToSell.Application.Exceptions;

namespace WantToSell.Application.Features.Items.Commands;

public static class DeleteItem
{
    public record Command(Guid Id) : IRequest<bool>;

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IStorageFileRepository _storageFileRepository;
        private readonly IFilesService _filesService;

        public Handler(IItemRepository itemRepository, IFilesService filesService, IStorageFileRepository storageFileRepository)
        {
            _itemRepository = itemRepository;
            _filesService = filesService;
            _storageFileRepository = storageFileRepository;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await _itemRepository.GetByIdAsync(request.Id);

            if (entity == null)
                throw new NotFoundException("Item can not be found!");

            var imagePaths = await _storageFileRepository.GetItemFilesPaths(entity.Id);

            await _itemRepository.DeleteAsync(entity);
            _filesService.DeleteFiles(imagePaths);
            
            return true;
        }
    }
}