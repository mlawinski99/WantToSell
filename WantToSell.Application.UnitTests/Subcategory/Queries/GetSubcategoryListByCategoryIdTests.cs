using AutoMapper;
using FluentAssertions;
using Moq;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Features.Subcategory.Commands;
using WantToSell.Application.Features.Subcategory.Models;
using WantToSell.Application.Mappings;
using WantToSell.Application.UnitTests.Mocks;
using Xunit;

namespace WantToSell.Application.UnitTests.Subcategory.Queries;

public class GetSubcategoryListByCategoryIdTests
{
    private readonly IMapper _mapper;
    private readonly Mock<ISubcategoryRepository> _subcategoryMockRepository;

    public GetSubcategoryListByCategoryIdTests()
    {
        _subcategoryMockRepository = MockSubcategoryRepository.GetSubcategoryRepositoryMock();

        var mapperConfiguration = new MapperConfiguration(c => { c.AddProfile<SubcategoryProfile>(); });

        _mapper = mapperConfiguration.CreateMapper();
    }

    [Fact]
    public async Task CreateCategoryTest_ShouldPass()
    {
        //Arrange
        var model = new SubcategoryCreateModel
        {
            Name = "Subcategory1",
            CategoryId = Guid.Parse("bc6a071f-c0d1-40e6-9889-528a74bde413")
        };

        var handler = new CreateSubcategory.Handler(_mapper, _subcategoryMockRepository.Object);

        // Act
        var result = await handler.Handle(new CreateSubcategory.Command(model), CancellationToken.None);

        // Assert
        result.Should().BeTrue();
    }
}