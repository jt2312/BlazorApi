using ShopOnlineModels.Dtos;
using ShoppOnline.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;

namespace ShoppOnline.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;

        public ProductService(HttpClient Http)
        {
            _http = Http;
        }

        public async Task<ProductDTO> GetItem(int id)
        {
            try
            {
                var response = await _http.GetAsync($"api/Product/{id}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default;
                    }
                    return await response.Content.ReadFromJsonAsync<ProductDTO>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {
                throw;

            }
        }

        public async Task<IEnumerable<ProductDTO>> GetItems()
        {
            try
            {
                var response = await _http.GetAsync("api/Product");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductDTO>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDTO>>();

                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }


            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<IEnumerable<ProductDTO>> GetItemsByCategory(int categoryId)
        {
            try
            {
                var response = await _http.GetAsync($"api/Product/{categoryId}/GetItemsByCategory");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductDTO>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDTO>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<IEnumerable<ProductCategoryDTO>> GetProductCategories()
        {
            try
            {
                var response = await _http.GetAsync("api/Product/GetProductCategories");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductCategoryDTO>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductCategoryDTO>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }
    }
}
