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
        private readonly string conn = "Data Source=192.168.1.116;Initial Catalog=Shop;User ID=sa;Pwd=123456";

        public IEnumerable<T> ShowAll()
        {
            var t = typeof(T);
            using (IDbConnection connection = new SqlConnection(conn))
            {
                return connection.Query<T>("select * from " + t.Name);
            }
        }
    }
}
