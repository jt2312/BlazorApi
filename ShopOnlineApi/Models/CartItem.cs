using System.Collections.Generic;

namespace ShopOnlineApi.Models
{
	public class CartItem 
	{
		public int Id { get; set; }
		public int CartId { get; set; }
		public int ProductId { get; set; }
		public int Qty { get; set; }
		
		//to do: eliminar 
		//public int CategoryId { get; set; }

	}
}
