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

public class UpdateSubcategoryTests
{
    private readonly Mock<ICategoryRepository> _categoryMockRepository;
    private readonly IMapper _mapper;
    private readonly Mock<ISubcategoryRepository> _subcategoryMockRepository;

    public UpdateSubcategoryTests()
    {
        _subcategoryMockRepository = MockSubcategoryRepository.GetSubcategoryRepositoryMock();
        _categoryMockRepository = MockCategoryRepository.GetCategoryRepositoryMock();

        var mapperConfiguration = new MapperConfiguration(c => { c.AddProfile<SubcategoryProfile>(); });

        _mapper = mapperConfiguration.CreateMapper();
    }

    [Fact]
    public async Task UpdateSubcategoryTest_ShouldPass()
    {
        //Arrange
        var model = new SubcategoryUpdateModel
        {
            Id = Guid.Parse("e8be60f5-eb3b-4c66-b8ff-815c799bbb8a"),
            Name = "SubcategoryUpdate",
            CategoryId = Guid.Parse("961bfa68-9f14-4ec2-b86a-b787102b1e7f")
        };

        var category = _categoryMockRepository.Object.GetByIdAsync(model.CategoryId).Result;

        var handler =
            new UpdateSubcategory.Handler(_subcategoryMockRepository.Object, _mapper, _categoryMockRepository.Object);

        // Act
        var result = await handler.Handle(new UpdateSubcategory.Command(model), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(model.Name);
        result.CategoryName.Should().Be(category.Name);
    }

    [Fact]
    public async Task UpdateSubcategoryTest_CategoryNotExists_ShouldThrowNotFoundException()
    {
        //Arrange
        var model = new SubcategoryUpdateModel
        {
            Id = Guid.Parse("e8be60f5-eb3b-4c66-b8ff-815c799bbb8a"),
            Name = "SubcategoryUpdate",
            CategoryId = Guid.Parse("1111fa68-9f14-4ec2-b86a-b787102b1e7f")
        };

        var handler =
            new UpdateSubcategory.Handler(_subcategoryMockRepository.Object, _mapper, _categoryMockRepository.Object);

        // Act
        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(new UpdateSubcategory.Command(model), CancellationToken.None));

        // Assert
        exception.Message.Should().Be("Category can not be found!");
        exception.Should().BeOfType<NotFoundException>();
    }

    [Fact]
    public async Task UpdateSubcategoryTest_SubcategoryNameExists_ShouldThrowBadRequestException()
    {
        //Arrange
        var model = new SubcategoryUpdateModel
        {
            Id = Guid.Parse("e8be60f5-eb3b-4c66-b8ff-815c799bbb8a"),
            Name = "Subcategory2",
            CategoryId = Guid.Parse("961bfa68-9f14-4ec2-b86a-b787102b1e7f")
        };

        var handler =
            new UpdateSubcategory.Handler(_subcategoryMockRepository.Object, _mapper, _categoryMockRepository.Object);

        // Act
        var exception = await Assert.ThrowsAsync<BadRequestException>(() =>
            handler.Handle(new UpdateSubcategory.Command(model), CancellationToken.None));

        // Assert
        exception.Message.Should().Be("Subcategory name already exists!");
        exception.Should().BeOfType<BadRequestException>();
    }

    [Fact]
    public async Task UpdateSubcategoryTest_SubcategoryNotExists_ShouldThrownNotFoundException()
    {
        //Arrange
        var model = new SubcategoryUpdateModel
        {
            Id = Guid.Parse("111111f5-eb3b-4c66-b8ff-815c799bbb8a"),
            Name = "Subcategory2",
            CategoryId = Guid.Parse("961bfa68-9f14-4ec2-b86a-b787102b1e7f")
        };

        var handler =
            new UpdateSubcategory.Handler(_subcategoryMockRepository.Object, _mapper, _categoryMockRepository.Object);

        // Act
        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(new UpdateSubcategory.Command(model), CancellationToken.None));

        // Assert
        exception.Message.Should().Be("Subcategory can not be found!");
        exception.Should().BeOfType<NotFoundException>();
    }
}