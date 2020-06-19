using Common;
using DataRepository.DataModel;
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
        /// 车辆计划报表
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderDeliverPlanReport(int carrierid = -1, string begindate = "", string enddate = "", int p = 1)
        {
            // 默认当月
            if (string.IsNullOrEmpty(begindate) || string.IsNullOrEmpty(enddate))
            {
                DateTime dt = DateTime.Now;
                begindate = dt.Year + "-" + dt.Month + "-" + "01";
                enddate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            List<OrderDeliverPlanEntity> mList = null;

            int count = OrderDeliverPlanService.GetOrderDeliverPlanCount(carrierid, begindate, enddate);
            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "OrderDeliverPlanReport";

            mList = OrderDeliverPlanService.GetOrderDeliverPlanInfoByRule(carrierid, begindate, enddate, pager);
            //默认承运商
            ViewBag.Carrier = CarrierService.GetCarrierByRule("", 1);//只显示使用中的数据
            ViewBag.reportList = mList;
            ViewBag.carrierid = carrierid;
            ViewBag.BeginDate = begindate;
            ViewBag.EndDate = enddate;
            ViewBag.Pager = pager;
            return View();
        }


        #region 订单查询
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">页面权限控制类型 check:通过、拒绝</param>
        /// <param name="carrierid"></param>
        /// <param name="storageid"></param>
        /// <param name="customerid"></param>
        /// <param name="orderstatus"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public ActionResult OrderDetailView(string orderIDs)
        {
            List<OrderEntity> mList = new List<OrderEntity>();

            if (!string.IsNullOrEmpty(orderIDs))
            {
                List<string> orderList = orderIDs.Split(',').ToList<string>();
                if (orderList != null && orderList.Count > 0)
                {
                    foreach (string orderid in orderList)
                    {
                        if (!string.IsNullOrEmpty(orderid))
                        {
                            OrderEntity orderEntity = OrderService.GetOrderByOrderID(orderid.ToInt(0));
                            mList.Add(orderEntity);
                        }
                    }
                }
            }
            ViewBag.OrderList = mList;
            return View();
        }
        #endregion

        /// <summary>
        /// 车辆计划报表 Excel导出
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderDeliverPlanReportToExcel(int carrierid = -1, string begindate = "", string enddate = "", int p = 1)
        {
            // 默认当月
            if (string.IsNullOrEmpty(begindate) || string.IsNullOrEmpty(enddate))
            {
                DateTime dt = DateTime.Now;
                begindate = dt.Year + "-" + dt.Month + "-" + "01";
                enddate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            List<OrderDeliverPlanEntity> list = null;

            int count = OrderDeliverPlanService.GetOrderDeliverPlanCount(carrierid, begindate, enddate);
            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = count;
            pager.SumCount = count;

            list = OrderDeliverPlanService.GetOrderDeliverPlanInfoByRule(carrierid, begindate, enddate, pager);

            //创建Excel文件的对象
            HSSFWorkbook book = new HSSFWorkbook();
            //添加一个sheet
            ISheet sheet1 = book.CreateSheet("Sheet1");
            sheet1.SetColumnWidth(0, 15 * 256);
            sheet1.SetColumnWidth(1, 15 * 256);
            sheet1.SetColumnWidth(2, 15 * 256);
            sheet1.SetColumnWidth(3, 15 * 256);
            sheet1.SetColumnWidth(4, 15 * 256);
            sheet1.SetColumnWidth(5, 15 * 256);
            sheet1.SetColumnWidth(6, 15 * 256);
            sheet1.SetColumnWidth(7, 15 * 256);
            sheet1.SetColumnWidth(8, 15 * 256);
            //给sheet1添加第一行的头部标题 								

            int K = 0;
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("运输单号");
            row1.CreateCell(1).SetCellValue("序号");
            row1.CreateCell(2).SetCellValue("承运商名称");
            row1.CreateCell(3).SetCellValue("温区");
            row1.CreateCell(4).SetCellValue("物流方式");
            row1.CreateCell(5).SetCellValue("驾驶员姓名");
            row1.CreateCell(6).SetCellValue("联系电话");
            row1.CreateCell(7).SetCellValue("车型");
            row1.CreateCell(8).SetCellValue("提货时间");
            row1.CreateCell(9).SetCellValue("备注");
            row1.CreateCell(10).SetCellValue("备注1（客服备注）");
            row1.CreateCell(11).SetCellValue("备注2（运营备注）");
            //将数据逐步写入sheet1各个行
            string remark = "";
            string sfhd = "";
            int row = 1;

            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(row++);
                    rowtemp.CreateCell(0).SetCellValue(list[i].DeliveryNo);
                    rowtemp.CreateCell(1).SetCellValue(list[i].PlanID);
                    rowtemp.CreateCell(2).SetCellValue(list[i].CarrierName);
                    rowtemp.CreateCell(3).SetCellValue(list[i].Temp);
                    rowtemp.CreateCell(4).SetCellValue(list[i].DeliveryType);
                    rowtemp.CreateCell(5).SetCellValue(list[i].DriverName);
                    rowtemp.CreateCell(6).SetCellValue(list[i].DriverTelephone);
                    rowtemp.CreateCell(7).SetCellValue(list[i].CarModel);
                    rowtemp.CreateCell(8).SetCellValue(list[i].DeliverDate.ToShortDateString());
                    rowtemp.CreateCell(9).SetCellValue(list[i].Remark);
                    rowtemp.CreateCell(10).SetCellValue("");
                    rowtemp.CreateCell(11).SetCellValue("");
                }
            }

            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", DateTime.Now.ToString("yyyyMMdd") + "车辆计划报表.xls");

            ////默认承运商
            //ViewBag.Carrier = CarrierService.GetCarrierByRule("", 1);//只显示使用中的数据
            //ViewBag.reportList = mList;
            //ViewBag.carrierid = carrierid;
            //ViewBag.BeginDate = begindate;
            //ViewBag.EndDate = enddate;
            //ViewBag.Pager = pager;
            return View();
        }

        #region 客服日常报表
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
            string ordertype = "", string orderno = "", string begindate = "", string enddate = "", int operatorid = -1, int p = 1)
        {
            // 默认当月
            if (string.IsNullOrEmpty(begindate) || string.IsNullOrEmpty(enddate))
            {
                DateTime dt = DateTime.Now;
                begindate = dt.Year + "-" + dt.Month + "-" + "01";
                enddate = DateTime.Now.ToString("yyyy-MM-dd");
            }

            List<OrderEntity> mList = null;

            int count = ReportService.GetOrderCount("", carrierid, storageid, customerid, -1, -1, -1, ordertype, orderno, begindate, enddate, operatorid).count;
            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "CustomServiceReport";

            if (carrierid > 0 || storageid > 0 || customerid > 0 || !string.IsNullOrEmpty(ordertype) || !string.IsNullOrEmpty(orderno) || !string.IsNullOrEmpty(begindate) || !string.IsNullOrEmpty(enddate) || operatorid > 0)
            {
                mList = ReportService.GetOrderInfoByRule(pager, "", carrierid, storageid, customerid, -1, -1, -1, ordertype, orderno, begindate, enddate, operatorid);
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

            ReEntity report = ReportService.CreateReportList(mList);
            ViewBag.Report = report;

            //订单类型
            List<BaseDataEntity> orderTypeList = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "OrderTypeList").ToList();

            ViewBag.customerid = customerid;
            ViewBag.storageid = storageid;
            ViewBag.carrierid = carrierid;
            ViewBag.OrderType = ordertype;
            ViewBag.ReceiverName = receivername;
            ViewBag.BeginDate = begindate;
            ViewBag.EndDate = enddate;
            ViewBag.orderTypeList = orderTypeList;
            ViewBag.OperatorID = operatorid;
            ViewBag.GUID = System.Guid.NewGuid().ToString();
            //存入缓存
            Cache.Add(ViewBag.GUID, report.reportList);
            //操作人
            ViewBag.Users = UserService.GetUserAll();
            ViewBag.Pager = pager;
            return View();
        }


        /// <summary>
        /// 客服日常报表导出
        /// </summary>
        /// <param name="carrierid"></param>
        /// <param name="storageid"></param>
        /// <param name="customerid"></param>
        /// <param name="receivername"></param>
        /// <param name="ordertype"></param>
        /// <param name="orderno"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public FileResult CustomServiceToExcel(int carrierid = 0, int storageid = 0, int customerid = 0, string receivername = "",
            string ordertype = "", string orderno = "", string begindate = "", string enddate = "")
        {
            int count = ReportService.GetOrderCount("", carrierid, storageid, customerid, -1, -1, -1, ordertype, orderno, begindate, enddate, -1).count;
            PagerInfo pager = new PagerInfo();
            pager.PageIndex = 1;
            pager.PageSize = count;
            pager.SumCount = count;
            pager.URL = "CustomServiceReport";
            List<OrderEntity> list = ReportService.GetOrderInfoByRule(pager, "", carrierid, storageid, customerid, -1, -1, -1, ordertype, orderno, begindate, enddate, -1);
            //创建Excel文件的对象
            HSSFWorkbook book = new HSSFWorkbook();
            //添加一个sheet
            ISheet sheet1 = book.CreateSheet("Sheet1");
            sheet1.SetColumnWidth(0, 15 * 256);
            sheet1.SetColumnWidth(1, 15 * 256);
            sheet1.SetColumnWidth(2, 15 * 256);
            sheet1.SetColumnWidth(3, 15 * 256);
            sheet1.SetColumnWidth(4, 15 * 256);
            sheet1.SetColumnWidth(5, 15 * 256);
            sheet1.SetColumnWidth(6, 15 * 256);
            sheet1.SetColumnWidth(7, 15 * 256);
            sheet1.SetColumnWidth(8, 15 * 256);
            sheet1.SetColumnWidth(9, 20 * 256);
            sheet1.SetColumnWidth(10, 15 * 256);
            sheet1.SetColumnWidth(11, 15 * 256);
            sheet1.SetColumnWidth(12, 15 * 256);
            sheet1.SetColumnWidth(13, 15 * 256);
            sheet1.SetColumnWidth(14, 15 * 256);
            sheet1.SetColumnWidth(15, 15 * 256);
            sheet1.SetColumnWidth(16, 15 * 256);
            sheet1.SetColumnWidth(17, 15 * 256);
            sheet1.SetColumnWidth(18, 15 * 256);
            sheet1.SetColumnWidth(19, 15 * 256);
            //给sheet1添加第一行的头部标题 
            //订单归属	订单类型	订单编号	发货仓库	承运商	下单日期	要求送达时间	实际送货时间	收货方	收货所属省	收货所属市	
            //收货地址	货品明细	批次	生产日期	到期日期	配送数量	货物重量	是否回单	备注

            int K = 0;
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("订单归属");
            row1.CreateCell(1).SetCellValue("订单类型");
            row1.CreateCell(2).SetCellValue("订单编号");
            row1.CreateCell(3).SetCellValue("发货仓库");
            row1.CreateCell(4).SetCellValue("承运商");
            row1.CreateCell(5).SetCellValue("下单日期");
            row1.CreateCell(6).SetCellValue("要求送达时间");
            row1.CreateCell(7).SetCellValue("实际送货时间");
            row1.CreateCell(8).SetCellValue("收货方");
            row1.CreateCell(9).SetCellValue("收货所属省");
            row1.CreateCell(10).SetCellValue("收货所属市");
            row1.CreateCell(11).SetCellValue("收货地址");

            row1.CreateCell(12).SetCellValue("货品明细");
            row1.CreateCell(13).SetCellValue("批次");
            row1.CreateCell(14).SetCellValue("生产日期");
            row1.CreateCell(15).SetCellValue("到期日期");
            row1.CreateCell(16).SetCellValue("配送数量");
            row1.CreateCell(17).SetCellValue("货物重量");
            row1.CreateCell(18).SetCellValue("是否回单");
            row1.CreateCell(19).SetCellValue("备注");
            //将数据逐步写入sheet1各个行
            string remark = "";
            string sfhd = "";
            int row = 1;
            for (int i = 0; i < list.Count; i++)
            {
                sfhd = !string.IsNullOrEmpty(list[i].AttachmentIDs) ? "是" : "否";
                remark = list[i].Remark;
                List<OrderDetailEntity> listDetail = OrderDetailService.GetOrderDetailByOrderID(list[i].OrderID);
                if (listDetail != null && listDetail.Count > 0)
                {
                    for (int j = 0; j < listDetail.Count; j++)
                    {
                        NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(row++);
                        rowtemp.CreateCell(0).SetCellValue(list[i].customer != null ? list[i].customer.CustomerName : "");
                        rowtemp.CreateCell(1).SetCellValue(list[i].OrderTypeDesc);
                        rowtemp.CreateCell(2).SetCellValue(list[i].OrderNo);
                        rowtemp.CreateCell(3).SetCellValue(list[i].sendstorage != null ? list[i].sendstorage.StorageName : "");
                        rowtemp.CreateCell(4).SetCellValue(list[i].carrier != null ? list[i].carrier.CarrierName : "");
                        rowtemp.CreateCell(5).SetCellValue(list[i].OrderDate);
                        rowtemp.CreateCell(6).SetCellValue(list[i].SendDate.ToShortDateString());
                        rowtemp.CreateCell(7).SetCellValue("");
                        rowtemp.CreateCell(8).SetCellValue(list[i].receiver != null ? list[i].receiver.ReceiverName : "");
                        rowtemp.CreateCell(9).SetCellValue(list[i].receiver != null ? (list[i].receiver.province != null ? list[i].receiver.province.ProvinceName : "") : "");
                        rowtemp.CreateCell(10).SetCellValue(list[i].receiver != null ? (list[i].receiver.city != null ? list[i].receiver.city.CityName : "") : "");
                        rowtemp.CreateCell(11).SetCellValue(list[i].receiver != null ? list[i].receiver.Address : "");

                        rowtemp.CreateCell(12).SetCellValue(listDetail[j].GoodsName);
                        rowtemp.CreateCell(13).SetCellValue(listDetail[j].BatchNumber);
                        rowtemp.CreateCell(14).SetCellValue(listDetail[j].ProductDate.ToShortDateString());
                        rowtemp.CreateCell(15).SetCellValue(listDetail[j].ExceedDate.ToShortDateString());
                        rowtemp.CreateCell(16).SetCellValue(listDetail[j].Quantity);
                        rowtemp.CreateCell(17).SetCellValue(listDetail[j].TotalWeight);//.goods != null ? listDetail[j].goods.Weight : ""
                        rowtemp.CreateCell(18).SetCellValue(sfhd);
                        rowtemp.CreateCell(19).SetCellValue(remark);
                    }
                }

            }
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", DateTime.Now.ToString("yyyyMMdd") + "客服日常报表.xls");
        }

        #endregion

        #region 客户对账单
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
            // 默认当月
            if (string.IsNullOrEmpty(begindate) || string.IsNullOrEmpty(enddate))
            {
                DateTime dt = DateTime.Now;
                begindate = dt.Year + "-" + dt.Month + "-" + "01";
                enddate = DateTime.Now.ToString("yyyy-MM-dd");
            }

            List<OrderEntity> mList = null;

            int count = ReportService.GetOrderCount("", -1, storageid, customerid, -1, -1, -1, ordertype, orderno, begindate, enddate, -1).count;
            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "CustomerReport";

            mList = ReportService.GetOrderInfoByRule(pager, "", -1, storageid, customerid, -1, -1, -1, ordertype, orderno, begindate, enddate, -1);

            //订单类型
            List<BaseDataEntity> orderTypeList = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "OrderTypeList").ToList();
            //默认承运商
            ViewBag.Carrier = CarrierService.GetCarrierByRule("", 1);//只显示使用中的数据
            //默认仓库
            ViewBag.Storage = StorageService.GetStorageByRule("", 1);//只显示使用中的数据
            //客户信息
            ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据

            ViewBag.Pager = pager;
            ReEntity report = ReportService.CreateReportList(mList);
            ViewBag.Report = report;

            ViewBag.GUID = System.Guid.NewGuid().ToString();
            ViewBag.storageid = storageid;
            ViewBag.customerid = customerid;
            ViewBag.OrderType = ordertype;
            ViewBag.BeginDate = begindate;
            ViewBag.EndDate = enddate;
            ViewBag.orderTypeList = orderTypeList;
            ViewBag.ReceiverName = receivername;
            //存入缓存
            Cache.Add(ViewBag.GUID, report.reportList);
            return View();
        }

        #endregion

        #region 供应商对账单
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
        public ActionResult CarrierReport(int carrierid = 0, int storageid = 0, int customerid = 0, string receivername = "",
            string ordertype = "", string orderno = "", string begindate = "", string enddate = "", int p = 1)
        {
            // 默认当月
            if (string.IsNullOrEmpty(begindate) || string.IsNullOrEmpty(enddate))
            {
                DateTime dt = DateTime.Now;
                begindate = dt.Year + "-" + dt.Month + "-" + "01";
                enddate = DateTime.Now.ToString("yyyy-MM-dd");
            }

            List<OrderEntity> mList = null;
            int count = ReportService.GetOrderCount("", carrierid, storageid, customerid, -1, -1, -1, ordertype, orderno, begindate, enddate, -1).count;
            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "CarrierReport";

            mList = ReportService.GetOrderInfoByRule(pager, "", carrierid, storageid, customerid, -1, -1, -1, ordertype, orderno, begindate, enddate, -1);

            //默认承运商
            ViewBag.Carrier = CarrierService.GetCarrierByRule("", 1);//只显示使用中的数据
            //默认仓库
            ViewBag.Storage = StorageService.GetStorageByRule("", 1);//只显示使用中的数据

            //客户信息
            ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据

            //订单类型
            List<BaseDataEntity> orderTypeList = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "OrderTypeList").ToList();

            ViewBag.Pager = pager;
            ViewBag.carrierid = carrierid;
            ViewBag.storageid = storageid;
            ViewBag.customerid = customerid;
            ViewBag.OrderType = ordertype;
            ViewBag.BeginDate = begindate;
            ViewBag.EndDate = enddate;
            ViewBag.orderTypeList = orderTypeList;
            ViewBag.ReceiverName = receivername;

            ReEntity report = ReportService.CreateReportList(mList, true);
            ViewBag.Report = report;

            ViewBag.GUID = System.Guid.NewGuid().ToString();
            //存入缓存
            Cache.Add(ViewBag.GUID, report.reportList);
            return View();
        }
        #endregion

        #region 利润分析表
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
            // 默认当月
            if (string.IsNullOrEmpty(begindate) || string.IsNullOrEmpty(enddate))
            {
                DateTime dt = DateTime.Now;
                begindate = dt.Year + "-" + dt.Month + "-" + "01";
                enddate = DateTime.Now.ToString("yyyy-MM-dd");
            }

            List<OrderEntity> mList = null;
            OrderFeeInfo feeinfo = ReportService.GetOrderCount("", carrierid, storageid, customerid, -1, -1, -1, ordertype, orderno, begindate, enddate, -1);
            int count = feeinfo.count;
            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "ProfitReport";

            mList = ReportService.GetOrderInfoByRule(pager, "", carrierid, storageid, customerid, -1, -1, -1, ordertype, orderno, begindate, enddate, -1);

            //默认承运商
            ViewBag.Carrier = CarrierService.GetCarrierByRule("", 1);//只显示使用中的数据
            //默认仓库
            ViewBag.Storage = StorageService.GetStorageByRule("", 1);//只显示使用中的数据
            //客户信息
            ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据
            //订单类型
            List<BaseDataEntity> orderTypeList = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "OrderTypeList").ToList();

            ReEntity report = ReportService.CreateReportList(mList);
            report.TotalAllPayAmount = feeinfo.TotalAllPayAmount;
            report.TotalllReceiverAmount = feeinfo.TotalllReceiverAmount;
            ViewBag.Report = report;




            ViewBag.GUID = System.Guid.NewGuid().ToString();
            ViewBag.carrierid = carrierid;
            ViewBag.storageid = storageid;
            ViewBag.customerid = customerid;
            ViewBag.OrderType = ordertype;
            ViewBag.BeginDate = begindate;
            ViewBag.EndDate = enddate;
            ViewBag.orderTypeList = orderTypeList;
            ViewBag.ReceiverName = receivername;
            //存入缓存
            Cache.Add(ViewBag.GUID, report.reportList);
            ViewBag.Pager = pager;
            return View();
        }
        #endregion

        #region 导出excel

        public FileResult ReportToExcel(int carrierid = 0, int storageid = 0, int customerid = 0, string receivername = "",
        string ordertype = "", string orderno = "", string begindate = "", string enddate = "", string type = "")
        {

            int count = ReportService.GetOrderCount("", carrierid, storageid, customerid, -1, -1, -1, ordertype, orderno, begindate, enddate, -1).count;
            PagerInfo pager = new PagerInfo();
            pager.PageIndex = 1;
            pager.PageSize = count;
            pager.SumCount = count;
            pager.URL = "CustomServiceReport";
            List<OrderEntity> list = ReportService.GetOrderInfoByRule(pager, "", carrierid, storageid, customerid, -1, -1, -1, ordertype, orderno, begindate, enddate, -1);
            ReEntity report = ReportService.CreateReportList(list, true);
            return ReportExportExcel(type, report.reportList, customerid, carrierid);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">利润分析表:LRR /客服日常报表:KFR /客户对账单:KHR 供应商对账单:GYSR</param>
        /// <returns></returns>
        public FileResult ReportExportExcel(string type, List<ReportEntity> list, int customerid = 0, int carrierid = 0)
        {
            //获取list数据        

            //List<ReportEntity> list = Cache.Get<List<ReportEntity>>(guid);
            //创建Excel文件的对象
            HSSFWorkbook book = new HSSFWorkbook();
            //添加一个sheet
            ISheet sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题 
            //序号	订单编号	订单属性	订单归属	发货仓库	供应商	下单日期	收货方	收货地址	货物重量	配送数量                      //客服日常报表
            //序号	订单编号	订单属性	订单归属	发货仓库	        下单日期	收货方	收货地址	货物重量	配送数量	应收总额              //客户对账单 
            //序号	订单编号	订单属性	订单归属	发货仓库	供应商	下单日期	收货方	收货地址	货物重量	配送数量	应付总额              //供应商对账单
            //序号	订单编号	订单属性	订单归属	发货仓库	供应商	下单日期	收货方	收货地址	货物重量	配送数量	应收总额	应付总额	利润  //利润分析表
            sheet1.SetColumnWidth(0, 15 * 256);
            sheet1.SetColumnWidth(1, 15 * 256);
            sheet1.SetColumnWidth(2, 15 * 256);
            sheet1.SetColumnWidth(3, 15 * 256);
            sheet1.SetColumnWidth(4, 15 * 256);
            sheet1.SetColumnWidth(5, 15 * 256);
            sheet1.SetColumnWidth(6, 15 * 256);
            sheet1.SetColumnWidth(7, 15 * 256);
            sheet1.SetColumnWidth(8, 15 * 256);
            sheet1.SetColumnWidth(9, 20 * 256);
            sheet1.SetColumnWidth(10, 15 * 256);
            sheet1.SetColumnWidth(11, 15 * 256);
            sheet1.SetColumnWidth(12, 15 * 256);
            sheet1.SetColumnWidth(13, 15 * 256);
            sheet1.SetColumnWidth(14, 15 * 256);
            sheet1.SetColumnWidth(15, 15 * 256);
            sheet1.SetColumnWidth(16, 15 * 256);
            sheet1.SetColumnWidth(17, 15 * 256);
            sheet1.SetColumnWidth(18, 15 * 256);
            sheet1.SetColumnWidth(19, 15 * 256);
            int K = 0;
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(K).SetCellValue("序号");
            if ("GYSR".Equals(type))
            {
                row1.CreateCell(K++).SetCellValue("运输单号");
            }

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
                row1.CreateCell(K++).SetCellValue("运输应收");
                row1.CreateCell(K++).SetCellValue("装卸应收");
                row1.CreateCell(K++).SetCellValue("分拣应收");
                row1.CreateCell(K++).SetCellValue("应收总额");
            }
            if ("GYSR".Equals(type) || "LRR".Equals(type))
            {
                row1.CreateCell(K++).SetCellValue("运输应付");
                row1.CreateCell(K++).SetCellValue("装卸应付");
                row1.CreateCell(K++).SetCellValue("分拣应付");
                row1.CreateCell(K++).SetCellValue("应付总额");
            }

            if ("LRR".Equals(type))
            {
                row1.CreateCell(K++).SetCellValue("利润");
            }

            if ("GYSR".Equals(type))
            {
                row1.CreateCell(K++).SetCellValue("车牌");
                row1.CreateCell(K++).SetCellValue("车型");
                row1.CreateCell(K++).SetCellValue("驾驶员");
            }
            row1.CreateCell(K++).SetCellValue("备注1");
            row1.CreateCell(K++).SetCellValue("备注2");

            //将数据逐步写入sheet1各个行
            for (int i = 0; i < list.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                int KK = 0;
                rowtemp.CreateCell(KK).SetCellValue(list[i].ID);
                if ("GYSR".Equals(type))
                {
                    rowtemp.CreateCell(KK++).SetCellValue(list[i].DeliveryNo);
                }
                rowtemp.CreateCell(KK++).SetCellValue(list[i].OrderNo);
                rowtemp.CreateCell(KK++).SetCellValue(list[i].OrderType);
                rowtemp.CreateCell(KK++).SetCellValue(list[i].OrderOwner);
                rowtemp.CreateCell(KK++).SetCellValue(list[i].SendStorageName);
                if (!"KHR".Equals(type))
                {
                    rowtemp.CreateCell(KK++).SetCellValue(list[i].CarrierName);
                }
                rowtemp.CreateCell(KK++).SetCellValue(list[i].OrderDate.ToShortDateString());
                rowtemp.CreateCell(KK++).SetCellValue(list[i].ReceiverName);
                rowtemp.CreateCell(KK++).SetCellValue(list[i].ReceiverAddress);
                rowtemp.CreateCell(KK++).SetCellValue(list[i].Weight);
                rowtemp.CreateCell(KK++).SetCellValue(list[i].Quantity);
                if ("KHR".Equals(type) || "LRR".Equals(type))
                {
                    //运输应收、装卸应收、分拣应收
                    rowtemp.CreateCell(KK++).SetCellValue(list[i].configPrice.ToString());
                    rowtemp.CreateCell(KK++).SetCellValue(list[i].configHandInAmt.ToString());
                    rowtemp.CreateCell(KK++).SetCellValue(list[i].configSortPrice.ToString());
                    rowtemp.CreateCell(KK++).SetCellValue(list[i].TotalReceiverFee.ToString());
                }
                if ("GYSR".Equals(type) || "LRR".Equals(type))
                {
                    //运输应付、装卸应付、分拣应付
                    rowtemp.CreateCell(KK++).SetCellValue(list[i].configCosting.ToString());
                    rowtemp.CreateCell(KK++).SetCellValue(list[i].configHandOutAmt.ToString());
                    rowtemp.CreateCell(KK++).SetCellValue(list[i].configSortCosting.ToString());
                    rowtemp.CreateCell(KK++).SetCellValue(list[i].TotalPayFee.ToString());
                }

                if ("LRR".Equals(type))
                {
                    rowtemp.CreateCell(KK++).SetCellValue(list[i].Profit.ToString());
                }

                if ("GYSR".Equals(type))
                {
                    try
                    {
                        rowtemp.CreateCell(KK++).SetCellValue(list[i].CarNo != null ? list[i].CarNo.ToString() : "");
                        rowtemp.CreateCell(KK++).SetCellValue(list[i].CarModel != null ? list[i].CarModel.ToString() : "");
                        rowtemp.CreateCell(KK++).SetCellValue(list[i].DriverName != null ? list[i].DriverName.ToString() : "");
                    }
                    catch (Exception)
                    {
                        LogHelper.WriteErrorLog("GYSR", "GYSR");
                    }
                }

                rowtemp.CreateCell(KK++).SetCellValue(list[i].Remark);
                rowtemp.CreateCell(KK++).SetCellValue(list[i].Remark2);
            }
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", getFileNames(type, list, customerid, carrierid));
        }

        /// <param name="type">利润分析表:LRR /客服日常报表:KFR /客户对账单:KHR 供应商对账单:GYSR</param>
        /// <returns></returns>
        private string getFileNames(string type, List<ReportEntity> list, int customerid = 0, int carrierid = 0)
        {
            string filename = DateTime.Now.ToString("yyyyMMdd") + "力橙对账单{0}.xls";
            string names = "";
            if ("KHR".Equals(type))
            {

                if (customerid > 0)
                {
                    names = list[0].OrderOwner + "客户";
                }
            }
            if ("GYSR".Equals(type))
            {
                if (carrierid > 0)
                {
                    names = list[0].CarrierName + "承运商";
                }
            }

            return string.Format(filename, names);
        }
        #endregion
    }
}
