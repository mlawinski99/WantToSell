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
using WantToSell.Domain.Interfaces;
using Xunit;

namespace WantToSell.Application.UnitTests.Address.Commands;

public class UpdateAddressTests
{
    private readonly Mock<IAddressRepository> _addressMockRepository;
    private readonly IMapper _mapper;
    private readonly Mock<IUserService> _userMockRepository;

    public UpdateAddressTests()
    {
        _addressMockRepository = MockAddressRepository.GetAddressRepositoryMock();
        _userMockRepository = MockUserRepository.GetUserRepositoryMock();

        var mapperConfiguration = new MapperConfiguration(c => { c.AddProfile<AddressProfile>(); });

        _mapper = mapperConfiguration.CreateMapper();
    }

    [Fact]
    public async Task UpdateAddressTest_UserWithAddress_ShouldPass()
    {
        // Arrange
        var updateModel = new AddressUpdateModel
        {
            Id = Guid.Parse("6565c140-1abd-4ba8-be8f-1c0a38802913"),
            City = "Katowice",
            Street = "Mickiewicza",
            PostalCode = "40-092",
            ApartmentNumber = "30"
        };

        var mockUserContext = new Mock<IUserContext>();
        var userId = _userMockRepository.Object.GetUsers().Result.First().Id; //User with address
        mockUserContext.SetupGet(u => u.UserId).Returns(Guid.Parse(userId));

        var handler = new UpdateAddress.Handler(_addressMockRepository.Object, mockUserContext.Object, _mapper);

        // Act
        var result = await handler.Handle(new UpdateAddress.Command(updateModel), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.City.Should().Be(updateModel.City);
        result.Street.Should().Be(updateModel.Street);
        result.PostalCode.Should().Be(updateModel.PostalCode);
        result.ApartmentNumber.Should().Be(updateModel.ApartmentNumber);
    }

    [Fact]
    public async Task UpdateAddressTest_AddressNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        var updateModel = new AddressUpdateModel
        {
            Id = Guid.Parse("2222d140-1abd-4ba8-be8f-1c0a38802913"),
            City = "Katowice",
            Street = "Mickiewicza",
            PostalCode = "40-092",
            ApartmentNumber = "30"
        };

        var mockUserContext = new Mock<IUserContext>();
        var userId = _userMockRepository.Object.GetUsers().Result.Last().Id; //User without address
        mockUserContext.SetupGet(u => u.UserId).Returns(Guid.Parse(userId));

        var handler = new UpdateAddress.Handler(_addressMockRepository.Object, mockUserContext.Object, _mapper);

        //Act
        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(new UpdateAddress.Command(updateModel), CancellationToken.None));

        //Assert
        exception.Message.Should().Be("Address can not be found!");
        exception.Should().BeOfType<NotFoundException>();
    }

    [Fact]
    public async Task UpdateAddressTest_AccessDenied_ShouldThrowAccessDeniedException()
    {
        // Arrange
        var updateModel = new AddressUpdateModel
        {
            Id = Guid.Parse("2a10a470-d9fa-4e1d-903e-ba559cbcd996"),
            City = "Katowice",
            Street = "Mickiewicza",
            PostalCode = "40-092",
            ApartmentNumber = "30"
        };

        var mockUserContext = new Mock<IUserContext>();
        var userId = _userMockRepository.Object.GetUsers().Result.First().Id; //User with address
        mockUserContext.SetupGet(u => u.UserId).Returns(Guid.Parse(userId));

        var handler = new UpdateAddress.Handler(_addressMockRepository.Object, mockUserContext.Object, _mapper);

        //Act
        var exception = await Assert.ThrowsAsync<AccessDeniedException>(() =>
            handler.Handle(new UpdateAddress.Command(updateModel), CancellationToken.None));

        //Assert
        exception.Should().BeOfType<AccessDeniedException>();
    }
}