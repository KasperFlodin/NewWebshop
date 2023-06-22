using Microsoft.AspNetCore.Mvc.Infrastructure;
using NewWebshopAPI.Controllers;
using NewWebshopAPI.Database.Entities;
using NewWebshopAPI.DTOs.ProductDTOs;
using NewWebshopAPI.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewWebshopTests.Controllers
{
    public class UserControllerTests
    {
        private readonly UserController _userController;
        private readonly Mock<IUserService> _userServiceMock = new();

        public UserControllerTests()
        {
            _userController = new(_userServiceMock.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenUserExists()
        {

            // Arrange
            List<UserResponse> users = new()
            {
                new UserResponse()
                {
                    Id = 1,
                    FirstName = "Peter",
                    LastName = "Plys",
                    Phone = "12345678",
                    Address = "Hunderedemeterskoven 1",
                    City = "Byen",
                    Zip = "1234",
                    Email = "peter.plys@gmail.com",
                    Role = Role.Admin,
                },

                new UserResponse()
                {
                    Id = 2,
                    FirstName = "Peter",
                    LastName = "Kanin",
                    Phone = "12345678",
                    Address = "Hunderedemeterskoven 17",
                    City = "Byen",
                    Zip = "1234",
                    Email = "peter.kanin@gmail.com",
                    Role = Role.User,
                }
            };

            _userServiceMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(users);

            // Act
            var result = (IStatusCodeActionResult)await _userController.GetAll();

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoUsersExists()
        {
            // Arrange
            List<UserResponse> users = new();

            _userServiceMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(users);

            // Act
            var result = (IStatusCodeActionResult)await _userController.GetAll();

            // Assert
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            List<User> users = new();

            _userServiceMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            // Act
            var result = (IStatusCodeActionResult)await _userController.GetAll();

            // Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void GetByIdAsync_ShouldReturnStatusCode200_WhenUserExists()
        {
            // Arrange
            int userId = 1;

            UserResponse user = new()
            {
                Id = userId,
                FirstName = "Peter",
                LastName = "Plys",
                Phone = "12345678",
                Address = "Hunderedemeterskoven 1",
                City = "Byen",
                Zip = "1234",
                Email = "peter.plys@gmail.com",
                Role = Role.Admin
            };

            _userServiceMock
                .Setup(x => x.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(user);

            //Act
               var result = (IStatusCodeActionResult)await _userController.GetById(userId);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetByIdAsync_ShouldReturnStatusCode404_WhenUserDoesNotExist()
        {
            // Arrange
            int userId = 1;

            _userServiceMock
                .Setup(x => x.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);
            
            // Act
            var result = (IStatusCodeActionResult)await _userController.GetById(userId);

            // Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void RegisterUser_ShouldReturnStatusCode200_WhenUserIsSuccessfullyCreated()
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
                Password = "123456"
            };

            int userId = 1;

            UserResponse userResponse = new()
            {
                Id = userId,
                FirstName = "Peter",
                LastName = "Plys",
                Phone = "12345678",
                Address = "Hunderedemeterskoven 1",
                City = "Byen",
                Zip = "1234",
                Email = "peter.plys@gmail.com",
                Role = Role.Admin
            };

            _userServiceMock
            .Setup(x => x.RegisterUserAsync(It.IsAny<RegisterUser>()))
            .ReturnsAsync(userResponse);

            // Act
            var result = (IStatusCodeActionResult)await _userController.Register(newUser);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void RegisterUser_ShouldReturnStatusCode500_WhenExceptionIsRaised()
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
                Password = "123456"
            };

            _userServiceMock
                .Setup(x => x.RegisterUserAsync(It.IsAny<RegisterUser>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            // Act
            var result = (IStatusCodeActionResult)await _userController.Register(newUser);

            // Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenUserIsUpdated()
        {
            // Arrange
            UserRequest updateUser = new()
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

            UserResponse userResponse = new()
            {
                Id = userId,
                FirstName = "Peter",
                LastName = "Plys",
                Phone = "12345678",
                Address = "Hunderedemeterskoven 2",
                City = "Byen",
                Zip = "1234",
                Email = "peter.plys@gmail.com"
            };

            _userServiceMock
                .Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<UserRequest>()))
                .ReturnsAsync(userResponse);

            // Act
            var result = (IStatusCodeActionResult)await _userController.Update(userId, updateUser);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            UserRequest updateUser = new()
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

            int userId = 1;

            _userServiceMock
                .Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<UserRequest>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            // Act
            var result = (IStatusCodeActionResult)await _userController.Update(userId, updateUser);

            // Assert
            Assert.Equal(500, result.StatusCode);
        }

       [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenUserIsDeleted()
        {
            // Arrange
            int userId = 1;

            UserResponse userResponse = new()
            {
                Id = userId,
                FirstName = "Peter",
                LastName = "Plys",
                Phone = "12345678",
                Address = "Hunderedemeterskoven 1",
                City = "Byen",
                Zip = "1234",
                Email = "peter.plys@gmail.com",
                Role = Role.Admin,
            };

            _userServiceMock
                .Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(userResponse);

            // Act
            var result = (IStatusCodeActionResult)await _userController.Delete(userId);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_whenUserDoesNotExist()
        {
            // Arrange
            int userId = 1;

            _userServiceMock
                .Setup(x => x.DeleteAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = (IStatusCodeActionResult)await _userController.Delete(userId);

            // Assert
            Assert.Equal(404, result.StatusCode);
        }

    }
}
