using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuoChe.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Remove(string cid)
        {
            int count= ContactService.Remove(long.Parse(cid));
            return new JsonResult
            {
                Data = count > 0 ? true : false
            };
        }
    }
}
