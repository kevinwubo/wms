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
using Service;


namespace GuoChe.Controllers
{
    public class AdviseController : BaseController
    {
        private int PAGESIZE = 20;
        public ActionResult Index(string title,string customerName,int dealStatus=-1,int p=1)
        {
            List<AdviseEntity> gList = null;
            int count = AdviseService.GetAdviseCount(customerName, title, dealStatus);

            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "/Advise";

            if (!string.IsNullOrEmpty(title) || !string.IsNullOrEmpty(customerName) || dealStatus > -1)
            {
                gList = AdviseService.GetAdviseInfoByRule(customerName, title, dealStatus, pager);
            }
            else
            {
                gList = AdviseService.GetAdviseInfoPager(pager);
            }
            
            ViewBag.Advises = gList;
            ViewBag.NTitle = title;
            ViewBag.CustomerName = customerName ?? "";
            ViewBag.DealStatus = dealStatus;
            ViewBag.Pager = pager;

            return View();
        }

        [HttpGet]
        public ActionResult Edit(string aid)
        {

            if (string.IsNullOrEmpty(aid))
            {
                ViewBag.Advise = new AdviseEntity();
            }
            else
            {
                AdviseEntity advise = AdviseService.GetAdviseInfoByID(aid.ToLong(0));
                ViewBag.Advise = advise;
            }

            return View();
        }

        [HttpPost]
        public void Modify(AdviseEntity adviseEntity)
        {
            if (adviseEntity != null)
            {
                adviseEntity.Operator = CurrentUser.UserID;
                AdviseService.ModifyAdvise(adviseEntity);
            }
        }

        [HttpPost]
        public ContentResult ModifyDeal(long aid,int status,string summary)
        {
            AdviseEntity adviseEntity = new AdviseEntity();
            adviseEntity.AdviseID = aid;
            adviseEntity.DealStatus = status;
            adviseEntity.DealSummary = summary;
            adviseEntity.Operator = CurrentUser.UserID;
            int f=AdviseService.ModifyDealInfo(adviseEntity);
            return Content(f > 0 ? "T" : "F"); 
        }

    }
}
