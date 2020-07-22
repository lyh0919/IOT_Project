using IOT_Project_Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOT_Project_BLL.ShopCar
{
    public interface IGoodsList
    {
        IEnumerable<Productinfo> GetProductinfos();
    }
}
