﻿using FluentValidation;
using WantToSell.Application.Features.Subcategory.Models;

namespace WantToSell.Application.Features.Subcategory.Validators;

public class SubcategoryCreateModelValidator : AbstractValidator<SubcategoryCreateModel>
{
    public SubcategoryCreateModelValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Name is required and can not be empty!");

        RuleFor(p => p.CategoryId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Category is required and can not be empty!");
    }
}