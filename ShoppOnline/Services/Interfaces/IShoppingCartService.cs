using ShopOnlineModels.Dtos;

namespace ShoppOnline.Services.Interfaces
{
	public interface IShoppingCartService
	{
		Task<List<CartItemDTO>> GetItems(int userId);
		Task<CartItemDTO> AddItem(CartItemToAddDTO cartItemToAddDto);
		Task<CartItemDTO> DeleteItem (int Id);
		Task<CartItemDTO> UpdateQty(CartItemQtyUpdateDTO cartItemQtyUpdateDTO);

	}
}
