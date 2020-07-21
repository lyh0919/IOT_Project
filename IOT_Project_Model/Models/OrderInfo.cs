using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IOT_Project_Model
{
	 public class OrderInfo
	 {
		 public int  Id { get; set; }
		 public string  OrderId { get; set; }
		 public int  UserId { get; set; }
		 public DateTime  Orderdate { get; set; }
		 public int  Orderamount { get; set; }
		 public string  Message { get; set; }
		 public string  Postmethod { get; set; }
		 public string  Paymethod { get; set; }
		 public string  Recevername { get; set; }
		 public string  Receveraddr { get; set; }
		 public string  Recevertel { get; set; }
		 public string  Memo { get; set; }
		 public string  Totalprice { get; set; }
	 }
}
