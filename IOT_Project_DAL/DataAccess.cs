using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace IOT_Project_DAL
{
    public class DataAccess<T>:IDataAccess<T> where T:class, new()
    {
        //private readonly string conn = "Data Source=192.168.1.112;Initial Catalog=Shop;User ID=sa;Pwd=123456";
        private readonly string conn = "Data Source=192.168.43.44;Initial Catalog=Shop;User ID=sa;Pwd=123456";
        public int AddOrder(string sql)
        {
            using (IDbConnection connection = new SqlConnection(conn))
            {
                return connection.Execute(sql);
            }
        }

        public int UptKuCun(string sql)
        {
            using (IDbConnection connection = new SqlConnection(conn))
            {
                return connection.Execute(sql);
            }
        }


        public IEnumerable<T> ShowAll()
        {
            var t = typeof(T);
            using (IDbConnection connection = new SqlConnection(conn))
            {
                return connection.Query<T>("select * from " + t.Name);
            }
        }

        public IEnumerable<T> ShowAll(int productId)
        {
            var t = typeof(T);
            using (IDbConnection connection = new SqlConnection(conn))
            {
                return connection.Query<T>("select * from " + t.Name + " where ProId = "+productId);
            }
        }
    }
}
