using Entity.ViewModel;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

namespace GuoChe.Controllers
{
    public class MenuController : BaseController
    {
        public ActionResult Index(string name,int status=-1)
        {
            List<MenuEntity> mList = null;
            if (!string.IsNullOrEmpty(name) || status>-1)
            {
                mList = MenuService.GetMenuByRule(name, status);
            }
            else
            {
                mList = MenuService.GetMenuAll();
            }
            
            ViewBag.Menus = mList;
            ViewBag.Name = name ?? "";
            ViewBag.Status = status;

            return View();
        }

        [HttpGet]
        public ActionResult Edit(string mid)
        {
            if (!string.IsNullOrEmpty(mid))
            {
                ViewBag.Menu = MenuService.GetMenuById(mid.ToLong(0));
            }
            else
            {
                ViewBag.Menu = new MenuEntity();
            }
            
            ViewBag.GroupCodes = BaseDataService.GetBaseDataAll().Where(t => t.Status == 1 && t.PCode=="S00").ToList();

            return View();
        }

        [HttpPost]
        public void Modify(MenuEntity menu)
        {
            MenuService.ModifyMenu(menu);

            Response.Redirect("/Menu/");
        }

        public void Remove(string mid)
        {
            MenuService.Remove(mid.ToLong(0));

            Response.Redirect("/Menu/");
        }

    }
}
