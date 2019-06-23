using Entity.ViewModel;
using Service.BaseBiz;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common;
using System.Text;
using Infrastructure.Helper;

namespace GuoChe.Controllers
{
    public class RoleController : BaseController
    {
        public ActionResult Index(string name, int status = -1)
        {
            List<RoleEntity> rList = null;
            if (!string.IsNullOrEmpty(name) || status > -1)
            {
                rList = RoleService.GetRoleByRule(name, status);
            }
            else
            {
                rList = RoleService.GetRoleAll();
            }

            ViewBag.Roles = rList;
            ViewBag.Name = name ?? "";
            ViewBag.Status = status;

            return View();
        }

        [HttpGet]
        public ActionResult Edit(string rid)
        {
            List<MenuEntity> allMenus = MenuService.GetMenuAll().Where(t => t.Status == 1).ToList();

            if (string.IsNullOrEmpty(rid))
            {
                ViewBag.MenuHtml = MakeMenus(allMenus, null);
                ViewBag.Role = new RoleEntity();
            }
            else
            {
                RoleEntity Role = RoleService.GetRoleById(rid.ToInt(0));
                ViewBag.MenuHtml = MakeMenus(allMenus, Role);
                ViewBag.Role = Role;
            }

            return View();
        }

        [HttpPost]
        public void Modify()
        {
            int roleid = (Request["RoleID"] ?? "").ToInt(0);
            string roleName = Request["RoleName"] ?? "";
            int status = (Request["Status"] ?? "").ToInt(0);
            string menuids = Request["MenuIDs"] ?? "";
            RoleEntity role = new RoleEntity();
            role.RoleID = roleid;
            role.RoleName = roleName;
            role.Status = status;
            role.Menus = MenuService.GetMenuByKeys(menuids);
            RoleService.ModifyRole(role);

            Response.Redirect("/Role/");
        }

        public void Remove(string rid)
        {
            RoleService.Remove(rid.ToInt(0));

            Response.Redirect("/Role/");
        }

        public string MakeMenus(List<MenuEntity> allMenus, RoleEntity role)
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
                    string checkedFlag = "";
                    if (role != null && !role.Menus.IsEmpty())
                    {
                        checkedFlag = role.Menus.Exists(t => t.MenuID == allMenus[i].MenuID) ? "checked" : "";
                    }
                    builder.AppendFormat("<input type=\"checkbox\" style=\"margin-bottom:5px\" name=\"MenuIDs\" value=\"{0}\" {1}/>{2}", allMenus[i].MenuID, checkedFlag, allMenus[i].MenuName);
                    builder.Append("</span>");

                    if (i == allMenus.Count - 1 || (i + 1) % 4 == 0)
                    {
                        builder.Append("</div>");
                    }
                }
            }

            return builder.ToString();
        }

    }
}
