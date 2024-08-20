using ShopOnlineModels.Dtos;

namespace ShoppOnline.Services.Interfaces
{
	public interface IShoppingCartService
	{
		Task<IEnumerable<CartItemDTO>> GetItems(int userId);
		Task<CartItemDTO> AddItem(CartItemToAddDTO cartItemToAddDto);


	}
}
