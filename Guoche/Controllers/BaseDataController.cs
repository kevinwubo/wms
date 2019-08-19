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
using System.Data;
using System.IO;
using Service.Inventory;


namespace GuoChe.Controllers
{
    public class BaseDataController : BaseController
    {

        public ActionResult InsertData()
        {
            return View();
        }

        public ActionResult Insert()
        {
            BaseDataService.GetAllTest();
            return View();
        }
        public ActionResult Index(string name)
        {
            List<BaseDataEntity> list = null;
            if (!string.IsNullOrEmpty(name))
            {
                list = BaseDataService.GetBaseDataByRule(name);
            }
            else
            {
                list = BaseDataService.GetBaseDataAll();
            }

            ViewBag.BaseData = list;
            ViewBag.Name = name ?? "";

            return View();
        }

        public ActionResult Main()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            List<BaseDataEntity> allData = BaseDataService.GetBaseDataAll().Where(t => t.Status == 1).ToList();
            
            if(string.IsNullOrEmpty(id))
            {
                ViewBag.BaseData = new BaseDataEntity();
            }
            else
            {
                ViewBag.BaseData = BaseDataService.GetBaseDataById(id.ToInt(0));
            }
            ViewBag.BaseDataAll = allData;
            

            return View();
        }

        [HttpPost]
        public void Modify(BaseDataEntity baseEntity)
        {
            BaseDataService.ModifyBaseData(baseEntity);

            Response.Redirect("/BaseData/");
        }

        public void Remove(string id)
        {
            BaseDataService.Remove(id.ToInt(0));

            Response.Redirect("/BaseData/");
        }

        public ActionResult Help()
        {
            return View();
        }

        public ActionResult Import()
        {
            ViewBag.hid_Goods_Token = Guid.NewGuid();
            ViewBag.hid_Inventory_Token = Guid.NewGuid();
            return View();
        }

        #region 商品基础数据导入
        /// <summary>
        /// 订单导入
        /// </summary>
        /// <returns></returns>
        public JsonResult GoodsImportData()
        {
            List<GoodsEntity> list = new List<GoodsEntity>();
            DataSet ds = new DataSet();
            if (Request.Files.Count == 0)
            {
                throw new Exception("请选择导入文件！");
            }

            String token = Request["token"];
            Cache.Remove(token);
            // 保存文件到UploadFiles文件夹
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                var fileName = file.FileName;
                var filePath = Server.MapPath(string.Format("~/{0}", "UploadFiles"));
                string path = Path.Combine(filePath, fileName);
                file.SaveAs(path);
                ds = ExcelHelper.ImportBaseExceltoDt(path);
                list = BaseDataService.GetImportList(ds);
                //存入缓存
                Cache.Add(token, list);
            }
            return Json(list);
        }

        /// <summary>
        /// 商品信息导入
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public JsonResult GenerateGoods(string token)
        {
            long operatorID = 0;
            if (CurrentUser != null)
            {
                operatorID = CurrentUser.UserID;
            }
            int result = 0;
            List<GoodsEntity> list = Cache.Get<List<GoodsEntity>>(token);
            if (list != null && list.Count > 0)
            {
                result = BaseDataService.InsertImport(list, operatorID);
            }
            return Json(result);
        }
        #endregion

        

        #region 库存数据导入
        public JsonResult InventoryDataImport()
        {
            List<ImportInventoryEntity> list = new List<ImportInventoryEntity>();
            try
            {
                LogHelper.WriteTextLog("InventoryDataImport", "库存数据导入");
                DataSet ds = new DataSet();
                if (Request.Files.Count == 0)
                {
                    throw new Exception("请选择导入文件！");
                }

                String token = Request["token"];
                Cache.Remove(token);
                // 保存文件到UploadFiles文件夹
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    var fileName = file.FileName;
                    var filePath = Server.MapPath(string.Format("~/{0}", "UploadFiles"));
                    string path = Path.Combine(filePath, fileName);
                    file.SaveAs(path);
                    LogHelper.WriteTextLog("InventoryDataImport", path);
                    ds = ExcelHelper.ImportExceltoDt_New(path);
                    list = InventoryService.GetInventoryImportList(ds);
                    LogHelper.WriteTextLog("InventoryDataImport", JsonHelper.ToJson(list));

                    //存入缓存
                    Cache.Add(token, list);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteTextLog("InventoryDataImport", ex.ToString());
            }
            return Json(list);
        }

        public JsonResult GenerateInventory(string token)
        {
            long operatorID = 0;
            if (CurrentUser != null)
            {
                operatorID = CurrentUser.UserID;
            }
            List<ImportInventoryEntity> list = Cache.Get<List<ImportInventoryEntity>>(token);
            int result = 0;
            if (list != null && list.Count > 0)
            {
                result = InventoryService.insertInventory(list, operatorID);
            }
            return Json(result);
        }
        #endregion

    }
}
