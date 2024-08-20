using ShopOnlineApi.Models;

namespace ShopOnlineApi.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetItems();
        Task<IEnumerable<ProductCategory>> GetCategories();
        Task<Product> GetItembyId(int Id);
        Task<ProductCategory> GetCategoryById(int Id);
    }
}
