using AutoMapper;
using FluentAssertions;
using Moq;
using WantToSell.Application.Contracts.Identity;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Application.Features.Address.Models;
using WantToSell.Application.Features.Address.Queries;
using WantToSell.Application.Mappings;
using WantToSell.Application.UnitTests.Mocks;
using WantToSell.Domain.Interfaces;
using Xunit;

namespace WantToSell.Application.UnitTests.Address.Queries
{
    public class GetAddressTests
    {
        private readonly Mock<IAddressRepository> _addressMockRepository;
        private readonly Mock<IUserService> _userMockRepository;
        private readonly IMapper _mapper;

        public GetAddressTests()
        {
            _addressMockRepository = MockAddressRepository.GetAddressRepositoryMock();
            _userMockRepository = MockUserRepository.GetUserRepositoryMock();

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<AddressProfile>();
            });

            _mapper = mapperConfiguration.CreateMapper();
        }
        [Fact]
        public async Task GetAddressTests_UserHasAddress_ShouldReturnAddressDetailModel()
        {
            // Arrange
            var addressList = await _addressMockRepository.Object.GetListAsync();
            var mockUserContext = new Mock<IUserContext>();
            var userId = _userMockRepository.Object.GetUsers().Result.First().Id; //User with address
           
            mockUserContext.SetupGet(u => u.UserId).Returns(Guid.Parse(userId));

            var expectedAddress = addressList.FirstOrDefault(s => s.CreatedBy == Guid.Parse(userId));
            var expectedAddressDetailModel = _mapper.Map<AddressDetailModel>(expectedAddress);

            var handler = new GetAddress.Handler(_mapper, _addressMockRepository.Object, mockUserContext.Object);

            // Act
            var result = await handler.Handle(new GetAddress.Query(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedAddressDetailModel);
        }

        [Fact]
        public async Task GetAddressTests_UserDoesNotHaveAddress_ShouldReturnNull()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            var addressRepositoryMock = new Mock<IAddressRepository>();
            
            var mockUserContext = new Mock<IUserContext>();
            var userId = _userMockRepository.Object.GetUsers().Result.Last().Id; //User without address
            mockUserContext.SetupGet(u => u.UserId).Returns(Guid.Parse(userId));

            var handler = new GetAddress.Handler(mapperMock.Object, addressRepositoryMock.Object, mockUserContext.Object);

            // Act
            var result = await handler.Handle(new GetAddress.Query(), CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }
    }
}
