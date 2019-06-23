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
using System.Configuration;
using Service;

namespace GuoChe.Controllers
{
    public class BrandController : BaseController
    {
        public ActionResult Index(string name,int status=-1)
        {
            List<BrandEntity> mList = null;

            if (!string.IsNullOrEmpty(name) || status > -1)
            {
                mList = BrandService.GetBrandByRule(name, status);
            }
            else
            {
                mList = BrandService.GetBrandAll();
            }
            ViewBag.Brands = mList;
            ViewBag.Name = name ?? "";
            ViewBag.Status = status;
            return View();
        }

        public ActionResult Edit(string bid)
        {
            ViewBag.MaxPicCount = 1;
            if (!string.IsNullOrEmpty(bid))
            {
                ViewBag.Brand = BrandService.GetBrandById(bid.ToLong(0));
            }
            else
            {
                ViewBag.Brand = new BrandEntity();
            }
            
            return View();
        }

        public void Modify(BrandEntity brand)
        {
            BrandService.ModifyBrand(brand);
            Response.Redirect("/Brand/");
        }

        public void Remove(string bid)
        {
            BrandService.Remove(bid.ToLong(0));

            Response.Redirect("/Brand/");
        } 




    }
}
