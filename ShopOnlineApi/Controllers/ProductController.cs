using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnlineApi.Extentions;
using ShopOnlineApi.Models;
using ShopOnlineApi.Services;
using ShopOnlineModels.Dtos;

namespace ShopOnlineApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _ProductService;

		public ProductController(IProductService productService)
		{
			_ProductService = productService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductDTO>>> GetItems()
		{
			try
			{
				var products = await this._ProductService.GetItems();
				var productsCategories = await this._ProductService.GetCategories();

				if (products == null || productsCategories == null)
				{
					return NotFound();
				}
				else
				{
					var productsDTOs = products.ConverToDto(productsCategories);
					return Ok(productsDTOs);

				}
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error retrieving data from the database");

			}
		}
		[HttpGet("{id:int}")]
		public async Task<ActionResult<ProductDTO>> GetItembyId(int id)
		{
			try
			{
				var product = await this._ProductService.GetItembyId(id);

				if (product == null)
				{
					return BadRequest();
				}
				else
				{
					var productCategory = await this._ProductService.GetCategoryById(product.CategoryId);
					var productDTO = product.ConverToDto(productCategory);
					return Ok(productDTO);

				}
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error retrieving data from the database");

			}
		}
		[HttpGet]
		[Route(nameof(GetProductCategories))]
		public async Task<ActionResult<IEnumerable<ProductCategoryDTO>>> GetProductCategories()
		{
			try
			{
				var productCategories = await _ProductService.GetCategories();

				var productCategoryDtos = productCategories.ConvertToDto();

				return Ok(productCategoryDtos);

			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
								"Error retrieving data from the database");
			}

		}

		[HttpGet]
		[Route("{categoryId}/GetItemsByCategory")]
		public async Task<ActionResult<IEnumerable<ProductDTO>>> GetItemsByCategory(int categoryId)
		{
			try
			{
				var products = await _ProductService.GetItemsByCategory(categoryId);

				var productDtos = products.ConvertToDto();

				return Ok(productDtos);

			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
								"Error retrieving data from the database");
			}
		}


	}
}




