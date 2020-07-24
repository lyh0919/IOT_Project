using IOT_Project_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT_Project_Api.DTO
{
    public class ProductInfoDto
    {
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public int Storage { get; set; }
		public int ProductAmount { get; set; }
		public decimal ProductPrice { get; set; }
		public decimal ProductDPrice { get; set; }
		public decimal ProductDiscount { get; set; }
		public int ProductDealamount { get; set; }
		public string ProductOutline { get; set; }
		public int ProductLookamount { get; set; }

        public List<ProductImg> ProductImgs { get; set; }
    }
}
