using Moq;
using WantToSell.Application.Contracts.Identity;
using WantToSell.Application.Models.Identity;

namespace WantToSell.Application.UnitTests.Mocks;

public class MockUserRepository
{
    public static Mock<IUserService> GetUserRepositoryMock()
    {
        var userList = new List<UserModel>
        {
            new()
            {//User with address
                Id = "02f7af17-d128-41fb-977b-2dec061114c8",
                UserName = "testuser",
                Name = "testuser",
                Email = "testuser@user.com",
                Password = "testpassword"
            },
            new()
            {//User with no address
                Id = "d9247df3-37e1-4423-847b-32cdd1b080d5",
                UserName = "testuser2",
                Name = "testuser2",
                Email = "testuser2@user.com",
                Password = "testpassword"
            }
        };
        
        var userMockRepository = new Mock<IUserService>();

        userMockRepository.Setup(s => s.GetUser(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id) =>
            {
                return userList
                    .FirstOrDefault(s => s.Id == id.ToString());
            });
      
        userMockRepository.Setup(s => s.GetUsers())
            .ReturnsAsync( () => userList.ToList());

        return userMockRepository;
    }
}