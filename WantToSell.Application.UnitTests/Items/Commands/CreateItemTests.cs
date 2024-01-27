using AutoMapper;
using FluentAssertions;
using Moq;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Features.Items.Commands;
using WantToSell.Application.Features.Items.Models;
using WantToSell.Application.Mappings;
using WantToSell.Application.UnitTests.Mocks;
using Xunit;

namespace WantToSell.Application.UnitTests.Items.Commands;

public class CreateItemTests
{
    private readonly Mock<IItemRepository> _itemMockRepository;
    private readonly IMapper _mapper;

    public CreateItemTests()
    {
        _itemMockRepository = MockItemRepository.GetItemRepositoryMock();

        var mapperConfiguration = new MapperConfiguration(c => { c.AddProfile<ItemProfile>(); });

        _mapper = mapperConfiguration.CreateMapper();
    }

    [Fact]
    public async Task CreateItemTests_ShouldPass()
    {
        //Arrange
        var model = new ItemCreateModel
        {
            Name = "ItemTest",
            Description = "ItemTest",
            DateExpiredUtc = DateTime.UtcNow.AddDays(1),
            CategoryId = Guid.NewGuid(),
            SubcategoryId = Guid.NewGuid(),
            Condition = "New"
        };

        var handler = new CreateItem.Handler(_mapper, _itemMockRepository.Object);

        // Act
        var result = await handler.Handle(new CreateItem.Command(model), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(model.Name);
        result.Description.Should().Be(model.Description);
        result.DateExpiredUtc.Date.Should().Be(model.DateExpiredUtc.Date);
        result.CategoryId.Should().Be(model.CategoryId);
        result.SubcategoryId.Should().Be(model.SubcategoryId);
        result.Condition.Should().Be(model.Condition);
    }
}