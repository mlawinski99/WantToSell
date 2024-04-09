using WantToSell.Application.Features.Address.Models;
using WantToSell.Domain.Mappers;

namespace WantToSell.Application.Mappers.Address;

public class AddressDetailModelMapper :
    IMapper<Domain.Address, AddressDetailModel>
{
    public async Task<AddressDetailModel> Map(Domain.Address model, AddressDetailModel? resultModel = null)
    {
        return new AddressDetailModel()
        {
            Id = model.Id,
            Street = model.Street,
            ApartmentNumber = model.ApartmentNumber,
            PostalCode = model.PostalCode,
            City = model.City
        };
    }
}