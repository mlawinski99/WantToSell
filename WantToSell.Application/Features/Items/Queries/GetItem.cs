﻿using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WantToSell.Application.Contracts.Logging;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Address.Models;
using WantToSell.Application.Features.Address.Queries;
using WantToSell.Application.Features.Items.Models;

namespace WantToSell.Application.Features.Items.Queries
{
	public class GetItem
	{
		public record Query(Guid id) : IRequest<ItemDetailModel>;

		public class Handler : IRequestHandler<Query, ItemDetailModel>
		{
			private readonly IMapper _mapper;
			private readonly IItemRepository _itemRepository;
			private readonly IApplicationLogger<GetItem> _logger;

			public Handler(IMapper mapper,
				IItemRepository itemRepository,
				IApplicationLogger<GetItem> logger)
			{
				_mapper = mapper;
				_itemRepository = itemRepository;
				_logger = logger;
			}
			public async Task<ItemDetailModel> Handle(Query request, CancellationToken cancellationToken)
			{
				try
				{
					var result = await _itemRepository.GetByIdAsync(request.id);

					return _mapper.Map<ItemDetailModel>(result);
				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message, ex);
					throw;
				}
			}
		}
	}
}
