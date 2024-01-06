using FluentAssertions;
using Moq;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Subcategory.Commands;
using WantToSell.Application.UnitTests.Mocks;
using Xunit;

namespace WantToSell.Application.UnitTests.Subcategory.Commands;

public class DeleteSubcategoryTests
{
    private readonly Mock<ISubcategoryRepository> _subcategoryMockRepository;

    public DeleteSubcategoryTests()
    {
        _subcategoryMockRepository = MockSubcategoryRepository.GetSubcategoryRepositoryMock();
    }

    [Fact]
    public async Task DeleteSubcategoryTest_ValidRequest_ShouldReturnTrue()
    {
        var existingSubcategoryId = new Guid("e8be60f5-eb3b-4c66-b8ff-815c799bbb8a");

        var handler = new DeleteSubcategory.Handler(_subcategoryMockRepository.Object);

        // Act
        var result = await handler.Handle(new DeleteSubcategory.Command(existingSubcategoryId), CancellationToken.None);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteCategoryTest_CategoryNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        var notExistingSubcategoryId = new Guid("2222c140-1abd-4ba8-be8f-1c0a38802913");

        var handler = new DeleteSubcategory.Handler(_subcategoryMockRepository.Object);

        // Act
        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(new DeleteSubcategory.Command(notExistingSubcategoryId), CancellationToken.None));

        // Assert
        exception.Message.Should().Be("Subcategory does not exist!");
        exception.Should().BeOfType<NotFoundException>();
    }
}