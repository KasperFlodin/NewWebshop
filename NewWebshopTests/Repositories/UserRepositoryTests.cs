using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewWebshopTests.Repositories
{
    public class UserRepositoryTests
    {
        private readonly DbContextOptions<DatabaseContext> _options;
        private readonly DatabaseContext _DbContext;
        private readonly UserRepository _userRepository;

        public UserRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "UserRepositoryTests").Options;

            _DbContext = new(_options);

            _userRepository = new(_DbContext);
        }


        public async void GetById_ShouldReturnUser_WhenUserExist()
        {
            // Arrange
            await _DbContext.Database.EnsureDeletedAsync();

            int userId = 1;

            _DbContext.User.Add(new()
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
            });

            await _DbContext.SaveChangesAsync();

            // Act
            var result = await _userRepository.GetById(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result?.Id);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnNull_WhenUserDoesNotExists()
        {
            // Arrange
            await _DbContext.Database.EnsureDeletedAsync();

            // Act
            var result = await _userRepository.GetById(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void GetByEmail_ShouldReturnUser_WhenEmailExists()
        {
            // Arrange
            await _DbContext.Database.EnsureDeletedAsync();

            string email = "peter.plys@gmail.com";

            _DbContext.User.Add(new()
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
            });

            await _DbContext.SaveChangesAsync();

            // Act
            var result = await _userRepository.GetByEmail(email);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(email, result?.Email);
        }

        [Fact]
        public async void GetByEmail_ShouldReturnNull_WhenEmailDoesNotExists()
        {
            // Arrange
            await _DbContext.Database.EnsureDeletedAsync();

            string email = "peter.plys@gmail.com";
            // Act
            var result = await _userRepository.GetByEmail(email);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfUsers_WhenUsersExists()
        {
            // Arrange
            await _DbContext.Database.EnsureDeletedAsync();

            _DbContext.User.Add(new()
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
                Role = Role.Admin

            });

            _DbContext.User.Add(new()
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
                Role = Role.Admin

            });

            await _DbContext.SaveChangesAsync();

            // Act
            var result = await _userRepository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<User>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfUsers_WhenNoUsersExists()
        {
            // Arrange
            await _DbContext.Database.EnsureDeletedAsync();

            // Act
            var result = await _userRepository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<User>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void Create_ShouldAddNewIdToUser_WhenSavingToDatabase()
        {
            // Arrange
            await _DbContext.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

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
                Role = Role.Admin
            };

            // Act
            var result = await _userRepository.Create(user);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(expectedNewId, result?.Id);
        }

        [Fact]
        public async void Create_ShouldFailToAddNewUser_WhenUserIdAlreadyExists()
        {
            // Arrange
            await _DbContext.Database.EnsureDeletedAsync();

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

            await _userRepository.Create(user);

            // Act
            async Task action() => await _userRepository.Create(user);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void Update_ShouldChangeValuesOnUser_WhenUserExists()
        {
            // Arrange
            await _DbContext.Database.EnsureDeletedAsync();

            int userId = 1;
            User newUser = new()
            {
                FirstName = "Peter",
                LastName = "Plys",
                Phone = "12345678",
                Address = "Hunderedemeterskoven 1",
                City = "Byen",
                Zip = "1234",
                Email = "peter.plys@gmail.com",
                Password = "123456",
                Role = Role.Admin
            };

            _DbContext.User.Add(newUser);
            await _DbContext.SaveChangesAsync();
            User updateUser = new()
            {
                Id = userId,
                FirstName = "new Peter",
                LastName = "new Plys",
                Phone = "1234567",
                Address = "Hunderedemeterskoven 15",
                City = "Andeby",
                Zip = "1235",
                Email = "peter.plys@gmail.com",
                Password = "123456",
                Role = Role.Admin
            };

            // Act
            var result = await _userRepository.Update(userId, updateUser);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result?.Id);
            Assert.Equal(updateUser.FirstName, result?.FirstName);
            Assert.Equal(updateUser.LastName, result?.LastName);
            Assert.Equal(updateUser.Phone, result?.Phone);
            Assert.Equal(updateUser.Address, result?.Address);
            Assert.Equal(updateUser.Zip, result?.Zip);
            Assert.Equal(updateUser.Email, result?.Email);
            Assert.Equal(updateUser.Password, result?.Password);
            Assert.Equal(updateUser.Role, result?.Role);
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            await _DbContext.Database.EnsureDeletedAsync();

            int userId = 1;
            User updateUser = new()
            {
                Id = userId,
                FirstName = "new Peter",
                LastName = "new Plys",
                Phone = "1234567",
                Address = "Hunderedemeterskoven 15",
                City = "Andeby",
                Zip = "1235",
                Email = "peter.plys@gmail.com",
                Password = "123456",
                Role = Role.Admin
            };

            // Act
            var result = await _userRepository.Update(userId, updateUser);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteByIdAsync_ShouldReturnDeletedUser_WhenUserIsDeleted()
        {
            // Arrange
            await _DbContext.Database.EnsureDeletedAsync();

            int userId = 1;
            User newUser = new()
            {
                FirstName = "Peter",
                LastName = "Plys",
                Phone = "12345678",
                Address = "Hunderedemeterskoven 1",
                City = "Byen",
                Zip = "1234",
                Email = "peter.plys@gmail.com",
                Password = "123456",
                Role = Role.Admin
            };

            _DbContext.User.Add(newUser);
            await _DbContext.SaveChangesAsync();

            // Act
            var result = await _userRepository.Delete(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result?.Id);
        }

        [Fact]
        public async void DeleteByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            await _DbContext.Database.EnsureDeletedAsync();

            // Act
            var result = await _userRepository.Delete(1);

            // Assert
            Assert.Null(result);
        }

    }
}
