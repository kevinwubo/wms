using Common;
using Entity.ViewModel;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuoChe.Controllers
{
    public class GoodsController : BaseController
    {
        //
        // GET: /Goods/
        public int PAGESIZE = 20;
        public ActionResult Index(string name,string mcode,int CustomerID=-1,int status = -1, int p = 1)
        {
            List<GoodsEntity> mList = null;

            int count = GoodsService.GetGoodsCount(name, mcode, CustomerID, status);

            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "/Goods";

            //客户信息
            ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据
            ViewBag.GoodsModel = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "GoodsCode" && t.Status == 1).ToList();
            if (!string.IsNullOrEmpty(name) || status > -1 || CustomerID > -1 || !string.IsNullOrEmpty(mcode))
            {
                mList = GoodsService.GetGoodsInfoByRule(name, mcode, CustomerID, status, pager);
            }
            else
            {
                mList = GoodsService.GetGoodsInfoPager(pager);
            }
            
            ViewBag.Name = name ?? "";
            ViewBag.Status = status;
            ViewBag.Description = mcode;
            ViewBag.CustomerID = CustomerID;
            ViewBag.Goods = mList;
            ViewBag.Pager = pager;
            return View();
        }


        public ActionResult Edit(string cid)
        {
            //客户信息
            ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据
            ViewBag.GoodsModel = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "GoodsCode" && t.Status == 1).ToList();
            ViewBag.UnitsList = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "Units00" && t.Status == 1).ToList();  //单位      
            if (!string.IsNullOrEmpty(cid))
            {
                GoodsEntity entity= GoodsService.GetGoodsEntityById(cid.ToLong(0));
                entity.Url = UrlPar;
                ViewBag.Goods = entity;
            }
            else
            {
                ViewBag.Goods = new GoodsEntity();
            }

            return View();
        }

        public void Modify(GoodsEntity goods)
        {
            if (goods != null)
            { 
                goods.OperatorID = CurrentUser.UserID; 
            }
            GoodsService.ModifyGoods(goods);
            Response.Redirect("/Goods/Index?" + goods.Url);
        }

        public void Remove(string cid)
        {
            GoodsService.RemoveGoods(cid);
            Response.Redirect("/Goods/");
        }

    }
}
