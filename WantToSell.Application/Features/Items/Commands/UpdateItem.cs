using MediatR;
using AutoMapper;
using WantToSell.Application.Contracts.Logging;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Items.Models;
using WantToSell.Application.Features.Items.Validators;

namespace WantToSell.Application.Features.Items.Commands
{
	public class UpdateItem
	{
		public record Command(ItemUpdateModel Model) : IRequest<bool>;

		public class Handler : IRequestHandler<Command, bool>
		{
			private readonly IMapper _mapper;
			private readonly IItemRepository _itemRepository;

			public Handler(IItemRepository itemRepository, 
				IMapper mapper)
			{
				_itemRepository = itemRepository;
				_mapper = mapper;
			}
			public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
			{
				var updateModel = await _itemRepository.GetByIdAsync(request.Model.Id);
				
				//@todo
				//check itemId == userId or userIsAdmin
				
				if (updateModel == null)
					throw new NotFoundException("Item can not be found!");

				_mapper.Map(request.Model, updateModel);
				
				await _itemRepository.UpdateAsync(updateModel);

				return true;
			}
		}
	}
}
