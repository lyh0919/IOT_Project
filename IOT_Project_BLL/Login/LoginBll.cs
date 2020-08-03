using IOT_Project_Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Linq;

namespace IOT_Project_BLL.Login
{
    public class LoginBll : ILogin
    {
        //Data Source=192.168.43.44;Initial Catalog=Shop;User ID=sa;Pwd=123456
        private readonly string conn = "Data Source=192.168.43.44;Initial Catalog=Shop;Integrated Security=True";
      
        public Customer ShowCustomer(Customer m)
        {
            string sql = "";
            using (SqlConnection db=new SqlConnection(conn))
            {
                
                    sql= $"select * from Customer where UserName='" + m.UserName + "' and UserPwd='" + m.UserPwd + "'";

                Customer customer = db.Query<Customer>(sql).FirstOrDefault();
                return customer;
            }
        }

        public int Add(Customer m)
        {
            using (SqlConnection db = new SqlConnection(conn))
            {
                string sql = $"insert into Customer values('"+m.UserName+"','"+m.UserPwd+"','"+m.UserSex+"','"+m.UserRealName+"','"+m.UserTel+"','"+m.UserBirthday+"','"+m.UserLevel+"','"+m.UserAddress+"','"+m.UserEmail+"')";

                return db.Execute(sql); 
            }
        }

        public int Update(Customer m)
        {
            using (SqlConnection db = new SqlConnection(conn))
            {
               string sql = $"update  Customer set UserName=" + m.UserName + "',UserPwd='" + m.UserPwd + "',UserSex='" + m.UserSex + "',UserRealName='" + m.UserRealName + "',UserTel='" + m.UserTel + "',UserBirthday='" + m.UserBirthday + "',UserLevel='" + m.UserLevel + "',UserAddress='" + m.UserAddress + "',UserEmail='" + m.UserEmail + "' where UserId="+m.UserId;

                return db.Execute(sql);
            }
        }

        public List<OrderInfo> ShowOrder()
        {
            using (SqlConnection db = new SqlConnection(conn))
            {
                string sql = $"select * from OrderInfo";
                return db.Query<OrderInfo>(sql).ToList();
            }
        }

        public List<Orderdetail> ShowOrderdetial()
        {
            using (SqlConnection db = new SqlConnection(conn))
            {
                string sql = $"select * from [dbo].[Orderdetail]";
                return db.Query<Orderdetail>(sql).ToList();
            }
         
        }

        public List<Customer> ShowCustome()
        {
            string sql = "";
            using (SqlConnection db = new SqlConnection(conn))
            {
                //if (name!=null)
               // {
                    sql = $"select * from Customer";
                    
                //}
                //else
                //{
                //   sql = $"select * from Customer";
                //}
                return db.Query<Customer>(sql).ToList();
            }
          
        }

        public List<ProductInfo> ShowProductinfo()
        {
            using (SqlConnection db = new SqlConnection(conn))
            {
                string sql = $"select * from Productinfo";
                return db.Query<ProductInfo>(sql).ToList();
            }
        }

        public List<producttypeinfo> ShowProducttype()
        {
            using (SqlConnection db = new SqlConnection(conn))
            {
                string sql = $"select * from producttypeinfo";
                return db.Query<producttypeinfo>(sql).ToList();
            }
        }
        public List<ProductImg> ShowPicture()
        {
            using (SqlConnection db = new SqlConnection(conn))
            {
                string sql = $"select * from ProductImg";
                return db.Query<ProductImg>(sql).ToList();
            }
        }
    }
}
