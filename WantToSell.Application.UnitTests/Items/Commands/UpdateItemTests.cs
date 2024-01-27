using AutoMapper;
using FluentAssertions;
using Moq;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Items.Commands;
using WantToSell.Application.Features.Items.Models;
using WantToSell.Application.Mappings;
using WantToSell.Application.UnitTests.Mocks;
using Xunit;

namespace WantToSell.Application.UnitTests.Items.Commands;

public class UpdateItemTests
{
    private readonly Mock<IItemRepository> _itemMockRepository;
    private readonly IMapper _mapper;

    public UpdateItemTests()
    {
        _itemMockRepository = MockItemRepository.GetItemRepositoryMock();

        var mapperConfiguration = new MapperConfiguration(c => { c.AddProfile<ItemProfile>(); });

        _mapper = mapperConfiguration.CreateMapper();
    }

    [Fact]
    public async Task UpdateItemTests_ShouldPass()
    {
        //Arrange
        var model = new ItemUpdateModel
        {
            Id = Guid.Parse("e1b0b6a0-0f7a-4e1e-9e1a-0b6b9e1a0b6b"),
            Name = "ItemTest",
            Description = "ItemTest",
            DateExpiredUtc = DateTime.UtcNow.AddDays(1),
            CategoryId = Guid.NewGuid(),
            SubcategoryId = Guid.NewGuid(),
            Condition = "New"
        };

        var handler = new UpdateItem.Handler(_itemMockRepository.Object, _mapper);

        // Act
        var result = await handler.Handle(new UpdateItem.Command(model), CancellationToken.None);

        // Assert
        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(model.Id);
        result.Name.Should().Be(model.Name);
        result.Description.Should().Be(model.Description);
        result.DateExpiredUtc.Date.Should().Be(model.DateExpiredUtc.Date);
        result.CategoryId.Should().Be(model.CategoryId);
        result.SubcategoryId.Should().Be(model.SubcategoryId);
        result.Condition.Should().Be(model.Condition);
    }

    [Fact]
    public async Task UpdateItemTests_ItemNotFound_ShouldThrowNotFoundException()
    {
        //Arrange
        var model = new ItemUpdateModel
        {
            Id = Guid.NewGuid(),
            Name = "ItemTest",
            Description = "ItemTest",
            DateExpiredUtc = DateTime.UtcNow.AddDays(1),
            CategoryId = Guid.NewGuid(),
            SubcategoryId = Guid.NewGuid(),
            Condition = "New"
        };

        var handler = new UpdateItem.Handler(_itemMockRepository.Object, _mapper);

        // Act
        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(new UpdateItem.Command(model), CancellationToken.None));

        //Assert
        exception.Message.Should().Be("Item can not be found!");
        exception.Should().BeOfType<NotFoundException>();
    }
}