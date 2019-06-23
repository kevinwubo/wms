using Common;
using DataRepository.DataAccess.Order;
using DataRepository.DataAccess.Reservations;
using Entity.ViewModel;
using Infrastructure.Cache;
using Service;
using Service.ApiBiz;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GuoChe.Controllers
{
    public class RevController:BaseController
    {
        public int PAGESIZE = 20;

        public ActionResult Index(string customerid,string paytype,int status=-1,int p=1)
        {

            ReservationsSearchEntity search=new ReservationsSearchEntity();
            search.CustomerID=customerid.ToLong(0);
            search.PayType=paytype;
            search.Status=status;

            int count = ReservationsService.GetReservationsCount(search);

            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "/Rev";

            List<CustomerEntity> customers =null;
            List<BaseDataEntity> paytypes=BaseDataService.GetBaseDataAll().Where(t=>t.PCode=="P00").ToList();
            ViewBag.Search = search;
            List<ReservationsEntity> mList = ReservationsService.GetReservationsByRule(search, pager);
            ViewBag.Reservations = mList;
            ViewBag.Pager = pager;
            ViewBag.Customers = customers;
            ViewBag.PayTypes = paytypes;

            return View();
        }


        public JsonResult Edit(long rid,int status)
        {       
            var result = new
            {
                 result=ReservationsService.EditReservationsStatus(rid,status)
            }; 

            return Json(result);
        }




    }
}
