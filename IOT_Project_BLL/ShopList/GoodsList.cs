using IOT_Project_DAL;
using IOT_Project_Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOT_Project_BLL.ShopCar
{
    public class GoodsList : IGoodsList
    {
        private IDataAccess<Productinfo> _data;
        public GoodsList(IDataAccess<Productinfo> data)
        {
            _data = data;
        }

        public IEnumerable<Productinfo> GetProductinfos()
        {
            return _data.ShowAll();
        }
    }
}
