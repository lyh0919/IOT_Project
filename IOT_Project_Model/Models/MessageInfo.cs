using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IOT_Project_Model
{
	 public class MessageInfo
	 {
		 public int  ProductId { get; set; }
		 public int  MessageId { get; set; }
		 public string  MessageType { get; set; }
		 public string  Messagetitle { get; set; }
		 public string  Messagecontent { get; set; }
		 public int  UserId { get; set; }
		 public string  Username { get; set; }
		 public DateTime  Commentdate { get; set; }
	 }
}
