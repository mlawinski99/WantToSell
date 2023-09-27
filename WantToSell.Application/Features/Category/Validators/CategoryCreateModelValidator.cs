using FluentValidation;
using WantToSell.Application.Features.Category.Models;

namespace WantToSell.Application.Features.Category.Validators;

public class CategoryCreateModelValidator : AbstractValidator<CategoryCreateModel>
{
    public CategoryCreateModelValidator()
    {
        RuleFor(p => p.Name).NotEmpty().NotNull().WithMessage("Name is required and can not be empty!");
    }
}