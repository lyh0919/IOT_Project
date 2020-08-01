using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IOT_Project_Model
{
	 public class ProductInfo
	 {
		 public int  ProductId { get; set; }
		 public int  ProductTypeId { get; set; }
		 public string  ProductName { get; set; }
		 public int  Storage { get; set; }
		 public int  ProductAmount { get; set; }
		 public decimal  ProductPrice { get; set; }
		 public decimal  ProductDPrice { get; set; }
		 public decimal  ProductDiscount { get; set; }
		 public int  ProductDealamount { get; set; }
		 public string  ProductOutline { get; set; }
		 public DateTime  ProductStoretime { get; set; }
		 public int  ProductLookamount { get; set; }
	 }
}
