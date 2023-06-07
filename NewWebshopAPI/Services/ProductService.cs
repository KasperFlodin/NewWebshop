using NewWebshopAPI.DTOs.ProductDTOs;
using NewWebshopAPI.Repositories;

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
            List<Product> products = await _productRepository.GetAllProducts();

            if (products == null)
            {
                throw new ArgumentNullException();
            }
            return products.Select(MapProductToProductResponse).ToList();
        }

        public async Task<ProductResponse> FindProductByIdAsync(int productId)
        {
            var product = await _productRepository.FindProductById(productId);

            if (product != null)
            {
                return MapProductToProductResponse(product);
            }

            return null;
        }

        public async Task<List<ProductResponse>> FindProductByTypeAsync(string productType)
        {
            List<Product> product = await _productRepository.FindProductByType(productType);

            if (product == null)
            {
                throw new ArgumentNullException();
            }

            //return MapProductToProductResponse(product);
            return product.Select(MapProductToProductResponse).ToList();
        }

        public async Task<ProductResponse> CreateProductAsync(ProductRequest newProduct)
        {
            var product = await _productRepository.CreateProduct(MapProductRequestToProduct(newProduct));

            if (product == null)
            {
                throw new ArgumentNullException();
            }

            return MapProductToProductResponse(product);
        }

        public async Task<ProductResponse> DeleteProductByIdAsync(int productId)
        {
            var product = await _productRepository.DeleteProductById(productId);

            if (product != null)
            {
                return MapProductToProductResponse(product);
            }
            return null;
        }

        public async Task<ProductResponse> UpdateProductByIdAsync(int productId, ProductRequest updateProduct)
        {
            var product = await _productRepository.UpdateProductById(productId, MapProductRequestToProduct(updateProduct));

            if (product != null)
            {
                return MapProductToProductResponse(product);
            }

            return null;
        }
    }
}
