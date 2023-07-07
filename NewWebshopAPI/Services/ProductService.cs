namespace NewWebshopAPI.Services
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllProductsAsync();
        Task<ProductResponse> FindProductByIdAsync(int productId);
        Task<List<ProductResponse>> FindProductByTypeAsync(string product);
        Task<ProductResponse> CreateProductAsync(ProductRequest newProduct);
        Task<ProductResponse> DeleteProductByIdAsync(int productId);
        Task<ProductResponse> UpdateProductByIdAsync(int productId, ProductRequest updateProduct);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        private static ProductResponse MapProductToProductResponse(Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Type = product.Type,
                Photolink = product.Photolink,
            };
        }

        private Product MapProductRequestToProduct(ProductRequest productRequest)
        {
            return new Product
            {
                Name = productRequest.Name,
                Type = productRequest.Type,
                Photolink = productRequest.Photolink,
            };
        }

        public async Task<List<ProductResponse>> GetAllProductsAsync()
        {
            List<Product> products = await _productRepository.GetAllProductsAsync();

            if (products == null)
            {
                throw new ArgumentNullException();
            }
            return products.Select(MapProductToProductResponse).ToList();
        }

        public async Task<ProductResponse> FindProductByIdAsync(int productId)
        {
            Product product = await _productRepository.FindProductByIdAsync(productId);

            if (product != null)
            {
                return MapProductToProductResponse(product);
            }

            return null;
        }

        public async Task<List<ProductResponse>> FindProductByTypeAsync(string productType)
        {
            List<Product> product = await _productRepository.FindProductByTypeAsync(productType);

            if (product == null)
            {
                throw new ArgumentNullException();
            }

            //return MapProductToProductResponse(product);
            return product.Select(MapProductToProductResponse).ToList();
        }

        public async Task<ProductResponse> CreateProductAsync(ProductRequest newProduct)
        {
            var product = await _productRepository.CreateProductAsync(MapProductRequestToProduct(newProduct));

            if (product == null)
            {
                throw new ArgumentNullException();
            }

            return MapProductToProductResponse(product);
        }

        public async Task<ProductResponse> DeleteProductByIdAsync(int productId)
        {
            var product = await _productRepository.DeleteProductByIdAsync(productId);

            if (product != null)
            {
                return MapProductToProductResponse(product);
            }
            return null;
        }

        public async Task<ProductResponse> UpdateProductByIdAsync(int productId, ProductRequest updateProduct)
        {
            var product = await _productRepository.UpdateProductByIdAsync(productId, MapProductRequestToProduct(updateProduct));

            if (product != null)
            {
                return MapProductToProductResponse(product);
            }

            return null;
        }
    }
}
