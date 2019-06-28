using Entity.ViewModel;
using Infrastructure.Cache;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Helper;
using Common;

namespace GuoChe.Controllers
{
    public class BaseController : Controller
    {
        public CacheRuntime Cache { 
             get { 
                 CacheRuntime cache=new CacheRuntime(); 
                 cache.ExpiredTimespan=30;//设置30分钟过期
                 return cache;
             } 
        }

        public string UKey {
            get {
                string ukey = "";
                if (!string.IsNullOrEmpty(Request["ukey"] ?? ""))
                {
                    ukey = Request["ukey"];//如果表单内有值就从表单内获取
                }
                else
                {
                    ukey = Request.Cookies["ukey"]!=null?Request.Cookies["ukey"].Value:"";//表单内没有就从cookie中获取
                }

                return ukey;
            }
        }

        protected UserEntity CurrentUser {
            get {
                UserEntity user = Cache.Get<UserEntity>(UKey);

                if (user == null)
                {
                    string ckey=Request.Cookies["ckey"]!=null?Request.Cookies["ckey"].Value:"";
                    if (string.IsNullOrEmpty(ckey))
                    {
                        Response.Redirect("/", true);
                    }
                    else
                    {
                        ckey = EncryptHelper.Decrypt(ckey);
                        UserEntity cuser = UserService.GetUserById(ckey.ToLong(0));
                        if (cuser == null)
                        {
                            Response.Redirect("/", true);
                        }
                        else
                        {
                            user = cuser;
                            Cache.Add<UserEntity>(UKey, cuser);//用户信息放入缓存
                        }
                    }
                    
                }

                return user;
            }
        }

        /// <summary>
        /// 门店模糊查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public JsonResult SearchByName(string name)
        {
            List<ReceiverEntity> list = new List<ReceiverEntity>();
            List<ReceiverEntity> reveiverList = ReceiverService.GetReceiverAll();
            if (reveiverList != null && reveiverList.Count > 0)
            {
                //foreach (ReceiverEntity entity in reveiverList)
                {
                    if (!string.IsNullOrEmpty(name))
                    {
                        var v = from d in reveiverList where d.ReceiverName.Contains(name) select d;

                        if (v != null)
                        {
                            foreach (var k in v)
                            {
                                ReceiverEntity model = new ReceiverEntity();
                                model.ReceiverID = k.ReceiverID;
                                model.ReceiverName = k.ReceiverName;
                                list.Add(model);

                            }
                        }
                    }
                    else
                    {
                        foreach (ReceiverEntity entity in reveiverList)
                        {
                            list.Add(entity);
                        }
                    }
                }
            }
            return Json(list);
        }

        public JsonResult GetCity(int pid)
        {
            List<City> listCity = BaseDataService.GetAllCity();
            if (!listCity.IsEmpty())
            {
                listCity = listCity.Where(t => t.ProvinceID == pid).ToList();
            }

            return Json(listCity);
        }

        /// <summary>
        /// 根据仓库ID筛选对应的库位区域
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public JsonResult GetStorageAreaNo(int sid)
        {
            List<StorageLocationEntity> list = StorageLocationService.GetAreaNoByStorageID(sid, 1);
            return Json(list);
        }


        /// <summary>
        /// 根据仓库区域筛选对应的库位子区域
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public JsonResult GetStorageSubAreaNo(string areaNo)
        {
            List<StorageLocationEntity> list = StorageLocationService.GetSubAreaNoByStorageAreaNo(areaNo, 1);
            return Json(list);
        }


        /// <summary>
        /// 根据收货人ID 查询对应的联系人信息
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        public JsonResult GetReceiverByGoodsID(int rid)
        {
            ContactModel contact = new ContactModel();
            ReceiverEntity entity = ReceiverService.GetReceiverEntityById(long.Parse(rid.ToString()));
            if (entity != null)
            {                
                List<ContactEntity> list=entity.listContact;
                if (list != null && list.Count > 0)
                {
                    contact.ContactName = list[0].ContactName;
                    contact.Mobile = string.IsNullOrEmpty(list[0].Mobile) ? list[0].Telephone : list[0].Mobile;
                }
                contact.Address = entity.Address;
            }
            return Json(contact);
        }


        /// <summary>
        /// 根据客户ID 查询对应的门店信息
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        public JsonResult GetReceiverByCustomerID(int customerID)
        {
            List<ReceiverEntity> list = ReceiverService.GetReceiverByCustomerID(customerID);
            return Json(list);
        }

        public JsonResult GetReceiver(string type)
        {
            List<ReceiverEntity> listReceiver = new List<ReceiverEntity>();
            if (!string.IsNullOrEmpty(type))
            {
                listReceiver = ReceiverService.GetReceiverByRule("", "", type, 1);//只显示使用中的数据
            }

            return Json(listReceiver);
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {

            
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.UKey = UKey;
            string action = RouteData.Route.GetRouteData(this.HttpContext).Values["controller"].ToString();
            string code = Request["Code"];
            Response.Cookies["userName"].Value = action;
            Response.Cookies["userName"].Expires = DateTime.Now.AddDays(1);
            if (CurrentUser != null)
            {
                List<MenuEntity> listNew = new List<MenuEntity>();
                List<MenuEntity> list = CurrentUser.Menus;
                if (list != null && list.Count > 0)
                {
                    foreach (MenuEntity entity in list)
                    {
                        entity.IsShow = false;
                        if (string.IsNullOrEmpty(code))
                        {                            
                            if (entity.URL.Contains(action))
                            {
                                entity.IsShow = true;
                            }
                        }
                        else
                        {                            
                            if (entity.GroupCode.Equals(code))
                            {
                                entity.IsShow = true;
                            }
                        }
                        listNew.Add(entity);
                    }
                }
                ViewBag.Username = CurrentUser != null ? CurrentUser.UserName : "";
                ViewBag.CommonMenus = listNew;
                ViewBag.CurrentSelectMenus = action;
            }
            //获取菜单信息
            base.OnActionExecuting(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
 
        }


    }
}
