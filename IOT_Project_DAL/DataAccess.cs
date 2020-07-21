using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace IOT_Project_DAL
{
    public class DataAccess : IDataAccess
    {
        private readonly string con = "Data Source=192.168.1.116;Initial Catalog=Shop;User ID=sa;Pwd=123456";

        public int Add<T>(T t)
        {
            throw new NotImplementedException();

        }

        public IList<AdminInfo> ShowAll<AdminInfo>()
        {
            using (IDbConnection connection = new SqlConnection(con))
            {
                return connection.Query<AdminInfo>("select * from AdminInfo").ToList();
            }
            
        }

        public int Update<T>(T t)
        {
            throw new NotImplementedException();
        }

        public int UpdatePart(int id, int num)
        {
            throw new NotImplementedException();
        }
    }
}
