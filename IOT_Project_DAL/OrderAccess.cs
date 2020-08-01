using Dapper;
using IOT_Project_Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IOT_Project_DAL
{
    public class OrderAccess : IOrderAccess
    {
        //删除
        public int DelOrderInfo(object id)
        {

            using (SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Shop;Integrated Security=True;"))
            {
                return conn.Execute("delete from  OrderInfo where OrderId in("+id+")");
            };

        }
           
    
        //查询显示
        public List<OrderInfo> OrderShow(string OrderId, string ReceverName)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Shop;Integrated Security=True;"))
            {
                if ((OrderId==null)&&(ReceverName==null))
                {
                    return conn.Query<OrderInfo>("select o.Id,o.OrderId,o.UserId,o.Orderdate,o.Orderamount,o.message,o.postmethod,o.paymethod," +
                   "o.recevername,o.receveraddr,o.recevertel,o.memo,o.totalprice from OrderInfo o join Customer c " +
                   "on o.UserId=c.UserId ").ToList();
                }
                else
                {
                    return conn.Query<OrderInfo>("select o.Id,o.OrderId,o.UserId,o.Orderdate,o.Orderamount,o.message,o.postmethod,o.paymethod," +
                   "o.recevername,o.receveraddr,o.recevertel,o.memo,o.totalprice from OrderInfo o join Customer c " +
                   "on o.UserId=c.UserId where o.OrderId like '%" + OrderId + "%' or o.ReceverNaem like '%" + ReceverName + "%'").ToList();
                }
               
            };
        }
        //订单详细显示
        public List<Orderdetail> DetailShow()
        {
            using (SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Shop;Integrated Security=True;"))
            {
                return conn.Query<Orderdetail>("select o.Id,o.OrderDetailId,o.OrderId,o.ProductId,o.orderamount," +
                    "o.poststatus,o.recevstatus,o.saletotalprice from Order ord join Orderdetail o on o.OrderId=ord.OrderId " +
                    " join ProductInfo p on o.ProductId=p.ProductId").ToList();
            };
        }
        //单个详情显示
        public List<OrderInfo> ShowOrder(int id)
        {
         
                using (SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Shop;Integrated Security=True;"))
                {
                    return conn.Query<OrderInfo>("select o.Id,o.OrderId,o.UserId,o.Orderdate,o.Orderamount,o.message,o.postmethod,o.paymethod," +
                   "o.recevername,o.receveraddr,o.recevertel,o.memo,o.totalprice from OrderInfo o join Customer c " +
                   "on o.UserId=c.UserId where o.Id="+id+"").ToList();
                };
          
        }
        /// <summary>
        /// 确认订单
        /// </summary>
        /// <param name="od"></param>
        /// <returns></returns>
        public int UptOrderInfo(OrderInfo od)
        {
           
         
                using (SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Shop;Integrated Security=True;"))
                {
                    return conn.Execute("update  OrderInfo set   OrderId='" + od.OrderId + "',UserId='" + od.UserId + "'," +
                        "Orderdate='" + od.Orderdate + "',Orderamount='" + od.Orderamount + "',Message='" + od.Message + "'," +
                        "Postmethod='" + od.Postmethod + "',Paymethod='" + od.Paymethod + "',Recevername='" + od.Recevername + "'," +
                        "Receveraddr='" + od.Receveraddr + "',Recevertel='" + od.Recevertel + "'," +
                        "Memo='" + od.Memo + "',Totalprice='" + od.Totalprice + "',)");

                }
        
        }
    
    }
}
