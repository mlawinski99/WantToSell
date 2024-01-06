using AutoMapper;
using FluentAssertions;
using Moq;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Category.Commands;
using WantToSell.Application.Features.Category.Models;
using WantToSell.Application.Mappings;
using WantToSell.Application.UnitTests.Mocks;
using Xunit;

namespace WantToSell.Application.UnitTests.Category.Commands;

public class UpdateCategoryTests
{
    private readonly Mock<ICategoryRepository> _categoryMockRepository;
    private readonly IMapper _mapper;

    public UpdateCategoryTests()
    {
        _categoryMockRepository = MockCategoryRepository.GetCategoryRepositoryMock();

        var mapperConfiguration = new MapperConfiguration(c => { c.AddProfile<CategoryProfile>(); });

        _mapper = mapperConfiguration.CreateMapper();
    }

    [Fact]
    public async Task UpdateCategoryTest_ShouldPass()
    {
        // Arrange
        var updateModel = new CategoryUpdateModel
        {
            Id = Guid.Parse("961bfa68-9f14-4ec2-b86a-b787102b1e7f"),
            Name = "CategoryUpdate"
        };

        var handler = new UpdateCategory.Handler(_categoryMockRepository.Object, _mapper);

        // Act
        var result = await handler.Handle(new UpdateCategory.Command(updateModel), CancellationToken.None);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateCategoryTest_CategoryNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        var updateModel = new CategoryUpdateModel
        {
            Id = Guid.Parse("323f541d-49c4-47f9-a9f3-72bd61663380"),
            Name = "CategoryUpdate"
        };

        var handler = new UpdateCategory.Handler(_categoryMockRepository.Object, _mapper);

        // Act
        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(new UpdateCategory.Command(updateModel), CancellationToken.None));

        //Assert
        exception.Message.Should().Be("Category can not be found!");
        exception.Should().BeOfType<NotFoundException>();
    }

    [Fact]
    public async Task UpdateCategoryTest_CategoryNameExists_ShouldThrowBadRequestException()
    {
        // Arrange
        var updateModel = new CategoryUpdateModel
        {
            Id = Guid.Parse("acc6d73a-85c2-4dff-aa78-e6e044ea638f"),
            Name = "Category1"
        };

        var handler = new UpdateCategory.Handler(_categoryMockRepository.Object, _mapper);

        // Act
        var exception = await Assert.ThrowsAsync<BadRequestException>(() =>
            handler.Handle(new UpdateCategory.Command(updateModel), CancellationToken.None));

        //Assert
        exception.Message.Should().Be("Category name already exists!");
        exception.Should().BeOfType<BadRequestException>();
    }

    [Fact]
    public async Task UpdateCategoryTest_CategoryNotExists_ShouldThrowNotFoundException()
    {
        // Arrange
        var updateModel = new CategoryUpdateModel
        {
            Id = Guid.Parse("1111d73a-85c2-4dff-aa78-e6e044ea638f"),
            Name = "Category1"
        };

        var handler = new UpdateCategory.Handler(_categoryMockRepository.Object, _mapper);

        // Act
        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(new UpdateCategory.Command(updateModel), CancellationToken.None));

        //Assert
        exception.Message.Should().Be("Category can not be found!");
        exception.Should().BeOfType<NotFoundException>();
    }
}