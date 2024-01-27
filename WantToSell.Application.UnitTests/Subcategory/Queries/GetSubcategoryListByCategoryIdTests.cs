using AutoMapper;
using FluentAssertions;
using Moq;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Features.Subcategory.Models;
using WantToSell.Application.Features.Subcategory.Queries;
using WantToSell.Application.Mappings;
using WantToSell.Application.UnitTests.Mocks;
using Xunit;

namespace WantToSell.Application.UnitTests.Subcategory.Queries;

public class GetSubcategoryListByCategoryIdTests
{
    private readonly Mock<ICategoryRepository> _categoryMockRepository;
    private readonly IMapper _mapper;
    private readonly Mock<ISubcategoryRepository> _subcategoryMockRepository;

    public GetSubcategoryListByCategoryIdTests()
    {
        _subcategoryMockRepository = MockSubcategoryRepository.GetSubcategoryRepositoryMock();
        _categoryMockRepository = MockCategoryRepository.GetCategoryRepositoryMock();

        var mapperConfiguration = new MapperConfiguration(c => { c.AddProfile<SubcategoryProfile>(); });

        _mapper = mapperConfiguration.CreateMapper();
    }

    [Fact]
    public async Task GetSubcategoryListByCategoryIdTests_ValidCategoryId_ShouldReturnList()
    {
        // Arrange
        var subcategoryList =
            await _subcategoryMockRepository.Object.GetListByCategoryIdAsync(
                Guid.Parse("961bfa68-9f14-4ec2-b86a-b787102b1e7f"));
        var mappedSubcategoryList = _mapper.Map<List<SubcategoryViewModel>>(subcategoryList);
        var handler = new GetSubcategoryListByCategoryId.Handler(_mapper, _subcategoryMockRepository.Object,
            _categoryMockRepository.Object);

        // Act
        var result =
            await handler.Handle(
                new GetSubcategoryListByCategoryId.Query(Guid.Parse("961bfa68-9f14-4ec2-b86a-b787102b1e7f")),
                CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(mappedSubcategoryList);
    }

    [Fact]
    public async Task GetSubcategoryListByCategoryIdTests_InvalidCategoryId_ShouldReturnEmptyList()
    {
        // Arrange
        var subcategoryList =
            await _subcategoryMockRepository.Object.GetListByCategoryIdAsync(
                Guid.Parse("acc6d73a-85c2-4dff-aa78-e6e044ea638f"));
        var mappedSubcategoryList = _mapper.Map<List<SubcategoryViewModel>>(subcategoryList);
        var handler = new GetSubcategoryListByCategoryId.Handler(_mapper, _subcategoryMockRepository.Object,
            _categoryMockRepository.Object);

        // Act
        var result =
            await handler.Handle(
                new GetSubcategoryListByCategoryId.Query(Guid.Parse("acc6d73a-85c2-4dff-aa78-e6e044ea638f")),
                CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(mappedSubcategoryList);
        result.Should().BeEmpty();
    }
}