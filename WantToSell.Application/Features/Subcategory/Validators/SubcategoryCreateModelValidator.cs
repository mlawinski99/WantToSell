using FluentValidation;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Features.Subcategory.Models;

namespace WantToSell.Application.Features.Subcategory.Validators
{
	public class SubcategoryCreateModelValidator : AbstractValidator<SubcategoryCreateModel>
	{
		private readonly ICategoryRepository _categoryRepository;

		public SubcategoryCreateModelValidator(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
			
			RuleFor(p => p.Name)
				.NotEmpty()
				.NotNull()
				.WithMessage("Name is required and can not be empty!");

			RuleFor(p => p.CategoryId)
				.NotEmpty()
				.NotNull()
				.WithMessage("Category is required and can not be empty!");

			RuleFor(p => p.CategoryId)
				.Must(IsCategoryExists)
				.WithMessage("Category does not exist!");
		}

		private bool IsCategoryExists(Guid categoryId)
		{
			return _categoryRepository.IsCategoryExists(categoryId);
		}
	}
}
