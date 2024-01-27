using AutoMapper;
using FluentAssertions;
using Moq;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Category.Queries;
using WantToSell.Application.Mappings;
using WantToSell.Application.UnitTests.Mocks;
using Xunit;

namespace WantToSell.Application.UnitTests.Category.Queries;

public class GetCategoryTests
{
    private readonly Mock<ICategoryRepository> _categoryMockRepository;
    private readonly IMapper _mapper;

    public GetCategoryTests()
    {
        _categoryMockRepository = MockCategoryRepository.GetCategoryRepositoryMock();

        var mapperConfiguration = new MapperConfiguration(c => { c.AddProfile<CategoryProfile>(); });

        _mapper = mapperConfiguration.CreateMapper();
    }

    [Fact]
    public async Task GetCategoryTest_ShouldPass()
    {
        // Arrange
        var existingCategoryId = Guid.Parse("961bfa68-9f14-4ec2-b86a-b787102b1e7f");

        var handler = new GetCategory.Handler(_mapper, _categoryMockRepository.Object);

        // Act
        var result = await handler.Handle(new GetCategory.Query(existingCategoryId), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Category1");
    }

    [Fact]
    public async Task GetCategoryTests_ShouldThrowNotFoundException()
    {
        // Arrange
        var categoryId = Guid.Parse("e1b02222-0f7a-4e1e-9e1a-0b6b9e1a0b6b");
        var handler = new GetCategory.Handler(_mapper, _categoryMockRepository.Object);

        // Act
        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(new GetCategory.Query(categoryId), CancellationToken.None));

        // Assert
        exception.Message.Should().Be("Category can not be found!");
        exception.Should().BeOfType<NotFoundException>();
    }
}