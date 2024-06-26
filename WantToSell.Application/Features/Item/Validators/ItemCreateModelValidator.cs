﻿using FluentValidation;
using WantToSell.Application.Features.Items.Models;

namespace WantToSell.Application.Features.Items.Validators;

public class ItemCreateModelValidator : AbstractValidator<ItemCreateModel>
{
    public ItemCreateModelValidator()
    {
        RuleFor(p => p.Name).NotEmpty().NotNull().WithMessage("Name is required and can not be empty!");
        RuleFor(p => p.Description).NotEmpty().NotNull().WithMessage("Description is required and can not be empty!");
        RuleFor(p => p.DateExpiredUtc)
            .NotEmpty().WithMessage("Expiration date is required and can not be empty!")
            .NotNull().WithMessage("Expiration date is required and can not be empty!")
            .GreaterThan(DateTime.UtcNow).WithMessage("Expiration date must be greater than current date!");
        RuleFor(p => p.Condition).NotEmpty().NotNull().WithMessage("Condition is required and can not be empty!");
        RuleFor(p => p.CategoryId).NotEmpty().NotNull().WithMessage("Category is required and can not be empty!");
        RuleFor(p => p.SubcategoryId).NotEmpty().NotNull().WithMessage("Subcategory is required and can not be empty!");
    }
}