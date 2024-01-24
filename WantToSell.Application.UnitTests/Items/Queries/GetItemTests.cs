using AutoMapper;
using FluentAssertions;
using Moq;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Items.Models;
using WantToSell.Application.Features.Items.Queries;
using WantToSell.Application.Mappings;
using WantToSell.Application.UnitTests.Mocks;
using Xunit;

namespace WantToSell.Application.UnitTests.Items.Queries;

public class GetItemTests
{
    private readonly Mock<IItemRepository> _itemMockRepository;
    private readonly IMapper _mapper;

    public GetItemTests()
    {
        _itemMockRepository = MockItemRepository.GetItemRepositoryMock();

        var mapperConfiguration = new MapperConfiguration(c => { c.AddProfile<ItemProfile>(); });

        _mapper = mapperConfiguration.CreateMapper();
    }

    [Fact]
    public async Task GetItemTests_ShouldReturn()
    {
        // Arrange
        var itemId = Guid.Parse("e1b0b6a0-0f7a-4e1e-9e1a-0b6b9e1a0b6b");
        var handler = new GetItem.Handler(_mapper, _itemMockRepository.Object);

        // Act
        var result = await handler.Handle(new GetItem.Query(itemId), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ItemDetailModel>();
        result.Id.Should().Be(itemId);
    }

    [Fact]
    public async Task GetItemTests_ShouldThrowNotFoundException()
    {
        // Arrange
        var itemId = Guid.Parse("e1b02222-0f7a-4e1e-9e1a-0b6b9e1a0b6b");
        var handler = new GetItem.Handler(_mapper, _itemMockRepository.Object);

        // Act
        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(new GetItem.Query(itemId), CancellationToken.None));

        // Assert
        exception.Message.Should().Be("Item can not be found!");
        exception.Should().BeOfType<NotFoundException>();
    }
}