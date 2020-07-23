using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IOT_Project_Api.DTO;
using IOT_Project_BLL.ShopCar;
using IOT_Project_DAL;
using IOT_Project_Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IOT_Project_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private IGoodsList _goodsList;
        public DefaultController(IGoodsList goodsList)
        {
            _goodsList = goodsList;
        }
        
        //显示列表
        [HttpGet]
        public List<ProductInfoDto> GetGoodsList(int goodTypeId,string goodName)
        {
            var goodlist = _goodsList.GetProductinfos();
            if (goodTypeId != 0)
            {
                switch (goodTypeId)
                {
                    case 1:
                        goodlist = goodlist.Where(g => g.ProductTypeId == goodTypeId);
                        break;
                    case 2:
                        goodlist = goodlist.Where(g => g.ProductTypeId == goodTypeId);
                        break;
                    case 3:
                        goodlist = goodlist.Where(g => g.ProductTypeId == goodTypeId);
                        break;
                }
            }
            if (!string.IsNullOrWhiteSpace(goodName))
            {
                goodlist = goodlist.Where(g => g.ProductName.Contains(goodName));
            }
            var goodlistimg = from g in goodlist
                              select new ProductInfoDto
                              {
                                  ProductId = g.ProductId,
                                  ProductName = g.ProductName,
                                  Storage = g.Storage,
                                  ProductAmount = g.ProductAmount,
                                  ProductPrice = g.ProductPrice,
                                  ProductDPrice = g.ProductDPrice,
                                  ProductDiscount = g.ProductDiscount,
                                  ProductDealamount = g.ProductDealamount,
                                  ProductOutline = g.ProductOutline,
                                  ProductLookamount = g.ProductLookamount,
                                  ProductImgs = _goodsList.GetProductImgs(g.ProductId).ToList()
                              };

            return goodlistimg.ToList();
        }
        //商品详情
        [HttpGet]
        public ProductInfoDto GetProductInfo(int productId)
        {
            ProductInfo g = _goodsList.GetProductinfo(productId);
            ProductInfoDto productInfoDto = new ProductInfoDto
            {
                ProductId = g.ProductId,
                ProductName = g.ProductName,
                Storage = g.Storage,
                ProductAmount = g.ProductAmount,
                ProductPrice = g.ProductPrice,
                ProductDPrice = g.ProductDPrice,
                ProductDiscount = g.ProductDiscount,
                ProductDealamount = g.ProductDealamount,
                ProductOutline = g.ProductOutline,
                ProductLookamount = g.ProductLookamount,
                ProductImgs = _goodsList.GetProductImgs(g.ProductId).ToList()
            };
            return productInfoDto;
        }

        

    }
}
