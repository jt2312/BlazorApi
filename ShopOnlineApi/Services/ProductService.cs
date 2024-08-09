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
        public Task<ProductCategory> GetCategoryById(int Id)
		{
			throw new NotImplementedException();
		}

		public Task<Product> GetItembyId(int Id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<ProductCategory>> GetCategories()
		{
			var categories = await this._shoppOnlineDbContext.productCategories.ToListAsync();
			return categories;
		}

		public async Task<IEnumerable<Product>> GetItems()
		{
			var products = await this._shoppOnlineDbContext.Products.ToListAsync();
			return products;
			
		}
	}
}
