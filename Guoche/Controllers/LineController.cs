using Common;
using Entity.ViewModel;
using Service;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuoChe.Controllers
{
    public class LineController : BaseController
    {
        //
        // GET: /Line/
        public int PAGESIZE = 20;
        public ActionResult Developing()
        {
            return View();
        }

        public ActionResult Index(int LineID = -1, int p = -1)
        {
            List<LineEntity> mList = null;
            int count = LineService.GetLineCount(LineID);
            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "/Line";
            if (LineID > -1)
            {
                mList = LineService.GetLineInfoByRule(LineID, pager);
            }
            else
            {
                mList = LineService.GetLineInfoPager(pager);
            }
            ViewBag.LineID = LineID;
            ViewBag.Line = mList;
            ViewBag.Pager = pager;
            return View();
        }

        public void Modify(string lineid, string name, int id = 0)
        {
            LineEntity entity = new LineEntity();
            entity.ID = id;
            entity.LineID = lineid.ToInt(0);
            entity.ReceiverName = name;
            entity.Address = "";
            if (entity != null)
            {
                entity.OperatorID = CurrentUser.UserID.ToString().ToInt(0);
            }
            LineService.ModifyLine(entity);
        }

        public void Delete(int id)
        {
            LineService.Delete(id);
            Response.Redirect("/Line/Index");
        }


        #region 订单出库
        /// <summary>
        /// 运输计划  查询已出库订单
        /// </summary>
        /// <param name="carrierid"></param>
        /// <param name="storageid"></param>
        /// <param name="customerid"></param>
        /// <param name="status"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public ActionResult OrderDeliveryPlan(int carrierid = 0, int storageid = 0, int customerid = 0, int status = -1, string orderno = "",
            string begindate = "", string enddate = "", int p = 1, int pageSize = 20)
        {
            List<OrderEntity> mList = null;

            // 默认当月
            if (string.IsNullOrEmpty(begindate) || string.IsNullOrEmpty(enddate))
            {
                DateTime dt = DateTime.Now;
                begindate = dt.Year + "-" + dt.Month + "-" + "01";
                enddate = DateTime.Now.ToString("yyyy-MM-dd");
            }

            //查询未出库 未安排运输计划订单
            int count = OrderService.GetOrderCount("", carrierid, storageid, customerid, status, -1, -1, "", orderno, begindate, enddate, -1, "", "", "F", "F");
            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = pageSize;
            pager.SumCount = count;
            pager.URL = "OrderDeliveryPlan";

            //if (status > -1 || carrierid > 0 || storageid > 0 || customerid > 0 || !string.IsNullOrEmpty(orderno) || !string.IsNullOrEmpty(begindate) || !string.IsNullOrEmpty(enddate))
            //{
                mList = OrderService.GetOrderInfoByRule(pager, "", carrierid, storageid, customerid, status, -1, -1, "", orderno, begindate, enddate, -1, "", "", "F", "F");
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
            //温度
            ViewBag.TemList = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "TM00" && t.Status == 1).ToList();
            //物流方式
            ViewBag.DeliverList = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "DeliverModel00" && t.Status == 1).ToList();

            //承运商类型
            ViewBag.CarrierModel = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "Carrier00" && t.Status == 1).ToList();
            ViewBag.UserID = CurrentUser != null ? CurrentUser.UserID : -1;
            ViewBag.Status = status;
            ViewBag.carrierid = carrierid;
            ViewBag.customerid = customerid;
            ViewBag.storageid = storageid;
            ViewBag.OrderList = mList;
            ViewBag.PageSize = pageSize;
            ViewBag.BeginDate = begindate;
            ViewBag.OrderNo = orderno;
            ViewBag.EndDate = enddate;
            ViewBag.Pager = pager;
            return View();
        }

        /// <summary>
        /// 运输计划 保存
        /// </summary>
        /// <param name="orderids"></param>
        /// <returns></returns>
        public JsonResult OrderDeliveryPlanProcess(string orderids, string carrierName, int carrierId, string temp, string deliveryType,
            string driverName, string driverTelephone, string carModel, string carNo, string deliverDate, string remark)
        {
            bool result = false;
            if (CurrentUser != null)
            {
                OrderDeliverPlanEntity entity = new OrderDeliverPlanEntity();
                entity.OrderIDS = orderids;
                entity.CarrierName = carrierName;
                entity.CarrierID = carrierId;
                entity.Temp = temp;
                entity.DeliveryType = deliveryType;
                entity.DriverName = driverName;
                entity.DriverTelephone = driverTelephone;
                entity.CarModel = carModel;
                entity.CarNo = carNo;
                entity.DeliverDate = !string.IsNullOrEmpty(deliverDate) ? Convert.ToDateTime(deliverDate) : DateTime.Now;
                entity.Remark = remark;
                entity.OperatorID = CurrentUser != null ? CurrentUser.UserID.ToString().ToInt(0) : -1;

                LogHelper.WriteTextLog("运输计划", "保存参数：" + JsonHelper.ToJson(entity) + "操作人：" + CurrentUser.UserID);
                //运输计划保存
                OrderDeliverPlanService.ModifyOrderDeliverPlan(entity);

                //更新订单承运商
                if (!string.IsNullOrEmpty(orderids))
                {
                    string[] ids = orderids.Split(',');
                    foreach (string id in ids)
                    {
                        if (!string.IsNullOrEmpty(id))
                        {
                            LogHelper.WriteTextLog("承运商更新", "订单ID：" + id + "承运商名称：" + carrierName);
                            //更新订单承运商
                            OrderService.UpdateOrderCarrier(id.ToInt(0), carrierId);
                        }
                    }
                }

            }
            return new JsonResult
            {
                Data = result
            };
        }

        #endregion
    }
}
