using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IOT_Project_Model
{
	 public class Orderdetail
	 {
		 public int  Id { get; set; }
		 public int  OrderDetailId { get; set; }
		 public string  OrderId { get; set; }
		 public int  ProductId { get; set; }
		 public int  Orderamount { get; set; }
		 public string  Poststatus { get; set; }
		 public string  Recevstatus { get; set; }
		 public string  Saletotalprice { get; set; }
	 }
}
