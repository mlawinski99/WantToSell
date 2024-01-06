using AutoMapper;
using Moq;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Mappings;
using WantToSell.Domain;

namespace WantToSell.Application.UnitTests.Mocks;

public class MockCategoryRepository
{
    public static Mock<ICategoryRepository> GetCategoryRepositoryMock()
    {
        var mapperConfiguration = new MapperConfiguration(c => { c.AddProfile<CategoryProfile>(); });
        var mapper = mapperConfiguration.CreateMapper();

        var itemList = new List<Item>
        {
            new()
            {
                Id = Guid.Parse("e1b0b6a0-0f7a-4e1e-9e1a-0b6b9e1a0b6b"),
                Name = "Item1",
                Description = "Description1",
                CategoryId = Guid.Parse("961bfa68-9f14-4ec2-b86a-b787102b1e7f"),
                SubcategoryId = Guid.Parse("336bc67d-86f2-4f96-927b-f977481fa813")
            }
        };

        var categoryList = new List<Domain.Category>
        {
            new()
            {
                Id = Guid.Parse("961bfa68-9f14-4ec2-b86a-b787102b1e7f"),
                Name = "Category1",
                Subcategories = new List<Domain.Subcategory>
                {
                    new()
                    {
                        Id = Guid.Parse("336bc67d-86f2-4f96-927b-f977481fa813"),
                        Name = "Subcategory1",
                        Items = itemList
                    }
                },
                Items = itemList
            },
            new()
            {
                Id = Guid.Parse("acc6d73a-85c2-4dff-aa78-e6e044ea638f"),
                Name = "Category2",
                Subcategories = new List<Domain.Subcategory>
                {
                    new()
                    {
                        Id = Guid.Parse("038d7697-ae4b-4c92-81e8-6dbd3c7cd122"),
                        Name = "Subcategory2"
                    }
                }
            }
        };

        var categoryMockRepository = new Mock<ICategoryRepository>();

        categoryMockRepository.Setup(s => s.GetListAsync())
            .ReturnsAsync(categoryList);

        categoryMockRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id) =>
            {
                return categoryList
                    .FirstOrDefault(a => a.Id == id);
            });

        categoryMockRepository.Setup(s => s.IsCategoryExists(It.IsAny<Guid>()))
            .Returns((Guid id) =>
            {
                return categoryList
                    .Any(a => a.Id == id);
            });

        categoryMockRepository.Setup(s => s.IsCategoryNameExists(It.IsAny<string>()))
            .Returns((string name) =>
            {
                return categoryList
                    .Any(a => a.Name == name);
            });

        categoryMockRepository.Setup(s => s.CreateAsync(It.IsAny<Domain.Category>()))
            .ReturnsAsync((Domain.Category category) =>
            {
                categoryList.Add(category);

                return category;
            });

        categoryMockRepository.Setup(s => s.UpdateAsync(It.IsAny<Domain.Category>()))
            .ReturnsAsync((Domain.Category category) =>
            {
                var existingCategory = categoryList
                    .FirstOrDefault(s => s.Id == category.Id);

                if (existingCategory == null)
                    return null;

                mapper.Map(category, existingCategory);
                return category;
            });

        categoryMockRepository.Setup(s => s.DeleteAsync(It.IsAny<Domain.Category>()))
            .Returns((Domain.Category category) =>
            {
                categoryList.Remove(category);

                return Task.CompletedTask;
            });

        return categoryMockRepository;
    }
}