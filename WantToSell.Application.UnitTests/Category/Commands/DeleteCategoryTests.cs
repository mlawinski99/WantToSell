using FluentAssertions;
using Moq;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Category.Commands;
using WantToSell.Application.UnitTests.Mocks;
using Xunit;

namespace WantToSell.Application.UnitTests.Category.Commands;

public class DeleteCategoryTests
{
    private readonly Mock<ICategoryRepository> _categoryMockRepository;

    public DeleteCategoryTests()
    {
        _categoryMockRepository = MockCategoryRepository.GetCategoryRepositoryMock();
    }

    [Fact]
    public async Task DeleteCategoryTest_ValidRequest_ShouldReturnTrue()
    {
        var existingCategoryId = new Guid("961bfa68-9f14-4ec2-b86a-b787102b1e7f");

        var handler = new DeleteCategory.Handler(_categoryMockRepository.Object);

        // Act
        var result = await handler.Handle(new DeleteCategory.Command(existingCategoryId), CancellationToken.None);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteCategoryTest_CategoryNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        var notExistingCategoryId = new Guid("2222c140-1abd-4ba8-be8f-1c0a38802913");

        var handler = new DeleteCategory.Handler(_categoryMockRepository.Object);

        // Act
        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(new DeleteCategory.Command(notExistingCategoryId), CancellationToken.None));

        // Assert
        exception.Message.Should().Be("Category does not exist!");
        exception.Should().BeOfType<NotFoundException>();
    }
}