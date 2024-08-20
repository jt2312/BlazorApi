using Microsoft.EntityFrameworkCore;
using ShopOnlineApi.Data;
using ShopOnlineApi.Models;
using ShopOnlineModels.Dtos;

namespace ShopOnlineApi.Services
{
	public class ShoppingCartService : IShoppingCartService
	{
		private readonly ShoppOnlineDbContext _ShoppOnlineDbContext;

		public ShoppingCartService(ShoppOnlineDbContext shoppOnlineDbContext) {
			_ShoppOnlineDbContext = shoppOnlineDbContext;
		}


		private async Task<bool> CartItemExists(int cartId, int productId)
		{
			return await this._ShoppOnlineDbContext.CartItems.AnyAsync(
				c => c.CartId == cartId && 
				c.ProductId == productId);

		}
		public async Task<CartItem> AddItem(CartItemToAddDTO cartItemToAddDTO)
		{
			if (await CartItemExists(cartItemToAddDTO.CartId, cartItemToAddDTO.ProductId) == false)
			{
				var item = await (from product in this._ShoppOnlineDbContext.Products
								  where product.Id == cartItemToAddDTO.ProductId
								  select new CartItem
								  {
									  CartId = cartItemToAddDTO.CartId,
									  ProductId = product.Id,
									  Qty = cartItemToAddDTO.Qty

								  }).SingleOrDefaultAsync();
				if (item != null)
				{
					var result = await this._ShoppOnlineDbContext.CartItems.AddAsync(item);
					await this._ShoppOnlineDbContext.SaveChangesAsync();
					return result.Entity;
				}
			}
			return null;
		}


		public async Task<CartItem> DeleteItem(int id)
		{
			var item = await this._ShoppOnlineDbContext.CartItems.FindAsync(id);

			if (item != null)
			{
				this._ShoppOnlineDbContext.CartItems.Remove(item);
				await this._ShoppOnlineDbContext.SaveChangesAsync();
			}

			return item;

		}
		  
		public async Task<CartItem> GetItem(int id)
		{
			return await (from cart in this._ShoppOnlineDbContext.Carts
						  join cartItem in this._ShoppOnlineDbContext.CartItems
						  on cart.Id equals cartItem.CartId
						  where cartItem.Id == id
						  select new CartItem
						  {	
							  Id = cartItem.Id,
							  ProductId = cartItem.ProductId,
							  Qty = cartItem.Qty,
							  CartId = cartItem.CartId

						  }).SingleOrDefaultAsync();
		}


		public async Task<IEnumerable<CartItem>> GetItems(int userId)
		{
			return await (from cart in this._ShoppOnlineDbContext.Carts
						  join cartItem in this._ShoppOnlineDbContext.CartItems
						  on cart.Id equals cartItem.CartId
						  where cart.UserId == userId
						  select new CartItem
						  {
							  Id = cartItem.Id,
							  ProductId = cartItem.ProductId,
							  Qty = cartItem.Qty,
							  CartId = cartItem.CartId
						  }).ToListAsync();
		}

		public async Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDTO cartItemQtyUpdateDTO)
		{
			var item = await this._ShoppOnlineDbContext.CartItems.FindAsync(id);

			if (item != null)
			{
				item.Qty = cartItemQtyUpdateDTO.Qty;
				await this._ShoppOnlineDbContext.SaveChangesAsync();
				return item;
			}

			return null;
		}
	}
}
