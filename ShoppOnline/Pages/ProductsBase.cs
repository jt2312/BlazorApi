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
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
		{
			try
			{
                Products = await ProductService.GetItems();
				var shppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
				var totalQty = shppingCartItems.Sum(x => x.Qty);
				ShoppingCartService.RaiseEventOnShoppingCartChanged(totalQty);
            }
            catch (Exception)
			{

				throw;
			}
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
