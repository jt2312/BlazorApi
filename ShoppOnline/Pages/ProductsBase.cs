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

		protected override async Task OnInitializedAsync()
		{
			Products = await ProductService.GetItems();
		}

		protected IOrderedEnumerable<IGrouping<int, ProductDTO>> GetGroupedProductsByCategory()
		{
			return	from produ in Products
			group produ by produ.CategoryId into prodByCatGroup
			orderby prodByCatGroup.Key
			select prodByCatGroup;
		}

		protected string GetCategoryName(IGrouping<int, ProductDTO> GroupedProductDTos) {
			return GroupedProductDTos.FirstOrDefault(pg => pg.CategoryId == GroupedProductDTos.Key).CategoryName;

		
		}

	}
}
