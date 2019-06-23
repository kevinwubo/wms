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
    public class PriceSetController : BaseController
    {
        //
        // GET: /PriceSet/
        public int PAGESIZE = 20;
        public ActionResult Index(int carrierid = 0, int storageid = 0, int customerid = 0, int status = -1, int p = 1)
        {
            List<PriceSetEntity> mList = null;

            int count = PriceSetService.GetPriceSetCount("", carrierid, storageid, customerid, status);

            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "/PriceSet";


            ViewBag.PriceSetModel = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "PriceSetCode" && t.Status == 1).ToList();
            if (status > -1 || carrierid > 0 || storageid > 0 || customerid > 0)
            {
                mList = PriceSetService.GetPriceSetInfoByRule("", carrierid, storageid, customerid, status, pager);
            }
            else
            {
                mList = PriceSetService.GetPriceSetInfoPager(pager);
            }
            //默认承运商
            ViewBag.Carrier = CarrierService.GetCarrierByRule("", 1);//只显示使用中的数据
            //默认仓库
            ViewBag.Storage = StorageService.GetStorageByRule("", 1);//只显示使用中的数据
            //门店
            ViewBag.Goods = GoodsService.GetGoodsByRule("", 1);//只显示使用中的数据
            //客户信息
            ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据

            ViewBag.Status = status;
            ViewBag.carrierid = carrierid;
            ViewBag.customerid = customerid;
            ViewBag.storageid = storageid;
            ViewBag.PriceSet = mList;
            ViewBag.Pager = pager;
            return View();
        }

        
        public ActionResult Edit(string cid)
        {
            //默认承运商
            ViewBag.Carrier = CarrierService.GetCarrierByRule("", 1);//只显示使用中的数据
            //默认仓库
            ViewBag.Storage = StorageService.GetStorageByRule("", 1);//只显示使用中的数据
            //门店
            ViewBag.Goods = GoodsService.GetGoodsByRule("", 1);//只显示使用中的数据
            //客户信息
            ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据
            //温度
            ViewBag.TemList = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "TM00" && t.Status == 1).ToList();
            //收货方类型
            ViewBag.ReceiverList = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "Reveiver00" && t.Status == 1).ToList();
            //收货方
            ViewBag.DeliverList = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "DeliverModel00" && t.Status == 1).ToList();
            if (!string.IsNullOrEmpty(cid))
            {
                ViewBag.PriceSet = PriceSetService.GetPriceSetEntityById(cid.ToLong(0));
            }
            else
            {
                ViewBag.PriceSet = new PriceSetEntity();
            }

            return View();
        }

        public void Modify(PriceSetEntity PriceSet)
        {
            if (PriceSet != null)
            {
                PriceSet.OperatorID = CurrentUser.UserID;
            }
            PriceSetService.ModifyPriceSet(PriceSet);
            Response.Redirect("/PriceSet/");
        }

        public void Remove(string cid)
        {
            PriceSetService.Remove(long.Parse(cid));
            Response.Redirect("/PriceSet/");
        }

    }
}
