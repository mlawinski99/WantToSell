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
                CategoryId = Guid.Parse("961bfa68-9f14-4ec2-b86a-b787102b1e7f")
            },
            new()
            {
                Id = Guid.Parse("a965d961-46a7-44b4-bc17-9f96a938a057"),
                Name = "Subcategory2",
                CategoryId = Guid.Parse("961bfa68-9f14-4ec2-b86a-b787102b1e7f")
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
            .ReturnsAsync((Guid categoryId) =>
            {
                return subcategoryList
                    .Where(a => a.CategoryId == categoryId)
                    .ToList();
            });

        subcategoryMockRepository.Setup(s => s.IsSubcategoryNameExists(It.IsAny<string>()))
            .Returns((string name) =>
            {
                return subcategoryList
                    .Any(a => a.Name == name);
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

                if (existingSubcategory == null)
                    return null;

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