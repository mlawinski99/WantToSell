using AutoMapper;
using FluentAssertions;
using Moq;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Features.Category.Commands;
using WantToSell.Application.Features.Category.Models;
using WantToSell.Application.Mappings;
using WantToSell.Application.UnitTests.Mocks;
using Xunit;

namespace WantToSell.Application.UnitTests.Category.Commands;

public class CreateCategoryTests
{
    private readonly Mock<ICategoryRepository> _categoryMockRepository;
    private readonly IMapper _mapper;

    public CreateCategoryTests()
    {
        _categoryMockRepository = MockCategoryRepository.GetCategoryRepositoryMock();

        var mapperConfiguration = new MapperConfiguration(c => { c.AddProfile<CategoryProfile>(); });

        _mapper = mapperConfiguration.CreateMapper();
    }

    [Fact]
    public async Task CreateCategoryTest_ShouldPass()
    {
        //Arrange
        var model = new CategoryCreateModel
        {
            Name = "Category1"
        };

        var handler = new CreateCategory.Handler(_mapper, _categoryMockRepository.Object);

        // Act
        var result = await handler.Handle(new CreateCategory.Command(model), CancellationToken.None);

        // Assert
        result.Should().BeTrue();
    }
}