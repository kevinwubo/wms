using Common;
using Entity.ViewModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Service;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuoChe.Controllers
{
    public class ReportController : BaseController
    {
        //
        // GET: /Report/
        public int PAGESIZE = 20;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 客服日常报表
        /// </summary>
        /// <param name="carrierid">承运商ID</param>
        /// <param name="storageid">仓库ID</param>
        /// <param name="customerid">客户ID</param>
        /// <param name="receivername">收货方</param>
        /// <param name="ordertype">订单类型</param>
        /// <param name="orderno">订单编号</param>
        /// <param name="begindate">订单开始时间</param>
        /// <param name="enddate">订单结束时间</param>
        /// <param name="p"></param>
        /// <returns></returns>
        public ActionResult CustomServiceReport(int carrierid = 0, int storageid = 0, int customerid = 0, string receivername = "",
            string ordertype = "", string orderno = "", string begindate = "", string enddate = "", int p = 1)
        {

            List<OrderEntity> mList = null;

            int count = ReportService.GetOrderCount("", carrierid, storageid, customerid, -1, -1, -1, ordertype, orderno, begindate, enddate, -1);
            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "CustomServiceReport";

            if (carrierid > 0 || storageid > 0 || customerid > 0 || !string.IsNullOrEmpty(ordertype) || !string.IsNullOrEmpty(orderno) || !string.IsNullOrEmpty(begindate) || !string.IsNullOrEmpty(enddate))
            {
                mList = ReportService.GetOrderInfoByRule(pager, "", carrierid, storageid, customerid, -1, -1, -1, ordertype, orderno, begindate, enddate, -1);
            }
            else
            {
                mList = ReportService.GetOrderInfoPager(pager);
            }

            //默认承运商
            ViewBag.Carrier = CarrierService.GetCarrierByRule("", 1);//只显示使用中的数据
            //默认仓库
            ViewBag.Storage = StorageService.GetStorageByRule("", 1);//只显示使用中的数据
            //客户信息
            ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据

            ViewBag.reportList = ReportService.CreateReportList(mList);
            ViewBag.GUID = System.Guid.NewGuid().ToString();
            //存入缓存
            Cache.Add(ViewBag.GUID, ViewBag.reportList);
            ViewBag.Pager = pager;
            return View();
        }

        /// <summary>
        /// 客户对账单
        /// </summary>
        /// <param name="storageid">仓库ID</param>
        /// <param name="customerid">客户ID</param>
        /// <param name="receivername">收货方</param>
        /// <param name="ordertype">订单类型</param>
        /// <param name="orderno">订单编号</param>
        /// <param name="begindate">订单开始时间</param>
        /// <param name="enddate">订单结束时间</param>
        /// <returns></returns>
        public ActionResult CustomerReport(int storageid = 0, int customerid = 0, string receivername = "",
            string ordertype = "", string orderno = "", string begindate = "", string enddate = "", int p = 1)
        {
            List<OrderEntity> mList = null;

            int count = ReportService.GetOrderCount("", -1, storageid, customerid, -1, -1, -1, ordertype, orderno, begindate, enddate, -1);
            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "CustomerReport";

            if ( storageid > 0 || customerid > 0 || !string.IsNullOrEmpty(ordertype) || !string.IsNullOrEmpty(orderno) || !string.IsNullOrEmpty(begindate) || !string.IsNullOrEmpty(enddate))
            {
                mList = ReportService.GetOrderInfoByRule(pager, "", -1, storageid, customerid, -1, -1, -1, ordertype, orderno, begindate, enddate, -1);
            }
            else
            {
                mList = ReportService.GetOrderInfoPager(pager);
            }
            //默认承运商
            ViewBag.Carrier = CarrierService.GetCarrierByRule("", 1);//只显示使用中的数据
            //默认仓库
            ViewBag.Storage = StorageService.GetStorageByRule("", 1);//只显示使用中的数据
            //客户信息
            ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据

            ViewBag.Pager = pager;

            ViewBag.reportList = ReportService.CreateReportList(mList);
            ViewBag.GUID = System.Guid.NewGuid().ToString();
            //存入缓存
            Cache.Add(ViewBag.GUID, ViewBag.reportList);
            return View();
        }

        /// <summary>
        /// 供应商对账单
        /// </summary>
        /// <param name="storageid">仓库ID</param>
        /// <param name="carrierid">承运商ID</param>
        /// <param name="receivername">收货方</param>
        /// <param name="ordertype">订单类型</param>
        /// <param name="orderno">订单编号</param>
        /// <param name="begindate">订单开始时间</param>
        /// <param name="enddate">订单结束时间</param>
        /// <returns></returns>
        public ActionResult CarrierReport(int storageid = 0, int carrierid = 0, string receivername = "",
            string ordertype = "", string orderno = "", string begindate = "", string enddate = "", int p = 1)
        {
            List<OrderEntity> mList = null;
            int count = ReportService.GetOrderCount("", carrierid, storageid, -1, -1, -1, -1, ordertype, orderno, begindate, enddate, -1);
            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "CarrierReport";

            if (storageid > 0 || carrierid > 0 || !string.IsNullOrEmpty(ordertype) || !string.IsNullOrEmpty(orderno) || !string.IsNullOrEmpty(begindate) || !string.IsNullOrEmpty(enddate))
            {
                mList = ReportService.GetOrderInfoByRule(pager, "", carrierid, storageid, -1, -1, -1, -1, ordertype, orderno, begindate, enddate, -1);
            }
            else
            {
                mList = ReportService.GetOrderInfoPager(pager);
            }
            //默认承运商
            ViewBag.Carrier = CarrierService.GetCarrierByRule("", 1);//只显示使用中的数据
            //默认仓库
            ViewBag.Storage = StorageService.GetStorageByRule("", 1);//只显示使用中的数据

            ViewBag.Pager = pager;

            ViewBag.reportList = ReportService.CreateReportList(mList);
            ViewBag.GUID = System.Guid.NewGuid().ToString();
            //存入缓存
            Cache.Add(ViewBag.GUID, ViewBag.reportList);
            return View();
        }


        /// <summary>
        /// 利润分析表
        /// </summary>
        /// <param name="carrierid">承运商ID</param>
        /// <param name="storageid">仓库ID</param>
        /// <param name="customerid">客户ID</param>
        /// <param name="receivername">收货方</param>
        /// <param name="ordertype">订单类型</param>
        /// <param name="orderno">订单编号</param>
        /// <param name="begindate">订单开始时间</param>
        /// <param name="enddate">订单结束时间</param>
        /// <param name="p"></param>
        /// <returns></returns>
        public ActionResult ProfitReport(int carrierid = 0, int storageid = 0, int customerid = 0, string receivername = "",
            string ordertype = "", string orderno = "", string begindate = "", string enddate = "", int p = 1)
        {
            List<OrderEntity> mList = null;
            int count = ReportService.GetOrderCount("", carrierid, storageid,customerid, -1, -1, -1, ordertype, orderno, begindate, enddate, -1);
            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "ProfitReport";

            if (storageid > 0 || carrierid > 0 || customerid>0 || !string.IsNullOrEmpty(ordertype) || !string.IsNullOrEmpty(orderno) || !string.IsNullOrEmpty(begindate) || !string.IsNullOrEmpty(enddate))
            {
                mList = ReportService.GetOrderInfoByRule(pager, "", carrierid, storageid, customerid, -1, -1, -1, ordertype, orderno, begindate, enddate, -1);
            }
            else
            {
                mList = ReportService.GetOrderInfoPager(pager);
            }
            //默认承运商
            ViewBag.Carrier = CarrierService.GetCarrierByRule("", 1);//只显示使用中的数据
            //默认仓库
            ViewBag.Storage = StorageService.GetStorageByRule("", 1);//只显示使用中的数据
            //客户信息
            ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据

            ViewBag.reportList = ReportService.CreateReportList(mList);
            ViewBag.GUID = System.Guid.NewGuid().ToString();
            //存入缓存
            Cache.Add(ViewBag.GUID, ViewBag.reportList);
            ViewBag.Pager = pager;
            return View();
        }

        #region 导出excel
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">利润分析表:LRR /客服日常报表:KFR /客户对账单:KHR 供应商对账单:GYSR</param>
        /// <returns></returns>
        public FileResult ReportExportExcel(string type,string guid)
        {
            //获取list数据         

            List<ReportEntity> list = Cache.Get<List<ReportEntity>>(guid);
            //创建Excel文件的对象
            HSSFWorkbook book = new HSSFWorkbook();
            //添加一个sheet
            ISheet sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题 
            //序号	订单编号	订单属性	订单归属	发货仓库	供应商	下单日期	收货方	收货地址	货物重量	配送数量                      //客服日常报表
            //序号	订单编号	订单属性	订单归属	发货仓库	        下单日期	收货方	收货地址	货物重量	配送数量	应收总额              //客户对账单 
            //序号	订单编号	订单属性	订单归属	发货仓库	供应商	下单日期	收货方	收货地址	货物重量	配送数量	应付总额              //供应商对账单
            //序号	订单编号	订单属性	订单归属	发货仓库	供应商	下单日期	收货方	收货地址	货物重量	配送数量	应收总额	应付总额	利润  //利润分析表

            int K = 0;
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(K).SetCellValue("序号");
            row1.CreateCell(K++).SetCellValue("订单编号");
            row1.CreateCell(K++).SetCellValue("订单属性");
            row1.CreateCell(K++).SetCellValue("订单归属");
            row1.CreateCell(K++).SetCellValue("发货仓库");
            if (!"KHR".Equals(type))
            {
                row1.CreateCell(K++).SetCellValue("供应商");
            }
            row1.CreateCell(K++).SetCellValue("下单日期");
            row1.CreateCell(K++).SetCellValue("收货方");
            row1.CreateCell(K++).SetCellValue("收货地址");
            row1.CreateCell(K++).SetCellValue("货物重量");
            row1.CreateCell(K++).SetCellValue("配送数量");
            if ("KHR".Equals(type) || "LRR".Equals(type))
            {
                row1.CreateCell(K++).SetCellValue("应收总额");
            }
            if ("GYSR".Equals(type) || "LRR".Equals(type))
            {
                row1.CreateCell(K++).SetCellValue("应付总额");
            }

            if ("LRR".Equals(type))
            {
                row1.CreateCell(K++).SetCellValue("利润");
            }

            //将数据逐步写入sheet1各个行
            for (int i = 0; i < list.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                int KK = 0;
                rowtemp.CreateCell(KK).SetCellValue(list[i].ID);
                rowtemp.CreateCell(KK++).SetCellValue(list[i].OrderNo);
                rowtemp.CreateCell(KK++).SetCellValue(list[i].OrderType);
                rowtemp.CreateCell(KK++).SetCellValue(list[i].OrderOwner);
                rowtemp.CreateCell(KK++).SetCellValue(list[i].SendStorageName);
                if (!"KHR".Equals(type))
                {
                    rowtemp.CreateCell(KK++).SetCellValue(list[i].CarrierName);
                }
                rowtemp.CreateCell(KK++).SetCellValue(list[i].OrderDate);
                rowtemp.CreateCell(KK++).SetCellValue(list[i].ReceiverName);
                rowtemp.CreateCell(KK++).SetCellValue(list[i].ReceiverAddress);
                rowtemp.CreateCell(KK++).SetCellValue(list[i].Weight);
                rowtemp.CreateCell(KK++).SetCellValue(list[i].Quantity);
                if ("KHR".Equals(type) || "LRR".Equals(type))
                {
                    rowtemp.CreateCell(KK++).SetCellValue(list[i].TotalReceiverFee.ToString());
                }
                if ("GYSR".Equals(type) || "LRR".Equals(type))
                {
                    rowtemp.CreateCell(KK++).SetCellValue(list[i].TotalPayFee.ToString());
                }

                if ("LRR".Equals(type))
                {
                    rowtemp.CreateCell(KK++).SetCellValue(list[i].Profit.ToString());
                }
            }
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", DateTime.Now.ToString("yyyyMMdd") + "报表.xls");
        }
        #endregion 
    }
}
