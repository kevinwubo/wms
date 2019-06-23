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
    public class CarrierController : BaseController
    {
        //
        // GET: /Carrier/
        public int PAGESIZE = 20;
        public ActionResult Index(string name, string mcode, int status = -1, int p = 1)
        {
            List<CarrierEntity> mList = null;

            int count = CarrierService.GetCarrierCount("", "", status);

            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "/Carrier";


            ViewBag.CarrierModel = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "Carrier00" && t.Status == 1).ToList();
            if (!string.IsNullOrEmpty(name) || status > -1 || !string.IsNullOrEmpty(mcode))
            {
                mList = CarrierService.GetCarrierInfoByRule(name, mcode, status, pager);
            }
            else
            {
                mList = CarrierService.GetCarrierInfoPager(pager);
            }
            ViewBag.Name = name ?? "";
            ViewBag.Status = status;
            ViewBag.ModelCode = mcode;
            ViewBag.Carrier = mList;
            ViewBag.Pager = pager;
            return View();
        }


        public ActionResult Edit(string cid)
        {
            ViewBag.CarrierModel = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "Carrier00" && t.Status == 1).ToList();
            if (!string.IsNullOrEmpty(cid))
            {
                ViewBag.Carrier = CarrierService.GetCarrierEntityById(cid.ToLong(0));
            }
            else
            {
                ViewBag.Carrier = new CarrierEntity();
            }

            return View();
        }

        public void Modify(CarrierEntity Carrier)
        {
            if (Carrier != null)
            {
                Carrier.OperatorID = CurrentUser.UserID;
            }
            CarrierService.ModifyCarrier(Carrier);
            Response.Redirect("/Carrier/");
        }

        public void Remove(string cid)
        {
            CarrierService.Remove(long.Parse(cid));
            Response.Redirect("/Carrier/");
        }

    }
}
