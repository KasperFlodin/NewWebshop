using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NewWebshopAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewWebshopTests.Controllers
{
    public class ProductControllerTests
    {
        private readonly ProductController _productController;
        private readonly Mock<IProductService> _productServiceMock = new();

        public ProductControllerTests()
        {
            _productController = new(_productServiceMock.Object);
        }

        [Fact]
        public async void GetAllProductsAsync_ShouldReturnStatusCode200_WhenProductExists()
        {
            // Arrange
            List<ProductResponse> products = new()
            {
                new()
                {
                    Id = 1,
                    Name = "Test",
                    Price = 123,
                    Type = "Chair",
                    Photolink = "Link"
                },
                new()
                {
                    Id = 2,
                    Name = "Test2",
                    Price = 1234,
                    Type = "Bed",
                    Photolink = "Link2"
                }
            };

            _productServiceMock
                .Setup(x => x.GetAllProductsAsync()).ReturnsAsync(products);
            
            // Act
            var result = (IStatusCodeActionResult)await _productController.GetAllProductAsync();

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAllProductsAsync_ShouldReturnStatusCode204_WhenNoProductsExists()
        {
            // Arrange
            List<ProductResponse> products = new();

            _productServiceMock
                .Setup(x => x.GetAllProductsAsync()).ReturnsAsync(products);

            // Act
            var result = (IStatusCodeActionResult)await _productController.GetAllProductAsync();

            // Assert
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async void GetAllProductsASync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _productServiceMock
                .Setup(x => x.GetAllProductsAsync())
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            // Act
            var result = (IStatusCodeActionResult)await _productController.GetAllProductAsync();

            // Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void CreateAsync_ShouldReturnStatusCode200_WhenCarIsSuccessfullyCreated()
        {
            // Arrange
            ProductRequest productRequest = new()
            {
                Name = "Test",
                Price = 1234,
                Type = "Chair",
                Photolink = "Link"
            };

            int productId = 1;

            ProductResponse productResponse = new()
            {
                Id = productId,
                Name = "Test",
                Price = 1234,
                Type = "Chair",
                Photolink = "Link"
            };

            _productServiceMock
                .Setup(x => x.CreateProductAsync(It.IsAny<ProductRequest>()))
                .ReturnsAsync(productResponse);

            // Act
            var result = (IStatusCodeActionResult)await _productController.CreateProductAsync(productRequest);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void CreateProductAsync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            ProductRequest productRequest = new()
            {
                Name = "Test",
                Price = 1234,
                Type = "Chair",
                Photolink = "Link"
            };

            _productServiceMock
                .Setup(x => x.CreateProductAsync(It.IsAny<ProductRequest>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));


            // Act
            var result = (IStatusCodeActionResult)await _productController.CreateProductAsync(productRequest);

            // Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnStatusCode200_WhenCarExists()
        {
            // Arrange
            int productId = 1;

            ProductResponse productResponse = new()
            {
                Id = productId,
                Name = "Test",
                Price = 1234,
                Type = "Chair",
                Photolink = "Link"
            };

            _productServiceMock
                .Setup(x => x.FindProductByIdAsync(It.IsAny<int>())).ReturnsAsync(productResponse);

            // Act
            var result = (IStatusCodeActionResult)await _productController.FindProductByIdAsync(productId);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int productId = 1;

            _productServiceMock
                .Setup(x => x.FindProductByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            // Act
            var result = (IStatusCodeActionResult)await _productController.FindProductByIdAsync(productId);

            // Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void UpdateProductByIdAsync_ShouldReturnStatusCode200_WhenProductIsUpdated()
        {
            // Arrange
            ProductRequest productRequest = new()
            {
                Name = "Test",
                Price = 1234,
                Type = "Chair",
                Photolink = "Link"
            };

            int productId = 1;

            ProductResponse productResponse = new()
            {
                Id = productId,
                Name = "Test",
                Price = 1234,
                Type = "Chair",
                Photolink = "Link"
            };
            
            _productServiceMock
                .Setup(x => x.UpdateProductByIdAsync(It.IsAny<int>(), It.IsAny<ProductRequest>()))
                .ReturnsAsync(productResponse);

            // Act
            var result = (IStatusCodeActionResult)await _productController.UpdateProductByIdAsync(productId, productRequest);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void UpdateByIdAsync_ShouldReturnStatusCode404_WhenCarDoesNotExist()
        {
            // Arrange
            ProductRequest productRequest = new()
            {
                Name = "Test",
                Price = 1234,
                Type = "Chair",
                Photolink = "Link"
            };

            int productId = 1;

            _productServiceMock
                .Setup(x => x.UpdateProductByIdAsync(It.IsAny<int>(), It.IsAny<ProductRequest>()))
                .ReturnsAsync(() => null);
            
            // Act
            var result = (IStatusCodeActionResult)await _productController.UpdateProductByIdAsync(productId, productRequest);
            
            // Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void UpdateByIdAsync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            ProductRequest productRequest = new()
            {
                Name = "Test",
                Price = 1234,
                Type = "Chair",
                Photolink = "Link"
            };

            int productId = 1;

            _productServiceMock
                .Setup(x => x.UpdateProductByIdAsync(It.IsAny<int>(), It.IsAny<ProductRequest>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            // Act
            var result = (IStatusCodeActionResult)await _productController.UpdateProductByIdAsync(productId, productRequest);

            // Assert
            Assert.Equal(500, result.StatusCode);
        }

        //[Fact]
        //public async void DeleteProductByIdAsync_ShouldReturnStatusCode200_WhenProductIsDeleted()
        //{
        //    // Arrange
        //    int productId = 1;

        //    ProductResponse productResponse = new()
        //    {
        //        Id = productId,
        //        Name = "Test",
        //        Price = 1234,
        //        Type = "Chair",
        //        Photolink = "Link"
        //    };

        //    _productServiceMock
        //        .Setup(x => x.DeleteProductByIdAsync(It.IsAny<int>()))
        //        .ReturnsAsync(productResponse);

        //    // Act
        //    var result = (IStatusCodeActionResult)await _productController.DeleteProductByIdAsync(productId);
            
        //    // Assert
        //    Assert.Equal(200, result.StatusCode);
        //}

        [Fact]
        public async void DeleteByIdAsync_ShouldReturnStatusCode404_WhenCarDoesNotExist()
        {
            // Arrange
            _productServiceMock
                .Setup(x => x.DeleteProductByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = (IStatusCodeActionResult)await _productController.DeleteProductByIdAsync(1);
            
            // Assert
            Assert.Equal(404, result.StatusCode);
        }

    }
}
