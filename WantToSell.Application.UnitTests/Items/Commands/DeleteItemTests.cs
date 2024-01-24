using AutoMapper;
using FluentAssertions;
using Moq;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Items.Commands;
using WantToSell.Application.Mappings;
using WantToSell.Application.UnitTests.Mocks;
using Xunit;

namespace WantToSell.Application.UnitTests.Items.Commands;

public class DeleteItemTests
{
    private readonly Mock<IItemRepository> _itemMockRepository;
    private readonly IMapper _mapper;

    public DeleteItemTests()
    {
        _itemMockRepository = MockItemRepository.GetItemRepositoryMock();

        var mapperConfiguration = new MapperConfiguration(c => { c.AddProfile<ItemProfile>(); });

        _mapper = mapperConfiguration.CreateMapper();
    }

    [Fact]
    public async Task DeleteItemTests_ShouldPass()
    {
        //Arrange
        var existingItemId = new Guid("e1b0b6a0-0f7a-4e1e-9e1a-0b6b9e1a0b6b");
        var handler = new DeleteItem.Handler(_itemMockRepository.Object);

        // Act
        var result = await handler.Handle(new DeleteItem.Command(existingItemId), CancellationToken.None);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteItemTests_ItemNotFound_ShouldThrowNotFoundException()
    {
        //Arrange
        var notExistingItemId = new Guid("2222c140-1abd-4ba8-be8f-1c0a38802913");
        var handler = new DeleteItem.Handler(_itemMockRepository.Object);

        // Act
        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(new DeleteItem.Command(notExistingItemId), CancellationToken.None));

        // Assert
        exception.Message.Should().Be("Item can not be found!");
        exception.Should().BeOfType<NotFoundException>();
    }
}