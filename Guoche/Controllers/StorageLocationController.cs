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
    public class StorageLocationController : BaseController
    {
        //
        // GET: /StorageLocation/
        public int PAGESIZE = 20;
        public ActionResult Index(int storageid = 0, string storageareano = "", string storagesubareano="", int status = -1, int p = 1)
        {
            List<StorageLocationEntity> mList = null;
            int storageidn = storageid;
            int count = StorageLocationService.GetStorageLocationCount(storageidn, storageareano, storagesubareano, status);

            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "/StorageLocation";
            //仓库
            ViewBag.Storage = StorageService.GetStorageByRule("", 1);//只显示使用中的数据

            if (storageidn > 0 || status > -1 || !string.IsNullOrEmpty(storageareano))
            {
                mList = StorageLocationService.GetStorageLocationInfoByRule(storageidn, storageareano, storagesubareano, status, pager);
            }
            else
            {
                mList = StorageLocationService.GetStorageLocationInfoPager(pager);
            }
            ViewBag.AreasNo = storageareano;
            ViewBag.SubAreasNo = storagesubareano;
            ViewBag.StorageID = storageid;
            ViewBag.Status = status;
            ViewBag.storageAreaNo = storageareano;
            ViewBag.StorageLocation = mList;
            ViewBag.Pager = pager;
            return View();
        }


        public ActionResult Edit(string cid)
        {
            //库位区域
            ViewBag.StorageAreas = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "StorageAreaNo00" && t.Status == 1).ToList();
            //仓库
            ViewBag.Storage = StorageService.GetStorageByRule("", 1);//只显示使用中的数据
            if (!string.IsNullOrEmpty(cid))
            {
                ViewBag.StorageLocation = StorageLocationService.GetStorageLocationEntityById(cid.ToLong(0));
            }
            else
            {
                ViewBag.StorageLocation = new StorageLocationEntity();
            }

            return View();
        }

        public void Modify(StorageLocationEntity StorageLocation)
        {
            if (StorageLocation != null)
            {
                StorageLocation.OperatorID = CurrentUser.UserID;
            }
            StorageLocationService.ModifyStorageLocation(StorageLocation);
            Response.Redirect("/StorageLocation/");
        }

        public void Remove(string cid)
        {
            StorageLocationService.Remove(long.Parse(cid));
            Response.Redirect("/StorageLocation/");
        }

    }
}
