using FluentValidation;
using WantToSell.Application.Features.Address.Models;

namespace WantToSell.Application.Features.Address.Validators
{
	public class AddressUpdateModelValidator : AbstractValidator<AddressUpdateModel>
	{
		public AddressUpdateModelValidator()
		{
			RuleFor(p => p.Id).NotEmpty().NotNull().WithMessage("Id is required and can not be empty!");
			RuleFor(p => p.ApartmentNumber).NotEmpty().NotNull().WithMessage("ApartmentNumber is required and can not be empty!");
			RuleFor(p => p.Street).NotEmpty().NotNull().WithMessage("Street is required and can not be empty!");
			RuleFor(p => p.PostalCode).NotEmpty().NotNull().WithMessage("Postal Code is required and can not be empty!");
			RuleFor(p => p.City).NotEmpty().NotNull().WithMessage("City is required and can not be empty!");
		}
	}
}
