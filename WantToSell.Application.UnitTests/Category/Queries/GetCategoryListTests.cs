using AutoMapper;
using FluentAssertions;
using Moq;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Features.Category.Models;
using WantToSell.Application.Features.Category.Queries;
using WantToSell.Application.Mappings;
using WantToSell.Application.UnitTests.Mocks;
using Xunit;

namespace WantToSell.Application.UnitTests.Category.Queries;

public class GetCategoryListTests
{
    private readonly Mock<ICategoryRepository> _categoryMockRepository;
    private readonly IMapper _mapper;

    public GetCategoryListTests()
    {
        _categoryMockRepository = MockCategoryRepository.GetCategoryRepositoryMock();

        var mapperConfiguration = new MapperConfiguration(c => { c.AddProfile<CategoryProfile>(); });

        _mapper = mapperConfiguration.CreateMapper();
    }

    [Fact]
    public async Task GetCategoryListsTests_ShouldReturnList()
    {
        // Arrange
        var categoryList = await _categoryMockRepository.Object.GetListAsync();
        var mappedCategoryList = _mapper.Map<List<CategoryListModel>>(categoryList);
        var handler = new GetCategoryList.Handler(_mapper, _categoryMockRepository.Object);

        // Act
        var result = await handler.Handle(new GetCategoryList.Query(), CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(mappedCategoryList);
    }
}