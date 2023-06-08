namespace NewWebshopAPI.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product?> FindProductByIdAsync(int productId);
        Task<List<Product>> FindProductByTypeAsync(string productType);
        Task<Product?> CreateProductAsync(Product product);
        Task<Product?> DeleteProductByIdAsync(int productId);
        Task<Product?> UpdateProductByIdAsync(int productId, Product product);
    }
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext _context;

        public ProductRepository(DatabaseContext context)
        {
            _context = context;
        }

        // Read
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Product.ToListAsync(); // use include for foreign key connections here
        }

        public async Task<Product?> FindProductByIdAsync(int productId)
        {
            return await _context.Product.FirstOrDefaultAsync(i => i.Id == productId); // use include for foreign key connections here
        }

        public async Task<List<Product>> FindProductByTypeAsync(string productType)
        {
            return await _context.Product.Where(t => t.Type == productType).ToListAsync(); // use include for foreign key connections here
        }

        // Create
        public async Task<Product?> CreateProductAsync(Product newProduct)
        {
            _context.Product.Add(newProduct);
            await _context.SaveChangesAsync();
            return newProduct;
        }

        // Delete
        public async Task<Product?> DeleteProductByIdAsync(int productId)
        {
            var product = await FindProductByIdAsync(productId);

            if (product != null)
            {
                _context.Remove(product);
                await _context.SaveChangesAsync();
            }

            return product;
        }

        // Update
        public async Task<Product?> UpdateProductByIdAsync(int productId, Product updateProduct)
        {
            var product = await FindProductByIdAsync(productId);

            if (product != null)
            {
                product.Name = updateProduct.Name;
                product.Price = updateProduct.Price;
                product.Type = updateProduct.Type;

                await _context.SaveChangesAsync();
            }
            return product;
        }

    }
}
