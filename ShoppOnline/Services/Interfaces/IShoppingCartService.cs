using ShopOnlineModels.Dtos;

namespace ShoppOnline.Services.Interfaces
{
	public interface IShoppingCartService
	{
		Task<IEnumerable<CartItemDTO>> GetItems(int userId);
		Task<CartItemDTO> AddItem(CartItemToAddDTO cartItemToAddDto);
		Task<CartItemDTO> DeleteItem(int id);
		Task<CartItemDTO> UpdateQty(CartItemQtyUpdateDTO cartItemQtyUpdateDto);

		event Action<int> OnShoppingCartChanged;
		void RaiseEventOnShoppingCartChanged(int totalQty);

	}
}
