using ShopOnlineModels.Dtos;

namespace ShoppOnline.Services.Interfaces
{
	public interface IProductService
	{
		Task<IEnumerable<ProductDTO>> GetItems();

	}
}
