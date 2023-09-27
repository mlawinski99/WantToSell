using FluentValidation;
using WantToSell.Application.Features.Category.Models;

namespace WantToSell.Application.Features.Category.Validators;

public class CategoryUpdateModelValidator : AbstractValidator<CategoryUpdateModel>
{
    public CategoryUpdateModelValidator()
    {
        RuleFor(p => p.Id).NotNull().WithMessage("Id is required!");
        RuleFor(p => p.Name).NotEmpty().NotNull().WithMessage("Name is required and can not be empty!");
    }
}