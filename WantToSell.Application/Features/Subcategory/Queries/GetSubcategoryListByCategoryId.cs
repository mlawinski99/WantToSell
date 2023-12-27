﻿using AutoMapper;
using MediatR;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Subcategory.Models;

namespace WantToSell.Application.Features.Subcategory.Queries
{
	public class GetSubcategoryListByCategoryId
	{
		public record Query(Guid CategoryId) : IRequest<List<SubcategoryListModel>>;

		public class Handler : IRequestHandler<Query, List<SubcategoryListModel>>
		{
			private readonly IMapper _mapper;
			private readonly ISubcategoryRepository _subcategoryRepository;
			private readonly ICategoryRepository _categoryRepository;

			public Handler(IMapper mapper,
				ISubcategoryRepository subcategoryRepository,
				ICategoryRepository categoryRepository)
			{
				_mapper = mapper;
				_subcategoryRepository = subcategoryRepository;
				_categoryRepository = categoryRepository;
			}
			public async Task<List<SubcategoryListModel>> Handle(Query request, CancellationToken cancellationToken)
			{
				if (!_categoryRepository.IsCategoryExists(request.CategoryId))
					throw new BadRequestException("Category does not exist!");

				var result = await _subcategoryRepository.GetListByCategoryIdAsync(request.CategoryId);

				return _mapper.Map<List<SubcategoryListModel>>(result);
			}
		}
	}
}