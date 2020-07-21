using System;
using System.Collections.Generic;
using System.Text;

namespace IOT_Project_DAL
{
    public interface IDataAccess
    {
        IList<T> ShowAll<T>();
        int Add<T>(T t);
        int Update<T>(T t);
        //修改库存
        int UpdatePart(int id,int num);
    }
}
