using Common;
using Entity.ViewModel;
using GuoChe.ApiConvert;
using Infrastructure.Cache;
using Service.BaseBiz;
using Service.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuoChe.Controllers
{
    public class ApiServiceController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.UKey = Guid.NewGuid().ToString();

            return View();
        }


        //
        // GET: /Api/

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public JsonResult Login(String UserName, String Password)
        {
            UserEntity user = UserService.GetLoginUser(UserName, EncryptHelper.MD5Encrypt(Password), false);
            //if (user != null && user.UserID > 0)
            //{
            //    HttpCookie cookie = WebHelper.CreateSingleValueCookie("ukey", Guid.NewGuid().ToString(), 0);//把缓存key放入cookie
            //    Response.Cookies.Add(cookie);
            //    HttpCookie cookieu = WebHelper.CreateSingleValueCookie("ckey", EncryptHelper.Encryption(user.UserID.ToString()), 0);//把缓存key放入cookie
            //    Response.Cookies.Add(cookieu);
            //}
            return Json(ApiProcess.convertUser(user));
        }

        /// <summary>
        /// 库存列表接口
        /// </summary>
        /// <param name="goodsid"></param>
        /// <returns></returns>
        public JsonResult InventoryList(int GoodsID)
        {
            List<InventoryEntity> list = InventoryService.GetInventoryByRule(GoodsID, 0, "");
            return Json(ApiProcess.convertInventoryList(list));
        }

        /// <summary>
        /// 库存明细接口
        /// </summary>
        /// <param name="inventoryid"></param>
        /// <returns></returns>
        public JsonResult InventoryDetailInfo(int InventoryID)
        {
            InventoryEntity entity = InventoryService.GetInventoryEntityById(InventoryID);
            return Json(ApiProcess.convertInventoryDetailInfo(entity));
        }


        /// <summary>
        /// 商品明细接口
        /// </summary>
        /// <param name="goodsid"></param>
        /// <returns></returns>
        public JsonResult GoodsInfo(int GoodsID)
        {
            GoodsEntity goodsentity = GoodsService.GetGoodsEntityById(GoodsID);
            return Json(ApiProcess.convertGoodsInfo(goodsentity));
        }

        /// <summary>
        /// 库存 查询接口 模糊查询
        /// </summary>
        /// <param name="Keywords"></param>
        /// <returns></returns>
        public JsonResult InventoryGoodsSearch(string Keywords)
        {
            List<InventoryEntity> list = InventoryService.GetInventoryByRule(-1, -1, "", -1, false, Keywords);
            return Json(ApiProcess.convertInventoryList(list));
        }
    }
}
