using Microsoft.AspNetCore.Components;
using ShopOnlineModels.Dtos;
using ShoppOnline.Services.Interfaces;

namespace ShoppOnline.Pages
{
	public class ProductsBase : ComponentBase
	{
		[Inject]
        public IProductService ProductService { get; set; }
		public IEnumerable<ProductDTO> Products { get; set; }
	
	}
}
