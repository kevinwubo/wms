using Common;
using Infrastructure.Helper;
using Entity.ViewModel;
using Infrastructure.Cache;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;
using System.Configuration;

namespace GuoChe.Controllers
{
    public class CarController : BaseController
    {
        public int PAGESIZE = 20;

        public ActionResult Index(string name,string mcode,int status=-1,int p=1)
        {
            List<CarEntity> mList = null;

            int count = CarService.GetCarCount(name, mcode, status);

            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "/Car";


            ViewBag.CarModel = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "C00" && t.Status == 1).ToList();
            if (!string.IsNullOrEmpty(name) || status > -1 || !string.IsNullOrEmpty(mcode))
            {
                mList = CarService.GetCarInfoByRule(name, mcode, status,pager);
            }
            else
            {
                mList = CarService.GetCarInfoPager(pager);
            }
            ViewBag.Name = name ?? "";
            ViewBag.Status = status;
            ViewBag.ModelCode = mcode;
            ViewBag.Cars = mList;
            ViewBag.Pager = pager;
            return View();
        }

        public ActionResult Edit(string cid)
        {
            ViewBag.Brands = BrandService.GetBrandAll().Where(t => t.IsUse == 1).ToList();
            ViewBag.Store = StoreService.GetStoreAll().Where(t => t.Status == 1).ToList();
            ViewBag.CarModel = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "C00" && t.Status == 1).ToList();
            ViewBag.MaxPicCount = ConfigurationManager.AppSettings["MaxPicCount"].ToString();
            if (!string.IsNullOrEmpty(cid))
            {
                ViewBag.Car = CarService.GetCarEntityById(cid.ToLong(0));
            }
            else
            {
                ViewBag.Car = new CarEntity();
            }
            
            return View();
        }

        public void Modify(CarEntity car)
        {
            if (car != null) { car.Operator = CurrentUser.UserID; }
            CarService.ModifyCar(car);
            Response.Redirect("/Car/");
        }

        public void Remove(string cid)
        {
            CarService.RemoveCar(cid.ToLong(0));
            Response.Redirect("/Car/");
        }

        public JsonResult GetBrand(string bid)
        {
            long brandid = bid.ToLong(0);
            BrandEntity result = BrandService.GetBrandById(brandid) ?? new BrandEntity();

            return Json(result);
        }

        public ActionResult Developing()
        {
            return View();
        }

    }
}
