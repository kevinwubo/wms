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
    public class LineController : BaseController
    {
        //
        // GET: /Line/
        public int PAGESIZE = 20;

        public ActionResult Index(int LineID = -1, int p = -1)
        {
            List<LineEntity> mList = null;
            int count = LineService.GetLineCount(LineID);
            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "/Line";
            if (LineID > -1)
            {
                mList = LineService.GetLineInfoByRule(LineID, pager);
            }
            else
            {
                mList = LineService.GetLineInfoPager(pager);
            }
            ViewBag.LineID = LineID;
            ViewBag.Line = mList;
            ViewBag.Pager = pager;
            return View();
        }

        public void Modify(string lineid, string name, int id = 0)
        {
            LineEntity entity = new LineEntity();
            entity.ID = id;
            entity.LineID = lineid.ToInt(0);
            entity.ReceiverName = name;
            entity.Address = "";
            if (entity != null)
            {
                entity.OperatorID = CurrentUser.UserID.ToString().ToInt(0);
            }
            LineService.ModifyLine(entity);
        }

        public void Delete(int id)
        {
            LineService.Delete(id);
            Response.Redirect("/Line/Index");
        }
    }
}
