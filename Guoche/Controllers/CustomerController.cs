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
    public class CustomerController : BaseController
    {
        //
        // GET: /Customer/
        public int PAGESIZE = 20;
        public ActionResult Index(string name, int status = -1, int p = 1)
        {
            List<CustomerEntity> mList = null;

            int count = CustomerService.GetCustomerCount("", "", status);

            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "/Customer";


            if (!string.IsNullOrEmpty(name) || status > -1)
            {
                mList = CustomerService.GetCustomerInfoByRule(name, "", status, pager);
            }
            else
            {
                mList = CustomerService.GetCustomerInfoPager(pager);
            }
            ViewBag.Name = name ?? "";
            ViewBag.Status = status;
            ViewBag.Customer = mList;
            ViewBag.Pager = pager;
            return View();
        }


        public ActionResult Edit(string cid)
        {            
            if (!string.IsNullOrEmpty(cid))
            {
                ViewBag.Customer = CustomerService.GetCustomerEntityById(cid.ToLong(0));
            }
            else
            {
                ViewBag.Customer = new CustomerEntity();
            }

            return View();
        }

        public void Modify(CustomerEntity Customer)
        {
            if (Customer != null)
            {
                Customer.OperatorID = CurrentUser.UserID;
            }
            CustomerService.ModifyCustomer(Customer);
            Response.Redirect("/Customer/");
        }

        public void Remove(string cid)
        {
            CustomerService.Remove(long.Parse(cid));
            Response.Redirect("/Customer/");
        }

    }
}
