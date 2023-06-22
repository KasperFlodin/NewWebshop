namespace NewWebshopTests.Services
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _userRepositoryMock = new();

        public UserServiceTests()
        {
            _userService = new(_userRepositoryMock.Object);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnListOfUserResponses_WhenUsersExists()
        {
            // Arrange
            List<User> users = new()
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "Peter",
                        LastName = "Plys",
                        Phone = "12345678",
                        Address = "Hunderedemeterskoven 1",
                        City = "Byen",
                        Zip = "1234",
                        Email = "peter.plys@gmail.com",
                        Password = "123456",
                        Role = Role.Admin,
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Peter",
                        LastName = "Kanin",
                        Phone = "12345678",
                        Address = "Hunderedemeterskoven 17",
                        City = "Byen",
                        Zip = "1234",
                        Email = "peter.kanin@gmail.com",
                        Password = "123456",
                        Role = Role.User
                    }
                };

            _userRepositoryMock
                .Setup(x => x.GetAll())
                .ReturnsAsync(users);

            // Act
            var result = await _userService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<UserResponse>>(result);
            Assert.Equal(2, result?.Count);

        }


    }
}
