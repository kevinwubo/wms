using Entity.ViewModel;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Infrastructure.Helper;
using System.Text;
using System.Web.Security;

namespace GuoChe.Controllers
{
    public class UserController : BaseController
    {
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Index(string name, int status = -1)
        {
            List<UserEntity> uList = null;
            if (!string.IsNullOrEmpty(name) || status > -1)
            {
                uList = UserService.GetUserByRule(name, status);
            }
            else
            {
                uList = UserService.GetUserAll();
            }

            ViewBag.Users = uList;
            ViewBag.Name = name ?? "";
            ViewBag.Status = status;

            return View();
        }

        [HttpGet]
        public ActionResult Edit(string uid)
        {
            List<GroupEntity> allGroups = GroupService.GetGroupAll().Where(t => t.Status == 1).ToList();
            List<RoleEntity> allRoles = RoleService.GetRoleAll().Where(t => t.Status == 1).ToList();
            if (string.IsNullOrEmpty(uid))
            {
                ViewBag.RoleHtml = MakeRoles(allRoles, null);
                ViewBag.GroupHtml = MakeGroups(allGroups, null);
                ViewBag.User = new UserEntity();
            }
            else
            {
                UserEntity user = UserService.GetUserById(uid.ToLong(0));
                ViewBag.GroupHtml = MakeGroups(allGroups, user);
                ViewBag.RoleHtml = MakeRoles(allRoles, user);
                ViewBag.User = user;
            }

            return View();
        }

        [HttpPost]
        public void Modify()
        {
            long uid = (Request["UserID"] ?? "").ToLong(0);
            string userName = Request["UserName"] ?? "";
            string nickName = Request["NickName"] ?? "";
            int status = (Request["Status"] ?? "").ToInt(0);
            string roleids = Request["RoleIDs"] ?? "";
            string groupids = Request["GroupIDs"] ?? "";
            UserEntity user = new UserEntity();
            user.UserID = uid;
            user.UserName = userName;
            user.NickName = nickName;
            user.Roles = RoleService.GetRoleByKeys(roleids);
            user.Groups = GroupService.GetGroupByKeys(groupids);
            user.Status = status;
            UserService.ModifyUser(user);

            Response.Redirect("/User/");
        }

        public void Remove(string uid)
        {
            UserService.Remove(uid.ToLong(0));

            Response.Redirect("/User/");
        }

        [HttpGet]
        public ActionResult ModifyPassword()
        {
            ViewBag.CurrentUser = CurrentUser;
            return View();
        }

        [HttpPost]
        public ContentResult CheckPwd(string oldPwd)
        {
            string result = "F";

            UserEntity user = UserService.GetLoginUser(CurrentUser.UserName, EncryptHelper.MD5Encrypt(oldPwd));

            if (user != null && user.UserID == CurrentUser.UserID)
            {
                result = "T";
            }

            return Content(result);
        }

        [HttpPost]
        public ContentResult ModifyPassword(string npwd)
        {
            string result = "F";
            int r = UserService.ModifyPassword(CurrentUser.UserID, EncryptHelper.MD5Encrypt(npwd));
            if (r > 0)
            {
                result = "T";
            }
            return Content(result);
        }



        public string MakeGroups(List<GroupEntity> allGroup, UserEntity user)
        {
            StringBuilder builder = new StringBuilder();
            if (!allGroup.IsEmpty())
            {
                for (int i = 0; i < allGroup.Count; i++)
                {
                    int c = i % 4;
                    if (c == 0)
                    {
                        builder.Append("<div class=\"row-fluid\" style=\"margin-bottom:15px;\">");
                    }

                    builder.Append("<span class=\"btn btn-box span3\" data-bubble=\"2\" style=\"margin-bottom:5px\">");
                    string checkedFlag = "";
                    if (user != null && !user.Groups.IsEmpty())
                    {
                        checkedFlag = user.Groups.Exists(t => t.GroupID == allGroup[i].GroupID) ? "checked" : "";
                    }
                    builder.AppendFormat("<input type=\"checkbox\" style=\"margin-bottom:5px\" name=\"GroupIDs\" value=\"{0}\" {1}/>{2}", allGroup[i].GroupID, checkedFlag, allGroup[i].GroupName);
                    builder.Append("</span>");

                    if (i == allGroup.Count - 1 || (i + 1) % 4 == 0)
                    {
                        builder.Append("</div>");
                    }
                }
            }

            return builder.ToString();
        }

        public string MakeRoles(List<RoleEntity> allRole, UserEntity user)
        {
            StringBuilder builder = new StringBuilder();
            if (!allRole.IsEmpty())
            {
                for (int i = 0; i < allRole.Count; i++)
                {
                    int c = i % 4;
                    if (c == 0)
                    {
                        builder.Append("<div class=\"row-fluid\" style=\"margin-bottom:15px;\" for=\"RoleIDs\">");
                    }

                    builder.Append("<span class=\"btn btn-box span3\" data-bubble=\"2\" style=\"margin-bottom:5px\">");
                    string checkedFlag = "";
                    if (user != null && !user.Roles.IsEmpty())
                    {
                        checkedFlag = user.Roles.Exists(t => t.RoleID == allRole[i].RoleID) ? "checked" : "";
                    }
                    builder.AppendFormat("<input type=\"checkbox\" style=\"margin-bottom:5px\" name=\"RoleIDs\" value=\"{0}\" {1}/>{2}", allRole[i].RoleID, checkedFlag, allRole[i].RoleName);
                    builder.Append("</span>");

                    if (i == allRole.Count - 1 || (i + 1) % 4 == 0)
                    {
                        builder.Append("</div>");
                    }
                }
            }

            return builder.ToString();
        }

        public ContentResult CheckIsExist(string name)
        {
            string result = "T";
            if (UserService.IsExists(name))
            {
                result = "F";
            }

            return Content(result);
        }
        
    }
}
