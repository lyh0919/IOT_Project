using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace IOT_Project_Common
{
    public class Md5Helper
    {
        public  string ToMd5(string TTT)
        {
            var MD5 = System.Security.Cryptography.MD5.Create();
            byte[] source = Encoding.UTF8.GetBytes(TTT);
            byte[] result = MD5.ComputeHash(source);
            string NTTT = "";
            foreach (byte b in result)
            {
                NTTT += b.ToString("X2");
            }
            return NTTT;
        }

        //public bool TransactionCancelOrder(string guid, ref int rCount, ref string errorInfo)
        //{
        //    StringBuilder strSql = new StringBuilder();

        //    SqlConnection conn = new SqlConnection(connectionString);
        //    conn.Open();
        //    SqlTransaction tran;
        //    tran = conn.BeginTransaction();
        //    SqlCommand comm = conn.CreateCommand();
        //    comm.Connection = conn;
        //    comm.Transaction = tran;

        //    try
        //    {
        //        strSql.Append("delete from OrderInfo ");
        //        strSql.Append(" where guid = '" + guid + "'  ");
        //        //rCount += DbHelperSQL.ExecuteSql(strSql.ToString());
        //        comm.CommandText = strSql.ToString();
        //        rCount += comm.ExecuteNonQuery();

        //        strSql = new StringBuilder();
        //        strSql.Append("delete from OrderState ");
        //        strSql.Append(" where oGuid = '" + guid + "'  ");
        //        //rCount += DbHelperSQL.ExecuteSql(strSql.ToString());
        //        comm.CommandText = strSql.ToString();
        //        rCount += comm.ExecuteNonQuery();

        //        tran.Commit();
        //        conn.Close();
        //        return true;

        //    }
        //    catch (Exception ex)
        //    {
        //        tran.Rollback();
        //        errorInfo = strSql.ToString();
        //        conn.Close();
        //        return false;
        //    }
        //    finally
        //    {
        //        //conn.Close();
        //    }

        //}
    }
}