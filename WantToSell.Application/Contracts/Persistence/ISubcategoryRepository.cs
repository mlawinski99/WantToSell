﻿using WantToSell.Domain;

namespace WantToSell.Application.Contracts.Persistence;

public interface ISubcategoryRepository : IGenericRepository<Subcategory>
{
	Task<List<Subcategory>> GetListByCategoryIdAsync(Guid categoryId);
}