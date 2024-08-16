using ShopOnlineApi.Models;
using ShopOnlineModels.Dtos;

namespace ShopOnlineApi.Extentions
{
	public static class DTOConversions
	{
		public static IEnumerable<ProductDTO> ConverToDto(this IEnumerable<Product> products,
																IEnumerable<ProductCategory> productCategories)
		{
			return (from product in products
					join productCategory in productCategories
					on product.CategoryId equals productCategory.Id
					select new ProductDTO
					{
						Id=product.Id,
						Name=product.Name,
						Description=product.Description,
						ImageURL=product.ImageURL,
						Price=product.Price,
						Qty=product.Qty,
						CategoryId = product.CategoryId,
						CategoryName = productCategory.Name
						

					}).ToList();
		}
		public static ProductDTO ConverToDto(this Product product,ProductCategory productCategories)
		{
			return new ProductDTO
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				ImageURL = product.ImageURL,
				Price = product.Price,
				Qty = product.Qty,
				CategoryId = product.CategoryId,
				CategoryName = product.Name

			};
		}
		public static IEnumerable<CartItemDTO> ConvertToDto(this IEnumerable<CartItem> cartItems,
															IEnumerable<Product> products)
		{
			return (from cartItem in cartItems
					join product in products
					on cartItem.ProductId equals product.Id
					select new CartItemDTO
					{
						Id = cartItem.Id,
						ProductId = cartItem.ProductId,
						ProductName = product.Name,
						ProductDescrition = product.Description,
						ProductImageURL = product.ImageURL,
						Price = product.Price,
						CartId = cartItem.CartId,
						Qty = cartItem.Qty,
						TotalPrice = product.Price * cartItem.Qty
					}).ToList();
		}
		public static CartItemDTO ConvertToDto(this CartItem cartItem,
													Product product)
		{
			return new CartItemDTO
			{
				Id = cartItem.Id,
				ProductId = cartItem.ProductId,
				ProductName = product.Name,
				ProductDescrition = product.Description,
				ProductImageURL = product.ImageURL,
				Price = product.Price,
				CartId = cartItem.CartId,
				Qty = cartItem.Qty,
				TotalPrice = product.Price * cartItem.Qty
			};
		}


	}
}
