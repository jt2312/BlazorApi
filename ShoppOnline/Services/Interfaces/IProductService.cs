using ShopOnlineModels.Dtos;

namespace ShoppOnline.Services.Interfaces
{
	public interface IProductService
	{
		Task<IEnumerable<ProductDTO>> GetItems();
		Task<ProductDTO> GetItem(int id);
        Task<IEnumerable<ProductCategoryDTO>> GetProductCategories();
        Task<IEnumerable<ProductDTO>> GetItemsByCategory(int categoryId);
    }
}
