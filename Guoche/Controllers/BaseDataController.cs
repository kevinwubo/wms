using Entity.ViewModel;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using System.Text;
using Infrastructure.Helper;


namespace GuoChe.Controllers
{
    public class BaseDataController : BaseController
    {
        public ActionResult Index(string name)
        {
            List<BaseDataEntity> list = null;
            if (!string.IsNullOrEmpty(name))
            {
                list = BaseDataService.GetBaseDataByRule(name);
            }
            else
            {
                list = BaseDataService.GetBaseDataAll();
            }

            ViewBag.BaseData = list;
            ViewBag.Name = name ?? "";

            return View();
        }

        public ActionResult Main()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            List<BaseDataEntity> allData = BaseDataService.GetBaseDataAll().Where(t => t.Status == 1).ToList();
            
            if(string.IsNullOrEmpty(id))
            {
                ViewBag.BaseData = new BaseDataEntity();
            }
            else
            {
                ViewBag.BaseData = BaseDataService.GetBaseDataById(id.ToInt(0));
            }
            ViewBag.BaseDataAll = allData;
            

            return View();
        }

        [HttpPost]
        public void Modify(BaseDataEntity baseEntity)
        {
            BaseDataService.ModifyBaseData(baseEntity);

            Response.Redirect("/BaseData/");
        }

        public void Remove(string id)
        {
            BaseDataService.Remove(id.ToInt(0));

            Response.Redirect("/BaseData/");
        }

        public ActionResult Help()
        {
            return View();
        }

    }
}
