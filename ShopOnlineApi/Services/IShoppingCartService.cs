using ShopOnlineApi.Models;
using ShopOnlineModels.Dtos;

namespace ShopOnlineApi.Services
{
	public interface IShoppingCartService
	{
		Task<CartItem> AddItem(CartItemToAddDTO cartItemToAddDTO);
		Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDTO cartItemQtyUpdateDTO);
		Task<CartItem> DeleteItem(int id);
		Task<CartItem> GetItem(int id);
		Task<IEnumerable<CartItem>> GetItems(int userid);
	}
}
