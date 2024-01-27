using AutoMapper;
using FluentAssertions;
using Moq;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Subcategory.Commands;
using WantToSell.Application.Features.Subcategory.Models;
using WantToSell.Application.Mappings;
using WantToSell.Application.UnitTests.Mocks;
using Xunit;

namespace WantToSell.Application.UnitTests.Subcategory.Commands;

public class CreateSubcategoryTests
{
    private readonly Mock<ICategoryRepository> _categoryMockRepository;
    private readonly IMapper _mapper;
    private readonly Mock<ISubcategoryRepository> _subcategoryMockRepository;

    public CreateSubcategoryTests()
    {
        _subcategoryMockRepository = MockSubcategoryRepository.GetSubcategoryRepositoryMock();
        _categoryMockRepository = MockCategoryRepository.GetCategoryRepositoryMock();

        var mapperConfiguration = new MapperConfiguration(c => { c.AddProfile<SubcategoryProfile>(); });

        _mapper = mapperConfiguration.CreateMapper();
    }

    [Fact]
    public async Task CreateSubcategoryTest_ShouldPass()
    {
        //Arrange
        var model = new SubcategoryCreateModel
        {
            Name = "SubcategoryCreate",
            CategoryId = Guid.Parse("961bfa68-9f14-4ec2-b86a-b787102b1e7f")
        };

        var category = _categoryMockRepository.Object.GetByIdAsync(model.CategoryId).Result;

        var handler =
            new CreateSubcategory.Handler(_mapper, _subcategoryMockRepository.Object, _categoryMockRepository.Object);

        // Act
        var result = await handler.Handle(new CreateSubcategory.Command(model), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(model.Name);
        result.CategoryName.Should().Be(category.Name);
    }

    [Fact]
    public async Task CreateSubcategoryTest_CategoryNotExists_ShouldThrowNotFoundException()
    {
        //Arrange
        var model = new SubcategoryCreateModel
        {
            Name = "Subcategory1",
            CategoryId = Guid.Parse("11111a68-9f14-4ec2-b86a-b787102b1e7f")
        };

        var handler =
            new CreateSubcategory.Handler(_mapper, _subcategoryMockRepository.Object, _categoryMockRepository.Object);

        // Act
        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(new CreateSubcategory.Command(model), CancellationToken.None));

        // Assert
        exception.Message.Should().Be("Category can not be found!");
        exception.Should().BeOfType<NotFoundException>();
    }

    [Fact]
    public async Task CreateSubcategoryTest_SubcategoryNameExists_ShouldThrowBadRequestException()
    {
        //Arrange
        var model = new SubcategoryCreateModel
        {
            Name = "Subcategory1",
            CategoryId = Guid.Parse("961bfa68-9f14-4ec2-b86a-b787102b1e7f")
        };

        var handler =
            new CreateSubcategory.Handler(_mapper, _subcategoryMockRepository.Object, _categoryMockRepository.Object);

        // Act
        var exception = await Assert.ThrowsAsync<BadRequestException>(() =>
            handler.Handle(new CreateSubcategory.Command(model), CancellationToken.None));

        // Assert
        exception.Message.Should().Be("Subcategory name already exists!");
        exception.Should().BeOfType<BadRequestException>();
    }
}