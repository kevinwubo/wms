using Common;
using DataRepository.DataAccess.Order;
using DataRepository.DataModel;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Helper;

namespace Service
{
    public class ReportService : BaseService
    {
        #region 分页相关
        public static int GetOrderCount(string name = "", int carrierid = -1, int storageid = -1, int customerid = -1, int status = -1, int uploadstatus = -1,
            int orderstatus = -1, string ordertype = "", string orderno = "", string begindate = "", string enddate = "", int operatorid = -1, string ordersource = "")
        {
            return new OrderRepository().GetOrderCount(name, carrierid, storageid, customerid, status, uploadstatus, orderstatus, ordertype, orderno, begindate, enddate, operatorid, ordersource);
        }

        public static List<OrderEntity> GetOrderInfoPager(PagerInfo pager)
        {
            List<OrderEntity> all = new List<OrderEntity>();
            OrderRepository mr = new OrderRepository();
            List<OrderInfo> miList = mr.GetAllOrderInfoPager(pager);
            foreach (OrderInfo mInfo in miList)
            {
                OrderEntity carEntity =OrderService.TranslateOrderEntity(mInfo);
                all.Add(carEntity);
            }
            return all;
        }

        public static List<OrderEntity> GetOrderInfoByRule(PagerInfo pager, string name = "", int carrierid = -1, int storageid = -1, int customerid = -1, int status = -1,
            int uploadstatus = -1, int orderstatus = -1, string ordertype = "", string orderno = "", string begindate = "", string enddate = "", int operatorid = -1, string ordersource = "")
        {
            List<OrderEntity> all = new List<OrderEntity>();
            OrderRepository mr = new OrderRepository();
            List<OrderInfo> miList = mr.GetOrderInfoByRule(name, carrierid, storageid, customerid, status, uploadstatus, orderstatus, ordertype, orderno, begindate, enddate, operatorid, ordersource, pager);

            if (!miList.IsEmpty())
            {
                foreach (OrderInfo mInfo in miList)
                {
                    OrderEntity storeEntity = OrderService.TranslateOrderEntity(mInfo);
                    all.Add(storeEntity);
                }
            }

            return all;
        }
        #endregion


        public static List<ReportEntity> CreateReportList(List<OrderEntity> list)
        {
            List<ReportEntity> reportList = new List<ReportEntity>();
            if (list != null && list.Count > 0)
            {
                int i=1;
                foreach (OrderEntity entity in list)
                {
                    ReportEntity rEntity = new ReportEntity();
                    //序号	订单编号	订单属性	订单归属	发货仓库	供应商	下单日期	收货方	收货地址	货物重量	配送数量	应收总额	应付总额	利润
                    rEntity.ID = i++;
                    rEntity.OrderNo = entity.OrderNo;
                    rEntity.OrderType = entity.OrderTypeDesc;
                    rEntity.OrderOwner = "";
                    rEntity.SendStorageName = entity.sendstorage != null ? entity.sendstorage.StorageName : "";                    
                    rEntity.CarrierName = entity.carrier != null ? entity.carrier.CarrierName : "";
                    rEntity.OrderDate = DateTime.Parse(entity.OrderDate);
                    rEntity.ReceiverName = entity.contact != null ? entity.contact.name : "";
                    rEntity.ReceiverAddress = entity.contact != null ? entity.contact.address : "";
                    rEntity.Weight = TotalWeight(entity.orderDetailList);
                    rEntity.Quantity = TotalQuantity(entity.orderDetailList);
                    rEntity.TotalReceiverFee = entity.configPrice + entity.configHandInAmt + entity.configSortPrice;
                    rEntity.TotalPayFee = entity.configCosting + entity.configHandOutAmt + entity.configSortCosting;
                    rEntity.Profit = rEntity.TotalReceiverFee - rEntity.TotalPayFee;
                    reportList.Add(rEntity);
                }
            }
            return reportList;
        }

        private static int TotalWeight(List<OrderDetailEntity> list)
        {
            int result = 0;
            if (list != null && list.Count > 0)
            {
                foreach (OrderDetailEntity entity in list)
                {
                    result += entity.Weight.ToInt(0);
                }
            }
            return result;
        }

        private static int TotalQuantity(List<OrderDetailEntity> list)
        {
            int result = 0;
            if (list != null && list.Count > 0)
            {
                foreach (OrderDetailEntity entity in list)
                {
                    result += entity.Quantity;
                }
            }
            return result;
        }
    }
}
