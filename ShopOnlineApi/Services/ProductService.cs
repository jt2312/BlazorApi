using Microsoft.EntityFrameworkCore;
using ShopOnlineApi.Data;
using ShopOnlineApi.Models;

namespace ShopOnlineApi.Services
{
    public class ProductService : IProductService
	{
		private readonly ShoppOnlineDbContext _shoppOnlineDbContext;
        public ProductService(ShoppOnlineDbContext shoppOnlineDbContext)
        {
			_shoppOnlineDbContext = shoppOnlineDbContext;
            
        }
        public async Task<ProductCategory> GetCategoryById(int Id)
		{
			var category = await _shoppOnlineDbContext.productCategories.SingleOrDefaultAsync(x => x.Id == Id);

			return category;
		
		}

		public async Task<Product> GetItembyId(int Id)
		{
			var product = await _shoppOnlineDbContext.Products.FindAsync(Id);
			return product;
		}

		public async Task<IEnumerable<ProductCategory>> GetCategories()
		{
			var categories = await _shoppOnlineDbContext.productCategories.ToListAsync();
			return categories;
		}

		public async Task<IEnumerable<Product>> GetItems()
		{
			var products = await this._shoppOnlineDbContext.Products.ToListAsync();
			return products;
			
		}

        public async Task<IEnumerable<Product>> GetItemsByCategory(int id)
        {
            var products = await this._shoppOnlineDbContext.Products
                                     .Include(p => p.ProductCategory)
                                     .Where(p => p.CategoryId == id).ToListAsync();
            return products;
        }
    }
}
