﻿using FluentValidation;
using WantToSell.Application.Features.Items.Models;

namespace WantToSell.Application.Features.Items.Validators
{
	internal class ItemUpdateModelValidator : AbstractValidator<ItemUpdateModel>
	{
		public ItemUpdateModelValidator()
		{
			RuleFor(p => p.Id).NotEmpty().NotNull().WithMessage("Id is required and can not be empty!");
			RuleFor(p => p.Name).NotEmpty().NotNull().WithMessage("Name is required and can not be empty!");
			RuleFor(p => p.Description).NotEmpty().NotNull().WithMessage("Description is required and can not be empty!");
			RuleFor(p => p.DateExpiredUtc).NotEmpty().NotNull().WithMessage("Expiration date is required and can not be empty!");
			RuleFor(p => p.Condition).NotEmpty().NotNull().WithMessage("Condition is required and can not be empty!");
			RuleFor(p => p.CategoryId).NotEmpty().NotNull().WithMessage("Category is required and can not be empty!");
			RuleFor(p => p.SubcategoryId).NotEmpty().NotNull().WithMessage("Subcategory is required and can not be empty!");
		}
	}
}
