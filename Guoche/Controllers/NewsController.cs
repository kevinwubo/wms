using Entity.ViewModel;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

namespace GuoChe.Controllers
{
    public class NewsController : BaseController
    {
        //
        // GET: /News/

        public ActionResult Index(string title,string status)
        {
            List<NewsEntity> nList = null;
            if (!string.IsNullOrEmpty(title) || !string.IsNullOrEmpty(status))
            {
                nList = NewsService.GetNews(title, status.ToInt(0));
            }
            else
            {
                nList = NewsService.GetNews("", -1);
            }
            ViewBag.Name = title ?? "";
            ViewBag.Status = status;
            ViewBag.News = nList;
            return View();
        }

        [HttpGet]
        public ActionResult Edit(string ID)
        {
            if (!string.IsNullOrEmpty(ID))
            {
                ViewBag.News = NewsService.GetNewsByID(ID.ToInt(0));
            }
            else
            {
                ViewBag.News = new NewsEntity();
            }
            return View();
        }

        

        [HttpPost]
        [ValidateInput(false)]
        public void Modify()
        {
            int id = (Request["ID"] ?? "").ToInt(0);
            string Title = Request["Title"] ?? "";
            int status = (Request["Status"] ?? "").ToInt(0);
            int ChannelID = (Request["ChannelID"] ?? "").ToInt(0);
            string content = Request["Content"] ?? "";
            content = Request["Content"] ?? "";
            string zhaiyao = Request["zhaiyao"] ?? "";
            string AttachmentIDs = Request["AttachmentIDs"] ?? "";
            NewsEntity entity = new NewsEntity();
            entity.ID = id;
            entity.Title = Title;
            entity.ChannelID = ChannelID;
            entity.zhaiyao = zhaiyao;
            entity.AttachmentIDs = AttachmentIDs;
            entity.Operator = CurrentUser == null ? 0 : CurrentUser.UserID;
            entity.Content = content;
            entity.Status = status;
            NewsService.ModifiyNews(entity);
            Response.Redirect("/News/");
        }

        public void Remove(string rid)
        {
            NewsService.RemoveNews(rid.ToInt(0));

            Response.Redirect("/Role/");
        }


    }
}
