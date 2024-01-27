using AutoMapper;
using FluentAssertions;
using Moq;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Subcategory.Models;
using WantToSell.Application.Features.Subcategory.Queries;
using WantToSell.Application.Mappings;
using WantToSell.Application.UnitTests.Mocks;
using Xunit;

namespace WantToSell.Application.UnitTests.Subcategoryś.Queries;

public class GetSubcategoryTests
{
    private readonly Mock<ICategoryRepository> _categoryMockRepository;
    private readonly IMapper _mapper;
    private readonly Mock<ISubcategoryRepository> _subcategoryMockRepository;

    public GetSubcategoryTests()
    {
        _categoryMockRepository = MockCategoryRepository.GetCategoryRepositoryMock();
        _subcategoryMockRepository = MockSubcategoryRepository.GetSubcategoryRepositoryMock();

        var mapperConfiguration = new MapperConfiguration(c => { c.AddProfile<SubcategoryProfile>(); });

        _mapper = mapperConfiguration.CreateMapper();
    }

    [Fact]
    public async Task GetSubcategoryTest_ShouldPass()
    {
        // Arrange
        var existingSubcategoryId = Guid.Parse("e8be60f5-eb3b-4c66-b8ff-815c799bbb8a");

        var handler = new GetSubcategory.Handler(_mapper, _subcategoryMockRepository.Object);

        // Act
        var result = await handler.Handle(new GetSubcategory.Query(existingSubcategoryId), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Subcategory1");
    }

    [Fact]
    public async Task GetCategoryTests_ShouldThrowNotFoundException()
    {
        // Arrange
        var categoryId = Guid.Parse("e1b02222-0f7a-4e1e-9e1a-0b6b9e1a0b6b");
        var handler = new GetSubcategory.Handler(_mapper, _subcategoryMockRepository.Object);

        // Act
        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(new GetSubcategory.Query(categoryId), CancellationToken.None));

        // Assert
        exception.Message.Should().Be("Subcategory can not be found!");
        exception.Should().BeOfType<NotFoundException>();
    }
}