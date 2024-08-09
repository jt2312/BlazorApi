using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnlineModels.Dtos
{
	public class CartItemToAddDTO
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
        public int Qty{ get; set; }
    }
}
