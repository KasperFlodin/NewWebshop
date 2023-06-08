namespace NewWebshopTests.Repositories
{
    public class ProductRepositoryTests
    {
        private readonly DbContextOptions<DatabaseContext> _options;
        private readonly DatabaseContext _context;
        private readonly ProductRepository _productRepository;

        public ProductRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "ProductRepositoryTests").Options;

            _context = new(_options);

            _productRepository = new(_context);
        }


        [Fact]
        public async void GetAllasync_ShouldReturnListOfProducts_WhenProductExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.Product.Add(new Product
            {
                Id = 1,
                Name = "Test",
                Price = 123,
                Type = "Chair",
                Photolink = "links",
            });

            _context.Product.Add(new Product
            {
                Id = 2,
                Name = "Testproduct2",
                Price = 321,
                Type = "Bed",
                Photolink = "linking",
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _productRepository.GetAllProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Product>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnEmptyListOfProducts_WhenNoProductExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _productRepository.GetAllProductsAsync();

            // Arrange
            Assert.NotNull(result);
            Assert.IsType<List<Product>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void CreateAsync_ShouldAddNewIdToProduct_WhenSavingToDatabase()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int expectedId = 1;

            Product product = new Product()
            {
                Id = 1,
                Name = "ThisIsATest",
                Price = 1234,
                Type = "Chair",
                Photolink = "links",
            };
             // Act
             var result = await _productRepository.CreateProductAsync(product);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(expectedId, result.Id);
        }

        [Fact]
        public async void CreateAsync_ShouldFailToAddNewProduct_WhenProductIdAlreadyExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int expectedId = 1;

            Product product = new Product()
            {
                Id = 1,
                Name = "ThisIsTest",
                Price = 132,
                Type = "Bed",
                Photolink = "LinkToPhoto"
            };

            await _productRepository.CreateProductAsync(product);

            // Act
            async Task action() => await _productRepository.CreateProductAsync(product);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int productId = 1;

            _context.Product.Add(new()
            {
                Id = productId,
                Name = "Fencing",
                Price = 123,
                Type = "Fence",
                Photolink = "",
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _productRepository.FindProductByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productId, result.Id);
        }

        [Fact]
        public async void FindProductByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int productId = 1;

            _context.Product.Add(new()
            {
                Id = productId,
                Name = "Tester",
                Price = 123,
                Type = "Chair",
                Photolink = "link"
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _productRepository.FindProductByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productId , result.Id);

        }

        [Fact]
        public async void FindProductByIdAsync_ShouldReturnProduct_WhenProductDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            await _context.SaveChangesAsync();

            // Act
            var result = await _productRepository.FindProductByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateProductByIdAsync_ShouldChangeValueOnProduct_WhenProductExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int productId = 1;

            Product product = new()
            {
                Id = productId,
                Name = "TestChair",
                Price = 1234,
                Type = "Chair",
                Photolink = "link"
            };
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            Product updateProduct = new()
            {
                Id = productId,
                Name = "newTestChair",
                Price = 4321,
                Type = "newChair",
                Photolink = "newlink"
            };
            // Act
            var result = await _productRepository.UpdateProductByIdAsync(productId, updateProduct);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.Id);
            Assert.IsType<Product>(result);
            Assert.Equal(updateProduct.Name, result.Name);
            Assert.Equal(updateProduct.Price, result.Price);
        }

        [Fact]
        public async void UpdateProductByIdAsync_ShouldReturnNull_WhenProductDoesNotExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int productId = 1;
            Product updateProduct = new()
            {
                Id = 1,
                Name = "TestChair",
                Price = 123,
                Type = "Chair",
                Photolink = "link"
            };

            // Act
            var result = await _productRepository.UpdateProductByIdAsync(productId, updateProduct);

            // Assert
            Assert.Null(result);
        }

    }
}
