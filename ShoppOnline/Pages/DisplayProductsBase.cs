using Microsoft.AspNetCore.Components;
using ShopOnlineModels.Dtos;

namespace ShoppOnline.Pages
{
	public class DisplayProductsBase:ComponentBase
	{
		[Parameter]
		public IEnumerable<ProductDTO> Products { get; set; }
			

	}
}
