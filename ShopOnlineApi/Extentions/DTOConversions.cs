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
	}
}
