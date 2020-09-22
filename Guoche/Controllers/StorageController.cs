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
    public class StorageController : BaseController
    {
        //
        // GET: /Storage/
        public int PAGESIZE = 20;
        public ActionResult Index(string name = "",int status = -1, int p = 1)
        {
            
            List<StorageEntity> mList = null;

            int count = StorageService.GetStorageCount("", "", status);

            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "/Storage";


            //ViewBag.StorageModel = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "StorageCode" && t.Status == 1).ToList();
            if (!string.IsNullOrEmpty(name) || status > -1)
            {
                mList = StorageService.GetStorageInfoByRule(name, "", status, pager);
            }
            else
            {
                mList = StorageService.GetStorageInfoPager(pager);
            }
            ViewBag.Name = name ?? "";
            ViewBag.Status = status;
            //ViewBag.ModelCode = mcode;
            ViewBag.Storage = mList;
            ViewBag.Pager = pager;
            return View();
        }


        public ActionResult Edit(string cid)
        {
            ViewBag.Province = BaseDataService.GetAllProvince();
            if (!string.IsNullOrEmpty(cid))
            {
                ViewBag.Storage = StorageService.GetStorageEntityById(cid.ToLong(0));
            }
            else
            {
                ViewBag.Storage = new StorageEntity();
            }

            return View();
        }

        public void Modify(StorageEntity Storage)
        {
            if (Storage != null)
            {
                Storage.OperatorID = CurrentUser.UserID;
            }
            StorageService.ModifyStorage(Storage);
            Response.Redirect("/Storage/");
        }

        public void Remove(string cid)
        {
            StorageService.RemoveStorage(cid);
            Response.Redirect("/Storage/");
        }

    }
}
