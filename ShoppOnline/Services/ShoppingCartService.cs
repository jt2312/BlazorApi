using ShopOnlineModels.Dtos;
using ShoppOnline.Pages;
using ShoppOnline.Services.Interfaces;
using System.Net.Http.Json;

namespace ShoppOnline.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly HttpClient _httpClient;
        public ShoppingCartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CartItemDTO> AddItem(CartItemToAddDTO cartItemToAddDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<CartItemToAddDTO>("api/ShoppingCart", cartItemToAddDto);
                if (response.IsSuccessStatusCode) 
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(CartItemDTO);
                    }
                    return await response.Content.ReadFromJsonAsync<CartItemDTO>();
                }
                else
                {
                    var message = await  response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status:{ response.StatusCode} - Message - { message }");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CartItemDTO> DeleteItem(int Id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/ShoppingCart/{Id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CartItemDTO>();
                }
                return default(CartItemDTO);
            }
            catch (Exception)
            {

                throw;
            }
            

        }

        public async Task<List<CartItemDTO>> GetItems(int userId)
        {
            try
            {
                
                var response = await _httpClient.GetAsync($"api/ShoppingCart/{userId}/GetItems");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<CartItemDTO>().ToList();
                    }
                    return await response.Content.ReadFromJsonAsync<List<CartItemDTO>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} - Message -{message}");
                }
            }
            catch (Exception)
            {

                throw;
            }


        }


    }
}
