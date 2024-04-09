using WantToSell.Application.Features.Address.Models;
using WantToSell.Domain.Mappers;

namespace WantToSell.Application.Mappers.Address;

public class AddressMapper :
    IMapper<AddressCreateModel, Domain.Address>,
    IMapper<AddressUpdateModel, Domain.Address>
{
    public async Task<Domain.Address> Map(AddressCreateModel model, Domain.Address? address = null)
    {
        return new Domain.Address
        {
            ApartmentNumber = model.ApartmentNumber,
            City = model.City,
            Street = model.Street,
            PostalCode = model.PostalCode
        };
    }

    public async Task<Domain.Address> Map(AddressUpdateModel model, Domain.Address address)
    {
        address.PostalCode = model.PostalCode;
        address.City = model.City;
        address.Street = model.Street;
        address.Id = model.Id;
        address.ApartmentNumber = model.ApartmentNumber;

        return address;
    }
}