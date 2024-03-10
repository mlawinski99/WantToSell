using AutoMapper;
using FluentAssertions;
using Moq;
using WantToSell.Application.Contracts.Identity;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Address.Commands;
using WantToSell.Application.Features.Address.Models;
using WantToSell.Application.Mappings;
using WantToSell.Application.UnitTests.Mocks;
using Xunit;

namespace WantToSell.Application.UnitTests.Address.Commands;

public class CreateAddressTests
{
    private readonly Mock<IAddressRepository> _addressMockRepository;
    private readonly IMapper _mapper;
    private readonly Mock<IUserService> _userMockRepository;

    public CreateAddressTests()
    {
        _addressMockRepository = MockAddressRepository.GetAddressRepositoryMock();
        _userMockRepository = MockUserRepository.GetUserRepositoryMock();

        var mapperConfiguration = new MapperConfiguration(c => { c.AddProfile<AddressProfile>(); });

        _mapper = mapperConfiguration.CreateMapper();
    }

    [Fact]
    public async Task CreateAddressTest_UserWithoutAddress_ShouldPass()
    {
        //Arrange
        var model = new AddressCreateModel
        {
            City = "Katowice",
            Street = "Mickiewicza",
            PostalCode = "40-092",
            ApartmentNumber = "30"
        };

        var mockUserContext = new Mock<IUserContext>();
        var userId = _userMockRepository.Object.GetUsers().Result.Last().Id; //User without address
        mockUserContext.SetupGet(u => u.UserId).Returns(Guid.Parse(userId));

        var handler = new CreateAddress.Handler(_mapper, _addressMockRepository.Object, mockUserContext.Object);

        // Act
        var result = await handler.Handle(new CreateAddress.Command(model), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.City.Should().Be(model.City);
        result.Street.Should().Be(model.Street);
        result.PostalCode.Should().Be(model.PostalCode);
        result.ApartmentNumber.Should().Be(model.ApartmentNumber);
    }

    [Fact]
    public async Task CreateAddressTest_UserWithAddress_ShouldThrowBadRequest()
    {
        //Arrange
        var model = new AddressCreateModel
        {
            City = "Katowice",
            Street = "Mickiewicza",
            PostalCode = "40-092",
            ApartmentNumber = "30"
        };

        var mockUserContext = new Mock<IUserContext>();
        var userId = _userMockRepository.Object.GetUsers().Result.First().Id; //User with address
        mockUserContext.SetupGet(u => u.UserId).Returns(Guid.Parse(userId));

        var handler = new CreateAddress.Handler(_mapper, _addressMockRepository.Object, mockUserContext.Object);

        // Act
        var exception = await Assert.ThrowsAsync<BadRequestException>(() =>
            handler.Handle(new CreateAddress.Command(model), CancellationToken.None));

        // Assert
        exception.Should().BeOfType<BadRequestException>();
        exception.Message.Should().Be("Address already exists!");
    }
}