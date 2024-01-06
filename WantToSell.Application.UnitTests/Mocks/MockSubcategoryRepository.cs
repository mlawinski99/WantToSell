using AutoMapper;
using Moq;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Mappings;

namespace WantToSell.Application.UnitTests.Mocks;

public class MockSubcategoryRepository
{
    public static Mock<ISubcategoryRepository> GetSubcategoryRepositoryMock()
    {
        var mapperConfiguration = new MapperConfiguration(c => { c.AddProfile<SubcategoryProfile>(); });
        var mapper = mapperConfiguration.CreateMapper();

        var subcategoryList = new List<Domain.Subcategory>
        {
            new()
            {
                Id = Guid.Parse("e8be60f5-eb3b-4c66-b8ff-815c799bbb8a"),
                Name = "Subcategory1",
                CategoryId = Guid.Parse("bc6a071f-c0d1-40e6-9889-528a74bde413")
            },
            new()
            {
                Id = Guid.Parse("a965d961-46a7-44b4-bc17-9f96a938a057"),
                Name = "Subcategory2",
                CategoryId = Guid.Parse("bc6a071f-c0d1-40e6-9889-528a74bde413")
            }
        };

        var subcategoryMockRepository = new Mock<ISubcategoryRepository>();

        subcategoryMockRepository.Setup(s => s.GetListAsync())
            .ReturnsAsync(subcategoryList);

        subcategoryMockRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id) =>
            {
                return subcategoryList
                    .FirstOrDefault(a => a.Id == id);
            });

        subcategoryMockRepository.Setup(s => s.GetListByCategoryIdAsync(It.IsAny<Guid>()))
            .Returns((Guid categoryId) =>
            {
                return subcategoryList
                    .Where(a => a.CategoryId == categoryId);
            });

        subcategoryMockRepository.Setup(s => s.CreateAsync(It.IsAny<Domain.Subcategory>()))
            .ReturnsAsync((Domain.Subcategory subcategory) =>
            {
                subcategoryList.Add(subcategory);

                return subcategory;
            });

        subcategoryMockRepository.Setup(s => s.UpdateAsync(It.IsAny<Domain.Subcategory>()))
            .ReturnsAsync((Domain.Subcategory subcategory) =>
            {
                var existingSubcategory = subcategoryList
                    .FirstOrDefault(s => s.Id == subcategory.Id);

                mapper.Map(subcategory, existingSubcategory);
                return subcategory;
            });

        subcategoryMockRepository.Setup(s => s.DeleteAsync(It.IsAny<Domain.Subcategory>()))
            .Returns((Domain.Subcategory subcategory) =>
            {
                subcategoryList.Remove(subcategory);

                return Task.CompletedTask;
            });

        return subcategoryMockRepository;
    }
}