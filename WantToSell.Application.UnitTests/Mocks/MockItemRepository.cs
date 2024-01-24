using AutoMapper;
using Moq;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Mappings;
using WantToSell.Domain;

namespace WantToSell.Application.UnitTests.Mocks;

public class MockItemRepository
{
    public static Mock<IItemRepository> GetItemRepositoryMock()
    {
        var mapperConfiguration = new MapperConfiguration(c => { c.AddProfile<ItemProfile>(); });
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
            },
            new()
            {
                Id = Guid.Parse("edac3f3d-6451-483a-afdc-517b168d27fe"),
                Name = "Item2",
                Description = "Description2",
                CategoryId = Guid.Parse("961bfa68-9f14-4ec2-b86a-b787102b1e7f"),
                SubcategoryId = Guid.Parse("336bc67d-86f2-4f96-927b-f977481fa813")
            }
        };

        var itemMockRepository = new Mock<IItemRepository>();

        itemMockRepository.Setup(s => s.GetListAsync())
            .ReturnsAsync(itemList);

        itemMockRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id) =>
            {
                return itemList
                    .FirstOrDefault(a => a.Id == id);
            });

        itemMockRepository.Setup(s => s.CreateAsync(It.IsAny<Item>()))
            .ReturnsAsync((Item item) =>
            {
                itemList.Add(item);

                return item;
            });

        itemMockRepository.Setup(s => s.UpdateAsync(It.IsAny<Item>()))
            .ReturnsAsync((Item item) =>
            {
                var existingItem = itemList
                    .FirstOrDefault(s => s.Id == item.Id);

                //@todo check if in other mock repositories its done in correct way

                if (existingItem == null)
                    return null;

                mapper.Map(item, existingItem);
                return item;
            });

        itemMockRepository.Setup(s => s.DeleteAsync(It.IsAny<Item>()))
            .Returns((Item item) =>
            {
                itemList.Remove(item);

                return Task.CompletedTask;
            });

        return itemMockRepository;
    }
}