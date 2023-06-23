using NewWebshopAPI.Database.Entities;
using System;

namespace NewWebshopTests.Services
{
    public class ProductServiceTests
    {
        private readonly ProductService _productService;
        private readonly Mock<IProductRepository> _productRepositoryMock = new();

        public ProductServiceTests()
        {
            _productService = new(_productRepositoryMock.Object);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnListOfProductResponses_WhenProductsExists()
        {
            // Arrange
            List<Product> products = new()
            {
                new Product
                {
                    Id = 1,
                    Name = "Test",
                    Price = 123,
                    Type = "ChairTest",
                    Photolink = "Link",
                },
                new Product
                {
                    Id= 2,
                    Name = "Test2",
                    Price = 321,
                    Type = "BedTest",
                    Photolink = "Link2",
                }
            };

            _productRepositoryMock
                .Setup(x => x.GetAllProductsAsync()).ReturnsAsync(products);

            // Act
            var result = await _productService.GetAllProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ProductResponse>>(result);
            Assert.Equal(2, products.Count);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnEmptyListOfProductResponses_WhenNoProductExists()
        {
            // Arrange
            List<Product> products = new();

            _productRepositoryMock
                .Setup(x => x.GetAllProductsAsync()).ReturnsAsync(products);

            // Act
            var result = await _productService.GetAllProductsAsync();

            // Assert
            Assert.Empty(result);
            Assert.IsType<List<ProductResponse>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetAllAsync_ShouldThrowNullException_WhenRepositoryReturnsNull()
        {
            // Arrange
            List<Product> product = new();

            _productRepositoryMock
                .Setup(x => x.GetAllProductsAsync()).ReturnsAsync(() => throw new ArgumentNullException());

            // Act
            async Task action() => await _productService.GetAllProductsAsync();

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(action);
            Assert.Contains("Value cannot be null", ex.Message);
        }

        [Fact]
        public async void CreateAsync_ShouldReturnCarResponse_WhenCreateIsSuccess()
        {
            // Arrange
            ProductRequest productRequest = new()
            {
                Name = "Chair1Test",
                Price = 12345,
                Type = "ChairTest",
                Photolink = "LinkTest"
            };

            int productId = 1;

            Product product = new()
            {
                Id = productId,
                Name = "Chair1Test",
                Price = 12345,
                Type = "ChairTest",
                Photolink = "LinkTest"
            };

            _productRepositoryMock
                .Setup(x => x.CreateProductAsync(It.IsAny<Product>())).ReturnsAsync(product);

            // Act
            var result = await _productService.CreateProductAsync(productRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductResponse>(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.Price, result.Price);
            Assert.Equal(product.Type, result.Type);
            Assert.Equal(product.Photolink, result.Photolink);
        }

        [Fact]
        public async void CreateAsync_ShouldThrowNullException_WhenRepositoryReturnsNull()
        {
            // Arrange
            ProductRequest productRequest = new()
            {
                Name = "ChairTest",
                Price = 1234,
                Type = "Chair",
                Photolink = "Link"
            };

            _productRepositoryMock
                .Setup(x => x.CreateProductAsync(It.IsAny<Product>())).ReturnsAsync(() => null);

            // Act
            async Task action() => await _productService.CreateProductAsync(productRequest);
            
            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(action);
            Assert.Contains("Value cannot be null", ex.Message);
        }

        [Fact]
        public async void FindProductByIdAsync_ShouldReturnProductResponse_WhenProductExists()
        {
            // Arrange
            int productId = 1;

            Product product = new()
            {
                Id = productId,
                Name = "ChairTest",
                Price = 1234,
                Type = "Chair",
                Photolink = "Link"
            };
            
            _productRepositoryMock
                .Setup(x => x.FindProductByIdAsync(It.IsAny<int>())).ReturnsAsync(product);

            // Act
            var result = await _productService.FindProductByIdAsync(productId);
            
            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductResponse>(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.Price, result.Price);
            Assert.Equal(product.Type, result.Type);
            Assert.Equal(product.Photolink, result.Photolink);
        }

        [Fact]
        public async void FindByIdAsync_ShoudReturnNull_WhenCarDoesNotExists()
        {
            // Arrange            
            _productRepositoryMock
                .Setup(x => x.FindProductByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _productService.FindProductByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateProductByIdAsync_ShouldChangeValueOnCar_WhenProductExists()
        {
            // Arrange
            _productRepositoryMock
                .Setup(x => x.FindProductByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = await _productService.FindProductByIdAsync(1);
            
            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateProductByIdAsync_ShouldChangeValueOnProduct_WhenProductExists()
        {
            // Arrange
            ProductRequest productRequest = new()
            {
                Name = "ChairName",
                Price = 1234,
                Type = "Chair",
                Photolink = "LinkHere"
            };

            int productId = 1;

            Product updateProduct = new()
            {
                Id = productId,
                Name = "Chairupdated",
                Price = 12345,
                Type = "Bed",
                Photolink = "Linkupdated"
            };

            _productRepositoryMock
                .Setup(x => x.UpdateProductByIdAsync(It.IsAny<int>(), It.IsAny<Product>()))
                .ReturnsAsync(updateProduct);

            // Act
            var result = await _productService.UpdateProductByIdAsync(productId, productRequest);
            
            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductResponse>(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal(updateProduct.Name, result.Name);
            Assert.Equal(updateProduct.Price, result.Price);
            Assert.Equal(updateProduct.Type, result.Type);
            Assert.Equal(updateProduct.Photolink, result.Photolink);
        }

        [Fact]
        public async void UpdateProductByIdAsync_ShouldChangeValueOnProduct_WhenProductDoesNotExists()
        {
            // Arrange
            ProductRequest productRequest = new()
            {
                Name = "ChairName",
                Price = 1234,
                Type = "Chair",
                Photolink = "LinkHere"
            };

            int productId = 1;

            _productRepositoryMock
                .Setup(X => X.UpdateProductByIdAsync(It.IsAny<int>(), It.IsAny<Product>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _productService.UpdateProductByIdAsync(productId, productRequest);
            
            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteProductByIdAsync_ShouldReturnProductResponse_WhenDeleteIsSuccess()
        {
            // Arrange
            int productId = 1;

            Product product = new()
            {
                Id = productId,
                Name = "ChairTest",
                Price = 123,
                Type = "Chair",
                Photolink = "Link"
            };
            
            _productRepositoryMock
                .Setup(x => x.DeleteProductByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(product);

            // Act
            var result = await _productService.DeleteProductByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductResponse>(result);
            Assert.Equal(productId, result.Id);
        }

        [Fact]
        public async void DeleteProductByIdAsync_ShouldReturnNull_WhenProductDoesNotExists()
        {
            // Arrange

            _productRepositoryMock
                .Setup(x => x.DeleteProductByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _productService.DeleteProductByIdAsync(1);

            // Assert
            Assert.Null(result);

        }
    }
}
