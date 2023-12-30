using AutoMapper;
using Moq;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Mappings;

namespace WantToSell.Application.UnitTests.Mocks;

public class MockAddressRepository
{
    public static Mock<IAddressRepository> GetAddressRepositoryMock()
    {
        var mapperConfiguration = new MapperConfiguration(c =>
        {
            c.AddProfile<AddressProfile>();
        });
        var mapper = mapperConfiguration.CreateMapper();
        
        var addressList = new List<Domain.Address>
        {
            new()
            {//Address of 1st user
                Id = new Guid("6565c140-1abd-4ba8-be8f-1c0a38802913"),
                City = "Katowice",
                Street = "Mickiewicza",
                PostalCode = "40-092",
                ApartmentNumber = "100",
                CreatedBy = new Guid("02f7af17-d128-41fb-977b-2dec061114c8"),
                DateCreatedUtc = DateTime.UtcNow,
                DateModifiedUtc = null
            },
            new()
            {//User for this address does not exists
                Id = new Guid("2a10a470-d9fa-4e1d-903e-ba559cbcd996"),
                City = "Katowice",
                Street = "Mickiewicza",
                PostalCode = "40-092",
                ApartmentNumber = "101",
                CreatedBy = new Guid("9d7a99b1-0a06-4840-96d3-10a8dbb1e78a"),
                DateCreatedUtc = DateTime.UtcNow,
                DateModifiedUtc = null
            }
        };

        var addressMockRepository = new Mock<IAddressRepository>();

        addressMockRepository.Setup(s => s.GetListAsync());
      
        addressMockRepository.Setup(s => s.GetAddressByUserId(It.IsAny<Guid>()))
            .ReturnsAsync((Guid userId) =>
            {
                return addressList
                    .FirstOrDefault(a => a.CreatedBy == userId);
            });
        
        addressMockRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id) =>
            {
                return addressList
                    .FirstOrDefault(a => a.Id == id);
            });
        
        addressMockRepository.Setup(s => s.IsExists(It.IsAny<Guid>()))
            .Returns((Guid userId) =>
            {
                return addressList
                    .Any(a => a.CreatedBy == userId);
            });  
        
        addressMockRepository.Setup(s => s.CreateAsync(It.IsAny<Domain.Address>()))
            .ReturnsAsync((Domain.Address address) =>
            {
                addressList.Add(address);
                
                return address;
            });
        
        addressMockRepository.Setup(s => s.UpdateAsync(It.IsAny<Domain.Address>()))
            .ReturnsAsync((Domain.Address address) =>
            {
                var existingAddress = addressList
                    .FirstOrDefault(s => s.Id == address.Id);

                mapper.Map(address, existingAddress);
                return address;
            });
        
        addressMockRepository.Setup(s => s.DeleteAsync(It.IsAny<Domain.Address>()))
            .Returns((Domain.Address address) =>
            {
                addressList.Remove(address);

                return Task.CompletedTask;
            });

        return addressMockRepository;
    }
}