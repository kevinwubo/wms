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
    public class ReceiverController : BaseController
    {
        //
        // GET: /Receiver/
        public int PAGESIZE = 20;
        public ActionResult Index(string name, string receiverType, int customerID = 0, int status = -1, int p = 1)
        {
            List<ReceiverEntity> mList = null;

            int count = ReceiverService.GetReceiverCount(name, receiverType, customerID, status);

            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "/Receiver";

            //客户信息
            ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据
            //收货方类型
            ViewBag.ReceiverList = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "Reveiver00" && t.Status == 1).ToList();

            if (!string.IsNullOrEmpty(name) || status > -1 || !string.IsNullOrEmpty(receiverType))
            {
                mList = ReceiverService.GetReceiverInfoByRule(name, receiverType,customerID, status, pager);
            }
            else
            {
                mList = ReceiverService.GetReceiverInfoPager(pager);
            }
            ViewBag.Name = name ?? "";
            ViewBag.Status = status;
            ViewBag.ReceiverType = receiverType;
            ViewBag.CustomerID = customerID;
            ViewBag.Receiver = mList;
            ViewBag.Pager = pager;
            return View();
        }


        public ActionResult Edit(string cid)
        {
            //省份
            ViewBag.Province = BaseDataService.GetAllProvince();
            //默认承运商
            ViewBag.Carrier = CarrierService.GetCarrierByRule("", 1);//只显示使用中的数据
            //默认仓库
            ViewBag.Storage = StorageService.GetStorageByRule("", 1);//只显示使用中的数据
            //客户信息
            ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据

            //收货方类型
            ViewBag.ReceiverList = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "Reveiver00" && t.Status == 1).ToList();
            if (!string.IsNullOrEmpty(cid))
            {
                ViewBag.Receiver = ReceiverService.GetReceiverEntityById(cid.ToLong(0));
            }
            else
            {
                ViewBag.Receiver = new ReceiverEntity();
            }

            return View();
        }

        public void Modify(ReceiverEntity Receiver)
        {
            if (Receiver != null)
            {
                Receiver.OperatorID = CurrentUser.UserID;
            }
            ReceiverService.ModifyReceiver(Receiver);
            Response.Redirect("/Receiver/");
        }

        public void Remove(string cid)
        {
            ReceiverService.Remove(long.Parse(cid));
            Response.Redirect("/Receiver/");
        }

    }
}
