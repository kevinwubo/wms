using Entity.ViewModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Service.BaseBiz;
using Service.Inventory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Common;
using System.Data;
using Service;
namespace GuoChe.Controllers
{
    public class InventoryController : BaseController
    {
        //
        // GET: /Inventory/
        int PAGESIZE = 20;

        #region 商品入库
        /// <summary>
        /// 商品入库
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //温度
            ViewBag.TemList = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "TM00" && t.Status == 1).ToList();
            //仓库信息
            ViewBag.Storage = StorageService.GetStorageByRule("", 1);//只显示使用中的数据
            return View();
        }

        /// <summary>
        /// 入库保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ContentResult SaveInInventory(int StorageID, string productDate, string json, string tempType)
        {
            bool result = InventoryService.ModifyInventory(StorageID, productDate, 0, json, CurrentUser.UserID, tempType, Common.OperatorType.IN);
            return Content(result ? "T" : "F");
        }

        #endregion

        #region 报损单
        /// <summary>
        /// 报损单
        /// </summary>
        /// <returns></returns>
        public ActionResult LossInventory()
        {
            //仓库信息
            ViewBag.Storage = StorageService.GetStorageByRule("", 1);//只显示使用中的数据
            return View();
        }
        /// <summary>
        /// 报损单保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ContentResult SaveLossInventory(int StorageID, string productDate, string json)
        {
            bool result = InventoryService.ModifyInventory(StorageID, productDate, 0, json, CurrentUser.UserID,"", Common.OperatorType.OUT);
            return Content(result ? "T" : "F");
        }


        /// <summary>
        /// 报损单明细查询
        /// </summary>
        /// <param name="name"></param>
        /// <param name="inventoryType">库存类型：入库、出库</param>
        /// <param name="StorageID">仓库ID</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="inventoryDate">出入库时间</param>
        /// <param name="p"></param>
        /// <returns></returns>
        public ActionResult LossInventoryDetail(string name, string inventoryType, int StorageID = 0, int customerID = 0, string inventoryDate = "", int p = 1)
        {
            PagerInfo pager = new PagerInfo();
            int count = InventoryDetailService.GetInventoryCount(name, inventoryType, StorageID, customerID, inventoryDate, OrderType.BSDD.ToString());
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "LossInventoryDetail";
            List<InventoryDetailEntity> mList = null;
            if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(inventoryType) || StorageID > 0 || customerID > 0 || !string.IsNullOrEmpty(inventoryDate))
            {
                mList = InventoryDetailService.GetInventoryDetailInfoByRule(name, inventoryType, StorageID, customerID, inventoryDate, OrderType.BSDD.ToString(), pager);
            }
            else
            {
                mList = InventoryDetailService.GetInventoryDetailInfoPager(pager);
            }

            ViewBag.inventoryDetail = mList;
            //客户信息
            ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据
            //默认仓库

            ViewBag.Storage = StorageService.GetStorageByRule("", 1);//只显示使用中的数据

            ViewBag.name = name ?? "";
            ViewBag.inventoryType = inventoryType ?? "";
            ViewBag.StorageID = StorageID;
            ViewBag.customerID = customerID;
            ViewBag.inventoryDate = inventoryDate ?? "";
            ViewBag.Pager = pager;
            return View();
        }


        private List<StorageEntity> buildNew(List<StorageEntity> stroageList, string storages)
        {
            if (string.IsNullOrEmpty(storages))
            {
                return stroageList;
            }

            List<StorageEntity> list = new List<StorageEntity>();
            foreach (StorageEntity entity in stroageList)
            {
                List<string> strLost = new List<string>(storages.Split(','));
                if (strLost.Contains(entity.StorageID.ToString()))
                {
                    list.Add(entity);
                }
            }

            if (list == null || list.Count == 0)
            {
                return stroageList;
            }



            return list;
        }

        #endregion

        #region 商品出库
        /// <summary>
        /// 商品出库
        /// </summary>
        /// <returns></returns>
        public ActionResult InventoryOut()
        {
            //仓库信息
            ViewBag.Storage = StorageService.GetStorageByRule("", 1);//只显示使用中的数据
            return View();
        }

        /// <summary>
        /// 出库保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ContentResult SaveOutInventory(int StorageID, string productDate, string json)
        {
            bool result = InventoryService.ModifyInventory(StorageID, productDate, 0, json, CurrentUser.UserID, "", Common.OperatorType.OUT);
            return Content(result ? "T" : "F");
        }

        #endregion

        #region 商品选择器
        /// <summary>
        /// 商品选择器
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mcode"></param>
        /// <param name="CustomerID"></param>
        /// <param name="status"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public ActionResult GoodsSelect(string name, string mcode, int CustomerID = -1, int status = -1, int p = 1)
        {
            List<GoodsEntity> mList = null;

            int count = GoodsService.GetGoodsCount(name, mcode, CustomerID, status);

            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "GoodsSelect";

            //客户信息
            ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据
            ViewBag.GoodsModel = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "GoodsCode" && t.Status == 1).ToList();
            if (!string.IsNullOrEmpty(name) || CustomerID > -1 || status > -1 || !string.IsNullOrEmpty(mcode))
            {
                mList = GoodsService.GetGoodsInfoByRule(name, mcode, CustomerID, status, pager);
            }
            else
            {
                mList = GoodsService.GetGoodsInfoPager(pager);
            }

            ViewBag.Name = name ?? "";
            ViewBag.Status = status;
            ViewBag.Description = mcode;
            ViewBag.CustomerID = CustomerID;
            ViewBag.Goods = mList;
            ViewBag.Pager = pager;
            return View();
        }
        #endregion        

        #region  库存查询
        /// <summary>
        /// 库存查询
        /// </summary>
        /// <param name="name"></param>
        /// <param name="inventoryType">库存类型：入库、出库</param>
        /// <param name="StorageID">仓库ID</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="inventoryDate">出入库时间</param>
        /// <param name="p"></param>
        /// <returns></returns>
        public ActionResult Inventory(string pagetype,string name, string batchNumber, int StorageID = -1, int customerID = -1, int p = 1)
        {
            bool outInventory = false;
            customerID = getCustomerId(customerID,ref outInventory);

            PagerInfo pager = new PagerInfo();
            int count = InventoryService.GetInventoryCount(name, batchNumber, StorageID, customerID);
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "Inventory";
            List<InventoryEntity> mList = null;
            if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(batchNumber) || StorageID > -1 || customerID >-1)
            {
                mList = InventoryService.GetInventoryInfoByRule(name, batchNumber, StorageID, customerID, pager);
            }
            else
            {
                mList = InventoryService.GetInventoryInfoPager(pager);
            }

            ViewBag.inventory = mList;

            if (outInventory)
            {
                //客户信息
                ViewBag.Customer = CustomerService.GetCustomerByKeys(customerID.ToString());
            }
            else
            {

                //客户信息
                ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据
            }
            //默认仓库
            //默认仓库
            List<StorageEntity> stroageList = StorageService.GetStorageByRule("", 1);//只显示使用中的数据

            ViewBag.Storage = buildNew(stroageList, CurrentUser.StorageIDs);

            ViewBag.name = name ?? "";
            ViewBag.batchNumber = batchNumber ?? "";
            ViewBag.StorageID = StorageID;
            ViewBag.customerID = customerID;
            ViewBag.Url = UrlPar;
            ViewBag.Pager = pager;
            ViewBag.PageType = pagetype;

            ViewBag.GUID = System.Guid.NewGuid().ToString();
            //存入缓存
            Cache.Add(ViewBag.GUID, mList);

            return View();
        }

        private int getCustomerId(int customerID, ref bool outInventory)
        {

            try
            {
                if (CurrentUser != null)
                {
                    if (CurrentUser.Roles.Find(p => p.RoleID == 10) != null)
                    {
                        outInventory = true;
                        return CurrentUser.CustomerID;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return customerID;
        }

        #endregion

        #region 库存盘点
        /// <summary>
        /// 库存查询
        /// </summary>
        /// <param name="name"></param>
        /// <param name="inventoryType">库存类型：入库、出库</param>
        /// <param name="StorageID">仓库ID</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="inventoryDate">出入库时间</param>
        /// <param name="p"></param>
        /// <returns></returns>
        public ActionResult InventoryCheck(string name, string batchNumber, int StorageID = -1, int customerID = -1, int p = 1)
        {
            PagerInfo pager = new PagerInfo();
            int count = InventoryService.GetInventoryCount(name, batchNumber, StorageID, customerID);
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "InventoryCheck";
            List<InventoryEntity> mList = null;
            if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(batchNumber) || StorageID > -1 || customerID > -1)
            {
                mList = InventoryService.GetInventoryInfoByRule(name, batchNumber, StorageID, customerID, pager);
            }
            else
            {
                mList = InventoryService.GetInventoryInfoPager(pager);
            }

            ViewBag.inventory = mList;
            //客户信息
            ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据
            //默认仓库
            ViewBag.Storage = StorageService.GetStorageByRule("", 1);//只显示使用中的数据

            ViewBag.name = name ?? "";
            ViewBag.batchNumber = batchNumber ?? "";
            ViewBag.StorageID = StorageID;
            ViewBag.customerID = customerID;
            ViewBag.Pager = pager;
            return View();
        }

        /// <summary>
        /// 盘点中
        /// </summary>
        /// <param name="iid"></param>
        /// <returns></returns>
        public void InventoryChecking(int iid)
        {
            bool result = InventoryService.ModifyInventoryStatus(iid, "T", InventoryStatus.盘点中.ToString());
            //return Content(result ? "T" : "F");
            Response.Redirect("Inventory/InventoryCheck/");
        }

        /// <summary>
        /// 盘点完成
        /// </summary>
        /// <param name="iid"></param>
        /// <returns></returns>
        public void InventoryCheckOver(int iid)
        {
            bool result = InventoryService.ModifyInventoryStatus(iid, "F", InventoryStatus.盘点完成.ToString());
            Response.Redirect("Inventory/InventoryCheck/");
            //return Content(result ? "T" : "F");
        }
        #endregion

        #region 出入库明细查询
        /// <summary>
        /// 出入库明细查询
        /// </summary>
        /// <param name="name"></param>
        /// <param name="inventoryType">库存类型：入库、出库</param>
        /// <param name="StorageID">仓库ID</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="inventoryDate">出入库时间</param>
        /// <param name="p"></param>
        /// <returns></returns>
        public ActionResult InventoryDetail(string name, string inventoryType, int StorageID = 0, int customerID = 0, string inventoryDate = "", int p = 1)
        {
            PagerInfo pager = new PagerInfo();
            int count = InventoryDetailService.GetInventoryCount(name, inventoryType, StorageID, customerID, inventoryDate,"");
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "InventoryDetail";
            List<InventoryDetailEntity> mList = null;
            if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(inventoryType) || StorageID > 0 || customerID > 0 || !string.IsNullOrEmpty(inventoryDate))
            {
                mList = InventoryDetailService.GetInventoryDetailInfoByRule(name, inventoryType, StorageID, customerID, inventoryDate, "", pager);
            }
            else
            {
                mList = InventoryDetailService.GetInventoryDetailInfoPager(pager);
            }

            ViewBag.inventoryDetail = mList;
            //客户信息
            ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据
            //默认仓库
            ViewBag.Storage = StorageService.GetStorageByRule("", 1);//只显示使用中的数据

            ViewBag.name = name ?? "";
            ViewBag.inventoryType = inventoryType ?? "";
            ViewBag.StorageID = StorageID ;
            ViewBag.customerID = customerID;
            ViewBag.inventoryDate = inventoryDate ?? "";
            ViewBag.Pager = pager;
            return View();
        }

        /// <summary>
        /// 库存明细导出
        /// </summary>
        /// <param name="name"></param>
        /// <param name="inventoryType"></param>
        /// <param name="StorageID"></param>
        /// <param name="customerID"></param>
        /// <param name="inventoryDate"></param>
        /// <returns></returns>
        public FileResult InventoryDetailToExcel(string name, string inventoryType, int StorageID = 0, int customerID = 0, string inventoryDate = "")
        {
            int count = InventoryDetailService.GetInventoryCount(name, inventoryType, StorageID, customerID, inventoryDate, "");
            PagerInfo pager = new PagerInfo();
            pager.PageIndex = 1;
            pager.PageSize = count;
            pager.SumCount = count;
            //获取list数据
            List<InventoryDetailEntity> list = InventoryDetailService.GetInventoryDetailInfoByRule(name, inventoryType, StorageID, customerID, inventoryDate, "", pager);

            //创建Excel文件的对象
            HSSFWorkbook book = new HSSFWorkbook();
            //添加一个sheet
            ISheet sheet1 = book.CreateSheet("Sheet1");

            //															
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("类型");
            row1.CreateCell(1).SetCellValue("订单属性");
            row1.CreateCell(2).SetCellValue("订单编号");
            row1.CreateCell(3).SetCellValue("所属客户");
            row1.CreateCell(4).SetCellValue("仓库名称");
            row1.CreateCell(5).SetCellValue("商品编号");
            row1.CreateCell(6).SetCellValue("商品名称");
            row1.CreateCell(7).SetCellValue("批次号");
            row1.CreateCell(8).SetCellValue("生产日期");
            row1.CreateCell(9).SetCellValue("保质期");
            row1.CreateCell(10).SetCellValue("规格型号");
            row1.CreateCell(11).SetCellValue("单位");
            row1.CreateCell(12).SetCellValue("数量");
            row1.CreateCell(13).SetCellValue("重量");
            row1.CreateCell(14).SetCellValue("出入库日期");
            row1.CreateCell(15).SetCellValue("备注");
            //将数据逐步写入sheet1各个行
            for (int i = 0; i < list.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(list[i].InventoryType);
                rowtemp.CreateCell(1).SetCellValue(list[i].OrderTypeDesc);
                rowtemp.CreateCell(2).SetCellValue(list[i].OrderNo);
                rowtemp.CreateCell(3).SetCellValue(list[i].customer != null ? list[i].customer.CustomerName : "");
                rowtemp.CreateCell(4).SetCellValue(list[i].storages != null ? list[i].storages.StorageName : "");
                rowtemp.CreateCell(5).SetCellValue(list[i].goods != null ? list[i].goods.GoodsNo : "");
                rowtemp.CreateCell(6).SetCellValue(list[i].goods != null ? list[i].goods.GoodsName : "");
                rowtemp.CreateCell(7).SetCellValue(list[i].BatchNumber);
                rowtemp.CreateCell(8).SetCellValue(list[i].ProductDate.ToShortDateString());
                rowtemp.CreateCell(9).SetCellValue(list[i].goods != null ? list[i].goods.exDate + list[i].goods.exUnits : "");
                rowtemp.CreateCell(10).SetCellValue(list[i].goods != null ? list[i].goods.GoodsModel : "");
                rowtemp.CreateCell(11).SetCellValue(list[i].goods != null ? list[i].goods.Units : "");
                rowtemp.CreateCell(12).SetCellValue(list[i].Quantity);
                rowtemp.CreateCell(13).SetCellValue(list[i].goods != null ? list[i].goods.Weight : "");
                rowtemp.CreateCell(14).SetCellValue(list[i].InventoryDate.ToShortDateString());
                rowtemp.CreateCell(15).SetCellValue(list[i].Remark);
            }
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);

            string filename = DateTime.Now.ToString("yyyyMMdd") + "{0}商品信息出入库明细 .xls";
            return File(ms, "application/vnd.ms-excel", string.Format(filename, name));
        }

        #endregion

        #region 库位查询
        /// <summary>
        /// 库位查询
        /// </summary>
        /// <param name="storageid"></param>
        /// <param name="storageareano"></param>
        /// <param name="status"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public ActionResult InventoryLocation(int storageid = -1, string storageareano = "", string storagesubareano = "", int status = -1, int p = 1)
        {
            List<StorageLocationEntity> mList = null;
            int storageidn = storageid;
            int count = StorageLocationService.GetStorageLocationCount(storageidn, storageareano, storagesubareano, status);

            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = 50;
            pager.SumCount = count;
            pager.URL = "InventoryLocation";
            //仓库
            ViewBag.Storage = StorageService.GetStorageByRule("", 1);//只显示使用中的数据
            ////库位区域
            //ViewBag.AreasNo = StorageLocationService.GetAreaNoByStorageID(storageid, status);
            ////库位子区域
            //ViewBag.SubAreasNo = StorageLocationService.GetSubAreaNoByStorageAreaNo(storageareano, status);
            if (storageidn > -1 || status > -1 || !string.IsNullOrEmpty(storageareano) || !string.IsNullOrEmpty(storagesubareano))
            {
                mList = StorageLocationService.GetStorageLocationInfoByRule(storageidn, storageareano, storagesubareano, status, pager);
            }
            else
            {
                mList = StorageLocationService.GetStorageLocationInfoPager(pager);
            }

            ViewBag.AreasNo = storageareano;
            ViewBag.SubAreasNo = storagesubareano;
            ViewBag.LocationHtml = GetHtml(mList);
            ViewBag.StorageID = storageid;
            ViewBag.Status = status;
            ViewBag.storageAreaNo = storageareano;
            ViewBag.StorageLocation = mList;
            ViewBag.Pager = pager;

            return View();
        }



        private string GetHtml(List<StorageLocationEntity> mList)
        {
            StringBuilder builder = new StringBuilder();

            if (mList != null && mList.Count > 0)
            {
                for (int i = 0; i < mList.Count; i++)
                {
                    int c = i % 10;
                    if (c == 0)
                    {
                        builder.Append("<tr class='even')>");
                    }
                    builder.Append("<td style='width:120px'><span class='btn btn-box' data-bubble='2' style='margin-bottom:5px'>" + mList[i].StorageLocationNo + "</span></td>");

                    if (i == mList.Count - 1 || (i + 1) % 10 == 0)
                    {
                        builder.Append("</tr>");
                    }
                }
            }

            return builder.ToString();
        }
        #endregion

        #region 导出excel
        public FileResult ExportExcel(string pagetype, string name, string batchNumber, int StorageID = -1, int customerID = -1)
        {
            int count = InventoryService.GetInventoryCount(name, batchNumber, StorageID, customerID);
            PagerInfo pager = new PagerInfo();
            pager.PageIndex = 1;
            pager.PageSize = count;
            pager.SumCount = count;
            //获取list数据
            List<InventoryEntity> list = InventoryService.GetInventoryInfoByRule(name, batchNumber, StorageID, customerID, pager); 

            //创建Excel文件的对象
            HSSFWorkbook book = new HSSFWorkbook();
            //添加一个sheet
            ISheet sheet1 = book.CreateSheet("Sheet1");

            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("所属客户");
            row1.CreateCell(1).SetCellValue("仓库名称");
            row1.CreateCell(2).SetCellValue("商品编号");
            row1.CreateCell(3).SetCellValue("商品名称");
            row1.CreateCell(4).SetCellValue("规格型号");
            row1.CreateCell(5).SetCellValue("单位");
            row1.CreateCell(6).SetCellValue("批次号");
            row1.CreateCell(7).SetCellValue("生产日期");
            row1.CreateCell(8).SetCellValue("保质期");
            row1.CreateCell(9).SetCellValue("数量");
            row1.CreateCell(10).SetCellValue("可用库存数量");
            row1.CreateCell(11).SetCellValue("待出库数量");
            row1.CreateCell(12).SetCellValue("重量");
            row1.CreateCell(13).SetCellValue("出入库日期");
            //将数据逐步写入sheet1各个行
            for (int i = 0; i < list.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(list[i].customer != null ? list[i].customer.CustomerName : "");
                rowtemp.CreateCell(1).SetCellValue(list[i].storages != null ? list[i].storages.StorageName : "");
                rowtemp.CreateCell(2).SetCellValue(list[i].goods != null ? list[i].goods.GoodsNo : "");
                rowtemp.CreateCell(3).SetCellValue(list[i].goods != null ? list[i].goods.GoodsName : "");
                rowtemp.CreateCell(4).SetCellValue(list[i].goods != null ? list[i].goods.GoodsModel : "");
                rowtemp.CreateCell(5).SetCellValue(list[i].goods != null ? list[i].goods.Units : "");
                rowtemp.CreateCell(6).SetCellValue(list[i].BatchNumber);
                rowtemp.CreateCell(7).SetCellValue(list[i].ProductDate.ToShortDateString());
                rowtemp.CreateCell(8).SetCellValue(list[i].goods != null ? list[i].goods.exDate + list[i].goods.exUnits : "");
                rowtemp.CreateCell(9).SetCellValue(list[i].Quantity);
                rowtemp.CreateCell(10).SetCellValue(list[i].CanUseQuantity);
                rowtemp.CreateCell(11).SetCellValue(list[i].WaitQuantity);
                rowtemp.CreateCell(12).SetCellValue(list[i].goods != null ? list[i].goods.Weight : "");
                rowtemp.CreateCell(13).SetCellValue(list[i].InventoryDate.ToShortDateString());
            }
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", getFileNames(list, customerID, StorageID));
        }

        /// <summary>
        /// 库存报表：xx客户+导出日期；  例:海南椰小鸡餐饮20200610库存报表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="customerid"></param>
        /// <param name="StorageID"></param>
        /// <returns></returns>
        private string getFileNames(List<InventoryEntity> list, int customerid = 0, int StorageID = 0)
        {
            string filename = "";

            if (customerid > 0 && list[0].customer != null)
            {
                filename += filename + list[0].customer.CustomerName;
            }
            if (StorageID > 0 && list[0].storages != null)
            {
                filename += filename + list[0].storages.StorageName;
            }

            return string.Format("{0}" + DateTime.Now.ToString("yyyyMMdd") + "库存报表.xls", filename);
        }
        #endregion     

        #region 库存删除
        public JsonResult Remove(string iid)
        {

            InventoryService.Remove(long.Parse(iid));
            return new JsonResult
            {
                Data = iid
            };
        }
        #endregion
        

        #region 订单出库  订单最后一步
         /// <summary>
        /// 订单出库  查询未出库订单
        /// </summary>
        /// <param name="carrierid"></param>
        /// <param name="storageid"></param>
        /// <param name="customerid"></param>
        /// <param name="status"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public ActionResult OrderOut(int carrierid = 0, int storageid = 0, int customerid = 0, int status = -1, string orderno="",
            string begindate = "", string enddate = "", string orderOutStatus = "", int p = 1, int pageSize = 20)
        {
            List<OrderEntity> mList = null;

            orderOutStatus = !string.IsNullOrEmpty(orderOutStatus) ? orderOutStatus : "F";//默认未出库订单

            // 默认当月
            if (string.IsNullOrEmpty(begindate) || string.IsNullOrEmpty(enddate))
            {
                DateTime dt = DateTime.Now;
                begindate = dt.Year + "-" + dt.Month + "-" + "01";
                enddate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            //查询未出库 已安排运输计划订单
            int count = OrderService.GetOrderCount("", carrierid, storageid, customerid, status, -1, -1, "", orderno, begindate, enddate, -1, "", "", orderOutStatus, "T", " AND OrderType!='YSDDB'");

            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = pageSize;
            pager.SumCount = count;
            pager.URL = "OrderOut";
           

            //if (status > -1 || carrierid > 0 || storageid > 0 || customerid > 0 || !string.IsNullOrEmpty(orderno) || !string.IsNullOrEmpty(begindate) || !string.IsNullOrEmpty(enddate))
            //{
            mList = OrderService.GetOrderInfoByRule(pager, "", carrierid, storageid, customerid, status, -1, -1, "", orderno, begindate, enddate, -1, "", "", orderOutStatus, "T", " AND OrderType!='YSDDB'");
            //}
            //else
            //{
            //    mList = OrderService.GetOrderInfoPager(pager);
            //}
            //默认承运商
            ViewBag.Carrier = CarrierService.GetCarrierByRule("", 1);//只显示使用中的数据
            //默认仓库
            ViewBag.Storage = StorageService.GetStorageByRule("", 1);//只显示使用中的数据
            //门店
            ViewBag.Goods = GoodsService.GetGoodsByRule("", 1);//只显示使用中的数据
            //客户信息
            ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据

            ViewBag.UserID=CurrentUser!=null?CurrentUser.UserID:-1;
            ViewBag.Status = status;
            ViewBag.carrierid = carrierid;
            ViewBag.customerid = customerid;
            ViewBag.storageid = storageid;
            ViewBag.OrderList = mList;
            ViewBag.PageSize = pageSize;
            ViewBag.BeginDate = begindate;
            ViewBag.OrderNo = orderno;
            ViewBag.EndDate = enddate;
            ViewBag.orderOutStatus = orderOutStatus;
            ViewBag.Pager = pager;
            return View();
        }

        /// <summary>
        /// 订单出库操作
        /// </summary>
        /// <param name="orderids"></param>
        /// <returns></returns>
        public JsonResult OrderOutProcess(string orderids)
        {
            bool temp = false;

            if (!string.IsNullOrEmpty(orderids))
            {
                string[] OrderIds = orderids.Split(',');
                foreach(string orderid in OrderIds)
                {
                    if (!string.IsNullOrEmpty(orderid))
                    {
                        LogHelper.WriteTextLog("订单出库开始", "订单号：" + orderid + "操作人：" + CurrentUser.UserID);

                        OrderInventoryService.OrderInventoryProcess(orderid.ToLong(0));

                        LogHelper.WriteTextLog("订单出库结束", "订单号：" + orderid + "操作人：" + CurrentUser.UserID);
                    }
                }
            }

            return new JsonResult
            {
                Data = temp
            };
        }

        #endregion

    }
}
