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
    public class GroupController : BaseController
    {
        public ActionResult Index(string name,int status=-1)
        {
            List<GroupEntity> gList = null;
            if (!string.IsNullOrEmpty(name) || status>-1)
            {
                gList = GroupService.GetGroupByRule(name, status);
            }
            else
            {
                gList = GroupService.GetGroupAll();
            }
            
            ViewBag.Groups = gList;
            ViewBag.Name = name ?? "";
            ViewBag.Status = status;

            return View();
        }

        [HttpGet]
        public ActionResult Edit(string gid)
        {
            List<MenuEntity> allMenus = MenuService.GetMenuAll().Where(t => t.Status == 1).ToList();
            
            if(string.IsNullOrEmpty(gid))
            {
                ViewBag.MenuHtml = MakeMenus(allMenus, null);
                ViewBag.Groups = new GroupEntity();
            }
            else
            {
                GroupEntity group = GroupService.GetGroupById(gid.ToLong(0));
                ViewBag.MenuHtml = MakeMenus(allMenus, group);
                ViewBag.Groups = group;
            }

            return View();
        }

        [HttpPost]
        public void Modify()
        {
            int groupid = (Request["GroupID"] ?? "").ToInt(0);
            string groupName = Request["GroupName"] ?? "";
            int status = (Request["Status"] ?? "").ToInt(0);
            string menuids = Request["MenuIDs"] ?? "";
            GroupEntity group = new GroupEntity();
            group.GroupID = groupid;
            group.GroupName = groupName;
            group.Status = status;
            group.Menus = MenuService.GetMenuByKeys(menuids);
            GroupService.ModifyGroup(group);

            Response.Redirect("/Group/");
        }

        public void Remove(string gid)
        {
            GroupService.Remove(gid.ToLong(0));

            Response.Redirect("/Group/");
        }

        public string MakeMenus(List<MenuEntity> allMenus,GroupEntity group)
        {
            StringBuilder builder = new StringBuilder();
            if (!allMenus.IsEmpty())
            {
                for (int i = 0; i < allMenus.Count; i++)
                {
                    int c = i % 4;
                    if (c == 0)
                    {
                        builder.Append("<div class=\"row-fluid\" style=\"margin-bottom:15px;\">");
                    }
                    
                    builder.Append("<span class=\"btn btn-box span3\" data-bubble=\"2\" style=\"margin-bottom:5px\">");
                    string checkedFlag="";
                    if (group != null && !group.Menus.IsEmpty())
                    {
                         checkedFlag=group.Menus.Exists(t => t.MenuID == allMenus[i].MenuID)?"checked":"";
                    }
                    builder.AppendFormat("<input type=\"checkbox\" style=\"margin-bottom:5px\" name=\"MenuIDs\" value=\"{0}\" {1}/>{2}", allMenus[i].MenuID, checkedFlag, allMenus[i].MenuName);
                    builder.Append("</span>");

                    if (i == allMenus.Count-1 || (i+1)%4==0)
                    {
                        builder.Append("</div>");
                    }
                }
            }

            return builder.ToString();
        }

    }
}
