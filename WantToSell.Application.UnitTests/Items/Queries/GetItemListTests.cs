using AutoMapper;
using FluentAssertions;
using Moq;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Features.Items.Models;
using WantToSell.Application.Features.Items.Queries;
using WantToSell.Application.Mappings;
using WantToSell.Application.UnitTests.Mocks;
using Xunit;

namespace WantToSell.Application.UnitTests.Items.Queries;

public class GetItemListTests
{
    private readonly Mock<IItemRepository> _itemMockRepository;
    private readonly IMapper _mapper;

    public GetItemListTests()
    {
        _itemMockRepository = MockItemRepository.GetItemRepositoryMock();

        var mapperConfiguration = new MapperConfiguration(c => { c.AddProfile<ItemProfile>(); });

        _mapper = mapperConfiguration.CreateMapper();
    }

    [Fact]
    public async Task GetItemListTests_ShouldReturnList()
    {
        //Arrange
        var itemList = await _itemMockRepository.Object.GetListAsync();
        var mappedList = _mapper.Map<List<ItemListModel>>(itemList);
        var handler = new GetItemList.Handler(_mapper, _itemMockRepository.Object);

        // Act
        var result = await handler.Handle(new GetItemList.Query(), CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(mappedList);
        result.Should().HaveCount(2);
    }
}