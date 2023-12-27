using FluentValidation;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Features.Subcategory.Models;

namespace WantToSell.Application.Features.Subcategory.Validators
{
	internal class SubcategoryUpdateModelValidator : AbstractValidator<SubcategoryUpdateModel>
	{
		public SubcategoryUpdateModelValidator()
		{
			RuleFor(p => p.Name)
				.NotEmpty()
				.NotNull()
				.WithMessage("Name is required and can not be empty!");

			RuleFor(p => p.CategoryId)
				.NotEmpty()
				.NotNull()
				.WithMessage("Category is required and can not be empty!");

			RuleFor(p => p.Id)
				.NotEmpty()
				.NotNull()
				.WithMessage("Id is required and can not be empty!");
		}
	}
}
