using ShopOnlineModels.Dtos;
using System.Net.Http.Json;

namespace ShoppOnline.Services.Interfaces
{
	public class ProductService : IProductService
	{
		private readonly HttpClient _http;

		public ProductService(HttpClient Http)
        {
			_http = Http;
		}
        public async Task<IEnumerable<ProductDTO>> GetItems()
		{
			try
			{
				var products = await this._http.GetFromJsonAsync<IEnumerable<ProductDTO>>("api/Product");
				return products;	
			
			}
			catch (Exception)
			{

				throw;
			}		
		}
	}
}
