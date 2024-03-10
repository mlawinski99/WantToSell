using AutoMapper;
using FluentAssertions;
using Moq;
using WantToSell.Application.Contracts.Identity;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Address.Commands;
using WantToSell.Application.Mappings;
using WantToSell.Application.UnitTests.Mocks;
using Xunit;

namespace WantToSell.Application.UnitTests.Address.Commands;

public class DeleteAddressTests
{
    private readonly Mock<IAddressRepository> _addressMockRepository;
    private readonly IMapper _mapper;
    private readonly Mock<IUserService> _userMockRepository;

    public DeleteAddressTests()
    {
        _addressMockRepository = MockAddressRepository.GetAddressRepositoryMock();
        _userMockRepository = MockUserRepository.GetUserRepositoryMock();

        var mapperConfiguration = new MapperConfiguration(c => { c.AddProfile<AddressProfile>(); });

        _mapper = mapperConfiguration.CreateMapper();
    }

    [Fact]
    public async Task DeleteAddressTest_ValidRequest_ShouldReturnTrue()
    {
        var existingAddressId = new Guid("6565c140-1abd-4ba8-be8f-1c0a38802913");

        var mockUserContext = new Mock<IUserContext>();
        var userId = _userMockRepository.Object.GetUsers().Result.First().Id; //User with address
        mockUserContext.SetupGet(u => u.UserId).Returns(Guid.Parse(userId));

        var handler = new DeleteAddress.Handler(_addressMockRepository.Object, mockUserContext.Object);

        // Act
        var result = await handler.Handle(new DeleteAddress.Command(existingAddressId), CancellationToken.None);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAddressTest_AddressNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        var notExistingAddresId = new Guid("2222c140-1abd-4ba8-be8f-1c0a38802913");

        var mockUserContext = new Mock<IUserContext>();
        var userId = _userMockRepository.Object.GetUsers().Result.First().Id; //User with address
        mockUserContext.SetupGet(u => u.UserId).Returns(Guid.Parse(userId));

        var handler = new DeleteAddress.Handler(_addressMockRepository.Object, mockUserContext.Object);

        // Act
        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(new DeleteAddress.Command(notExistingAddresId), CancellationToken.None));

        // Assert
        exception.Message.Should().Be("Address does not exist!");
        exception.Should().BeOfType<NotFoundException>();
    }

    [Fact]
    public async Task DeleteAddressTest_AccessDenied_ShouldThrowAccessDeniedException()
    {
        // Arrange
        var existingAddressId = new Guid("6565c140-1abd-4ba8-be8f-1c0a38802913");

        var mockUserContext = new Mock<IUserContext>();
        var userId = _userMockRepository.Object.GetUsers().Result.Last().Id; //User without address
        mockUserContext.SetupGet(u => u.UserId).Returns(Guid.Parse(userId));

        var handler = new DeleteAddress.Handler(_addressMockRepository.Object, mockUserContext.Object);

        // Act
        var exception = await Assert.ThrowsAsync<AccessDeniedException>(() =>
            handler.Handle(new DeleteAddress.Command(existingAddressId), CancellationToken.None));

        //Assert
        exception.Should().BeOfType<AccessDeniedException>();
    }
}