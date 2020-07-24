using System;
using System.Collections.Generic;
using System.Text;

namespace IOT_Project_DAL
{
    public interface IDataAccess<T> where T : class, new()
    {
        IEnumerable<T> ShowAll();

        IEnumerable<T> ShowAll(int productId);

        int AddOrder(string sql);

        int UptKuCun(string sql);

    }
}
