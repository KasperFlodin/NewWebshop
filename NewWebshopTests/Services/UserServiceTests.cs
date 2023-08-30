using NewWebshopAPI.DTOs.UserDTOs;

namespace NewWebshopTests.Services
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _userRepositoryMock = new();

        public UserServiceTests()
        {
            //_userService = new(_userRepositoryMock.Object);
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

        [Fact]
        public async void GetAllAsync_ShouldThrowException_WhenRepositoryReturnsNull()
        {
            // Arrange
            List<User> users = new();

            _userRepositoryMock
                .Setup(x => x.GetAll()).ReturnsAsync(() => throw new ArgumentNullException());

            // Act
            async Task action() => await _userService.GetAllAsync();

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(action);
            Assert.Contains("Value cannot be null", ex.Message);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnUserResponse_WhenUserExists()
        {
            // Arrange
            int userId = 1;

            User user = new()
            {
                Id = userId,
                FirstName = "Peter",
                LastName = "Plys",
                Phone = "12345678",
                Address = "Hunderedemeterskoven 1",
                City = "Byen",
                Zip = "1234",
                Email = "peter.plys@gmail.com",
                Password = "123456",
                Role = Role.Admin,
            };

            _userRepositoryMock
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.FirstName, result.FirstName);
            Assert.Equal(user.LastName, result.LastName);
            Assert.Equal(user.Phone, result.Phone);
            Assert.Equal(user.Address, result.Address);
            Assert.Equal(user.Zip, result.Zip);
            Assert.Equal(user.Email, result.Email);
            //Assert.Equal(user.Password, result.Password);
            Assert.Equal(user.Role, result.Role);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            int userId = 1;

            _userRepositoryMock
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void FindByEmailAsync_ShouldReturnUserResponse_WhenEmailExists()
        {
            // Arrange
            string email = "peter.plys@gmail.com";

            User user = new()
            {
                Id = 1,
                Email = email,
                Password = "123456",
                Role = Role.Admin,
            };

            _userRepositoryMock
                .Setup(x => x.GetByEmail(It.IsAny<string>()))
                .ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserByEmailAsync(email);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Email, result.Email);
            //Assert.Equal(user.Password, result.Password);
            Assert.Equal(user.Role, result.Role);
        }

        [Fact]
        public async void CreateAsync_ShouldReturnUserResponse_WhenCreateIsSuccess()
        {
            // Arrange
            RegisterUser newUser = new()
            {
                FirstName = "Peter",
                LastName = "Plys",
                Phone = "12345678",
                Address = "Hunderedemeterskoven 1",
                City = "Byen",
                Zip = "1234",
                Email = "peter.plys@gmail.com",
                Password = "123456",
            };

            int userId = 1;

            User user = new()
            {
                Id = userId,
                FirstName = "Peter",
                LastName = "Plys",
                Phone = "12345678",
                Address = "Hunderedemeterskoven 1",
                City = "Byen",
                Zip = "1234",
                Email = "peter.plys@gmail.com",
                Password = "123456",
                Role = Role.Admin,
            };

            _userRepositoryMock
                .Setup(x => x.Create(It.IsAny<User>()))
                .ReturnsAsync(user);

            // Act
            var result = await _userService.RegisterUserAsync(newUser);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal(user.FirstName, result.FirstName);
            Assert.Equal(user.LastName, result.LastName);
            Assert.Equal(user.Phone, result.Phone);
            Assert.Equal(user.Address, result.Address);
            Assert.Equal(user.Zip, result.Zip);
            Assert.Equal(user.Email, result.Email);
            //Assert.Equal(user.Password, result.Password);
            //Assert.Equal(user.Role, result.IsAdmin);
        }

        [Fact]
        public async void CreateAsync_ShouldThrowNullException_WhenRepositoryReturnsNull()
        {
            // Arrange
            RegisterUser registerUser = new()
            {
                FirstName = "Peter",
                LastName = "Plys",
                Phone = "12345678",
                Address = "Hunderedemeterskoven 1",
                City = "Byen",
                Zip = "1234",
                Email = "peter.plys@gmail.com",
                Password = "123456",
            };

            _userRepositoryMock
                .Setup(x => x.Create(It.IsAny<User>())).ReturnsAsync(() => null);

            // Act
            async Task action() => await _userService.RegisterUserAsync(registerUser);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(action);
            Assert.Contains("Value cannot be null", ex.Message);
        }

        [Fact]
        public async void UpdateByIdAsync_ShouldReturnUserResponse_WhenUpdateIsSuccess()
        {
            // Arrange
            UserRequest userRequest = new()
            {
                FirstName = "Peter",
                LastName = "Plys",
                Phone = "12345678",
                Address = "Hunderedemeterskoven 1",
                City = "Byen",
                Zip = "1234",
                Email = "peter.plys@gmail.com",
                Password = "123456",
                Role = Role.Admin,
            };

            int userId = 1;

            User user = new()
            {
                Id = userId,
                FirstName = "Peter",
                LastName = "Plys",
                Phone = "12345678",
                Address = "Hunderedemeterskoven 1",
                City = "Byen",
                Zip = "1234",
                Email = "peter.plys@gmail.com",
                Password = "123456",
                Role = Role.Admin,
            };

            _userRepositoryMock
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await _userService.UpdateUserByIdAsync(userId, userRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal(user.FirstName, result.FirstName);
            Assert.Equal(user.LastName, result.LastName);
            Assert.Equal(user.Phone, result.Phone);
            Assert.Equal(user.Address, result.Address);
            Assert.Equal(user.Zip, result.Zip);
            Assert.Equal(user.Email, result.Email);
            //Assert.Equal(user.Password, result.Password);
            //Assert.Equal(user.Role, result.IsAdmin);

        }

        [Fact]
        public async void UpdateByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            UserRequest userRequest = new()
            {
                FirstName = "Peter",
                LastName = "Plys",
                Phone = "12345678",
                Address = "Hunderedemeterskoven 1",
                City = "Byen",
                Zip = "1234",
                Email = "peter.plys@gmail.com",
                Password = "123456",
                Role = Role.Admin,
            };

            int userId = 1;

            _userRepositoryMock
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<User>())).ReturnsAsync(() => null);

            // Act
            //var result = await _userService.UpdateByIdAsync(userId, userRequest);
            var result = await _userService.UpdateUserByIdAsync(userId, userRequest);
            // Assert
            Assert.Null(result);
        }

           [Fact]
            public async void DeleteByIdAsync_ShouldReturnUserResponse_WhenDeleteIsSuccess()
            {
                // Arrange
                int userId = 1;

                User user = new()
                {
                    FirstName = "Peter",
                    LastName = "Plys",
                    Phone = "12345678",
                    Address = "Hunderedemeterskoven 1",
                    City = "Byen",
                    Zip = "1234",
                    Email = "peter.plys@gmail.com",
                    Password = "123456",
                    Role = Role.Admin,
                };

                _userRepositoryMock
                    .Setup(x => x.Delete(It.IsAny<int>()))
                    .ReturnsAsync(user);

                // Act
                var result = await _userService.DeleteUserByIdAsync(userId);

                // Assert
                Assert.NotNull(result);
                Assert.IsType<UserResponse>(result);
                Assert.Equal(user.Id, result?.Id);
            }

        [Fact]
        public async void DeleteByIdAsync_ShouldReturnNull_WhenUserDoesNotExists()
        {
            // Arrange
            int userId = 1;

            _userRepositoryMock
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _userService.DeleteUserByIdAsync(userId);

            // Assert
            Assert.Null(result);
        }

    }
}
