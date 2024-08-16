using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnlineApi.Extentions;
using ShopOnlineApi.Services;
using ShopOnlineModels.Dtos;

namespace ShopOnlineApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShoppingCartController : ControllerBase
	{
		private readonly IShoppingCartService _ShoppingCartService;
		private readonly IProductService _ProductService;

		public ShoppingCartController(IShoppingCartService ShoppingCartService,
									  IProductService ProductService)
		{
			_ShoppingCartService = ShoppingCartService;
			_ProductService = ProductService;
		}

		[HttpGet]
		[Route("{userId}/GetItems")]
		public async Task<ActionResult<IEnumerable<CartItemDTO>>> GetItems(int userId)
		{
			try
			{
				var cartItems = await this._ShoppingCartService.GetItems(userId);

				if (cartItems == null)
				{
					return NoContent();
				}

				var products = await this._ProductService.GetItems();

				if (products == null)
				{
					throw new Exception("No products exist in the system");
				}

				var cartItemsDto = cartItems.ConvertToDto(products);

				return Ok(cartItemsDto);

			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

			}
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<CartItemDTO>> GetItem(int id)
		{
			try
			{
				var cartItem = await this._ShoppingCartService.GetItem(id);
				if (cartItem == null)
				{
					return NotFound();
				}
				var product = await _ProductService.GetItembyId(cartItem.ProductId);

				if (product == null)
				{
					return NotFound();
				}
				var cartItemDto = cartItem.ConvertToDto(product);

				return Ok(cartItemDto);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpPost]
		public async Task<ActionResult<CartItemDTO>> PostItem([FromBody] CartItemToAddDTO cartItemToAddDTO)
		{
			try
			{
				var newCartItem = await this._ShoppingCartService.AddItem(cartItemToAddDTO);

				if (newCartItem == null)
				{
					return NoContent();
				}

				var product = await _ProductService.GetItembyId(newCartItem.ProductId);

				if (product == null)
				{
					throw new Exception($"Something went wrong when attempting to retrieve product (productId:({cartItemToAddDTO.ProductId})");
				}

				var newCartItemDto = newCartItem.ConvertToDto(product);

				return CreatedAtAction(nameof(GetItem), new { id = newCartItemDto.Id }, newCartItemDto);


			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult<CartItemDTO>> DeleteItem(int id)
		{
			try
			{
				var cartItem = await this._ShoppingCartService.DeleteItem(id);

				if (cartItem == null)
				{
					return NotFound();
				}

				var product = await this._ProductService.GetItembyId(cartItem.ProductId);

				if (product == null)
					return NotFound();

				var cartItemDto = cartItem.ConvertToDto(product);

				return Ok(cartItemDto);

			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
		[HttpPatch("{id:int}")]
		public async Task<ActionResult<CartItemDTO>> UpdateQty(int id, CartItemQtyUpdateDTO cartItemQtyUpdateDto)
		{
			try
			{
				var cartItem = await this._ShoppingCartService.UpdateQty(id, cartItemQtyUpdateDto);
				if (cartItem == null)
				{
					return NotFound();
				}

				var product = await _ProductService.GetItembyId(cartItem.ProductId);

				var cartItemDto = cartItem.ConvertToDto(product);

				return Ok(cartItemDto);

			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}

		}


	}
}
