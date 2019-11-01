using Common;
using DataRepository.DataModel;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Order
{
    public class OrderRepository : DataAccessBase
    {

        public List<OrderInfo> GetAllOrder()
        {
            List<OrderInfo> result = new List<OrderInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderStatement.GetAllOrder, "Text"));
            result = command.ExecuteEntityList<OrderInfo>();
            return result;
        }

        public List<OrderInfo> GetOrderByKeys(string keys)
        {
            List<OrderInfo> result = new List<OrderInfo>();
            if (!string.IsNullOrEmpty(keys))
            {
                string sqlText = OrderStatement.GetOrderByKeys;
                sqlText = sqlText.Replace("#ids#", keys);
                DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
                result = command.ExecuteEntityList<OrderInfo>();
            }

            return result;
        }

        public List<OrderInfo> GetOrderByRule(string name, int status)
        {
            List<OrderInfo> result = new List<OrderInfo>();
            string sqlText = OrderStatement.GetAllOrderByRule;
            if (!string.IsNullOrEmpty(name))
            {
                sqlText += " AND OrderName LIKE '%'+@key+'%'";
            }
            if (status > -1)
            {
                sqlText += " AND Status=@Status";
            }


            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            if (!string.IsNullOrEmpty(name))
            {
                command.AddInputParameter("@key", DbType.String, name);
            }
            if (status > -1)
            {
                command.AddInputParameter("@Status", DbType.Int32, status);
            }

            result = command.ExecuteEntityList<OrderInfo>();
            return result;
        }

        public OrderInfo GetOrderByOrderID(long oid)
        {
            OrderInfo result = new OrderInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderStatement.GetOrderByOrderID, "Text"));
            command.AddInputParameter("@OrderID", DbType.String, oid);
            result = command.ExecuteEntity<OrderInfo>();
            return result;
        }

        public OrderInfo GetOrderByOrderNo(string orderNo)
        {
            OrderInfo result = new OrderInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderStatement.GetOrderByOrderNo, "Text"));
            command.AddInputParameter("@OrderNO", DbType.String, orderNo);
            result = command.ExecuteEntity<OrderInfo>();
            return result;
        }

        public long CreateNew(OrderInfo Order)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderStatement.CreateNewOrder, "Text"));
            command.AddInputParameter("@OrderNo", DbType.String, Order.OrderNo);
            command.AddInputParameter("@MergeNo", DbType.String, Order.MergeNo);
            command.AddInputParameter("@OrderType", DbType.String, Order.OrderType);
            command.AddInputParameter("@SubOrderType", DbType.String, Order.SubOrderType);
            command.AddInputParameter("@ReceiverID", DbType.Int32, Order.ReceiverID);
            command.AddInputParameter("@CustomerID", DbType.Int32, Order.CustomerID);            
            command.AddInputParameter("@SendStorageID", DbType.Int32, Order.SendStorageID);
            command.AddInputParameter("@ReceiverStorageID", DbType.Int32, Order.ReceiverStorageID);
            command.AddInputParameter("@CarrierID", DbType.Int32, Order.CarrierID);

            command.AddInputParameter("@OrderDate", DbType.DateTime, Order.OrderDate);
            command.AddInputParameter("@SendDate", DbType.DateTime, Order.SendDate);
            command.AddInputParameter("@configPrice", DbType.Decimal, Order.configPrice);
            command.AddInputParameter("@configHandInAmt", DbType.Decimal, Order.configHandInAmt);
            command.AddInputParameter("@configSortPrice", DbType.Decimal, Order.configSortPrice);
            command.AddInputParameter("@configCosting", DbType.Decimal, Order.configCosting);
            command.AddInputParameter("@configHandOutAmt", DbType.Decimal, Order.configHandOutAmt);
            command.AddInputParameter("@configSortCosting", DbType.Decimal, Order.configSortCosting);


            command.AddInputParameter("@DeliveryType", DbType.String, Order.DeliveryType);
            command.AddInputParameter("@TempType", DbType.String, Order.TempType);
            command.AddInputParameter("@OrderStatus", DbType.Int32, Order.OrderStatus);
            command.AddInputParameter("@UploadStatus", DbType.Int32, Order.UploadStatus);
            command.AddInputParameter("@Status", DbType.Int32, Order.Status);
            command.AddInputParameter("@Remark", DbType.String, Order.Remark);            
            command.AddInputParameter("@OperatorID", DbType.Int32, Order.OperatorID);

            command.AddInputParameter("@IsImport", DbType.String, Order.IsImport); 
            command.AddInputParameter("@OrderSource", DbType.String, Order.OrderSource); 
            command.AddInputParameter("@SalesMan", DbType.String, Order.SalesMan); 
            command.AddInputParameter("@PromotionMan", DbType.String, Order.PromotionMan);
            command.AddInputParameter("@OrderOutStatus", DbType.String, Order.OrderOutStatus);
            command.AddInputParameter("@LineID", DbType.Int32, Order.LineID); 

            command.AddInputParameter("@CreateDate", DbType.DateTime, Order.CreateDate);
            command.AddInputParameter("@ChangeDate", DbType.DateTime, Order.ChangeDate);

            var o = command.ExecuteScalar<object>();
            return Convert.ToInt64(o);
        }


        /// <summary>
        ///  更新订单出库状态
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int UpdateOrderOutType(OrderInfo order)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderStatement.UpdateOrderOutType, "Text"));
            command.AddInputParameter("@OrderOutStatus", DbType.String, order.OrderOutStatus);
            command.AddInputParameter("@OrderID", DbType.Int32, order.OrderID);
            return command.ExecuteNonQuery();
        }

        public int UpdateOrderAttachmentIDs(OrderInfo order)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderStatement.UpdateOrderAttachmentIDs, "Text"));
            command.AddInputParameter("@AttachmentIDs", DbType.String, order.AttachmentIDs);
            command.AddInputParameter("@OrderID", DbType.Int32, order.OrderID);
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// 更新配送时间
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int UpdateOrderArriverDate(OrderInfo order)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderStatement.UpdateOrderArriverDate, "Text"));
            command.AddInputParameter("@ArriverDate", DbType.DateTime, order.ArriverDate);
            command.AddInputParameter("@OrderID", DbType.Int32, order.OrderID);
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int UpdateOrderStatus(OrderInfo order)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderStatement.UpdateOrderStatus, "Text"));
            command.AddInputParameter("@OrderStatus", DbType.Int32, order.OrderStatus);
            command.AddInputParameter("@OrderID", DbType.Int32, order.OrderID);
            return command.ExecuteNonQuery();
        }


        /// <summary>
        /// 更新承运商信息
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int UpdateOrderCarrier(OrderInfo order)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderStatement.UpdateOrderCarrier, "Text"));
            command.AddInputParameter("@CarrierID", DbType.Int32, order.CarrierID);
            command.AddInputParameter("@OrderID", DbType.Int32, order.OrderID);
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// 更新下载(接单)状态
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int UpdateUploadStatus(OrderInfo order)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderStatement.UpdateUploadStatus, "Text"));
            command.AddInputParameter("@UploadStatus", DbType.Int32, order.UploadStatus);
            command.AddInputParameter("@OrderID", DbType.Int32, order.OrderID);
            return command.ExecuteNonQuery();
        }

        public int ModifyOrder(OrderInfo Order)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderStatement.ModifyOrder, "Text"));
            command.AddInputParameter("@OrderNo", DbType.String, Order.OrderNo);
            command.AddInputParameter("@MergeNo", DbType.String, Order.MergeNo);
            command.AddInputParameter("@OrderType", DbType.String, Order.OrderType);
            command.AddInputParameter("@ReceiverID", DbType.Int32, Order.ReceiverID);
            command.AddInputParameter("@CustomerID", DbType.Int32, Order.CustomerID);            
            command.AddInputParameter("@SendStorageID", DbType.Int32, Order.SendStorageID);
            command.AddInputParameter("@ReceiverStorageID", DbType.Int32, Order.ReceiverStorageID);
            command.AddInputParameter("@CarrierID", DbType.Int32, Order.CarrierID);

            command.AddInputParameter("@OrderDate", DbType.DateTime, Order.OrderDate);
            command.AddInputParameter("@SendDate", DbType.DateTime, Order.SendDate);
            command.AddInputParameter("@configPrice", DbType.Decimal, Order.configPrice);
            command.AddInputParameter("@configHandInAmt", DbType.Decimal, Order.configHandInAmt);
            command.AddInputParameter("@configSortPrice", DbType.Decimal, Order.configSortPrice);
            command.AddInputParameter("@configCosting", DbType.Decimal, Order.configCosting);
            command.AddInputParameter("@configHandOutAmt", DbType.Decimal, Order.configHandOutAmt);
            command.AddInputParameter("@configSortCosting", DbType.Decimal, Order.configSortCosting);

            command.AddInputParameter("@TempType", DbType.String, Order.TempType);
            command.AddInputParameter("@OrderStatus", DbType.Int32, Order.OrderStatus);
            command.AddInputParameter("@UploadStatus", DbType.Int32, Order.UploadStatus);
            command.AddInputParameter("@Status", DbType.Int32, Order.Status);
            command.AddInputParameter("@DeliveryType", DbType.String, Order.DeliveryType);
            command.AddInputParameter("@Remark", DbType.String, Order.Remark);
            command.AddInputParameter("@OperatorID", DbType.Int32, Order.OperatorID);            
            command.AddInputParameter("@ChangeDate", DbType.DateTime, Order.ChangeDate);
            command.AddInputParameter("@OrderID", DbType.Int32, Order.OrderID);
            return command.ExecuteNonQuery();
        }

        public int Remove(long OrderID)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderStatement.Remove, "Text"));
            command.AddInputParameter("@OrderID", DbType.Int64, OrderID);
            int result = command.ExecuteNonQuery();
            return result;
        }

        public int Delete(long OrderID)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderStatement.Delete, "Text"));
            command.AddInputParameter("@OrderID", DbType.Int64, OrderID);
            int result = command.ExecuteNonQuery();
            return result;
        }


        #region 分页相关
        public List<OrderInfo> GetAllOrderInfoPager(PagerInfo pager)
        {
            List<OrderInfo> result = new List<OrderInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderStatement.GetAllOrderInfoPager, "Text"));
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);
            result = command.ExecuteEntityList<OrderInfo>();
            return result;
        }


        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="name"></param>
        /// <param name="carrierid">承运商</param>
        /// <param name="storageid">仓库</param>
        /// <param name="customerid">客户</param>
        /// <param name="status">订单状态</param>
        /// <param name="uploadstatus">状态</param>
        /// <param name="orderstatus">订单状态</param>
        /// <param name="ordertype">订单类型</param>
        /// <param name="orderno">订单编号</param>
        /// <param name="begindate">订单开始时间</param>
        /// <param name="enddate">订单结束时间</param>
        /// <param name="operatorid">入单人</param>
        /// <param name="ordersource">订单来源</param>
        /// <param name="subOrderType">订单子状态</param>
        /// <param name="OrderOutStatus">订单出库状态</param>
        /// <param name="desc">排序规则</param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public List<OrderInfo> GetOrderInfoByRule(string name, int carrierid, int storageid, int customerid, int status, int uploadstatus,
            int orderstatus, string ordertype, string orderno, string begindate, string enddate, int operatorid, string ordersource, string subOrderType, string OrderOutStatus, string desc, PagerInfo pager)
        {
            List<OrderInfo> result = new List<OrderInfo>();

            StringBuilder builder = new StringBuilder();
            if (carrierid > 0)
            {
                builder.Append(" AND Carrierid=@Carrierid");
            }
            if (storageid > 0)
            {
                builder.Append(" AND StorageID=@StorageID");
            }
            if (customerid > 0)
            {
                builder.Append(" AND CustomerID=@CustomerID");
            }
            if (status > -1)
            {
                builder.Append(" AND Status=@Status");
            }
            if (uploadstatus > -1)
            {
                builder.Append(" AND UploadStatus=@UploadStatus");
            }
            if (orderstatus > -1)
            {
                builder.Append(" AND orderstatus=@orderstatus");
            }
            if (!string.IsNullOrEmpty(ordertype))
            {
                builder.Append(" AND ordertype=@ordertype");
            }
            if (!string.IsNullOrEmpty(orderno))//订单号 收货方查询
            {
                builder.Append(" AND (orderno='" + orderno + "' or ReceiverID in(select ReceiverID from wms_ReceiverInfo where ReceiverName like '%" + orderno + "%') or ReceiverStorageID in (select StorageID from wms_StorageInfo where StorageName like '%" + orderno + "%' )) ");
            }
            if (!string.IsNullOrEmpty(begindate) && !string.IsNullOrEmpty(enddate))
            {
                builder.Append(" AND OrderDate BETWEEN @begindate AND @enddate ");
            }
            if (operatorid > -1)
            {
                builder.Append(" AND OperatorID=@OperatorID ");
            }
            if (!string.IsNullOrEmpty(ordersource))
            {
                builder.Append(" AND OrderSource=@OrderSource");
            }
            if (!string.IsNullOrEmpty(subOrderType))
            {
                builder.Append(" AND SubOrderType=@SubOrderType");
            }

            if (!string.IsNullOrEmpty(OrderOutStatus))
            {
                builder.Append(" AND OrderOutStatus=@OrderOutStatus");
            }

            string sqlText = OrderStatement.GetAllOrderInfoByRulePagerHeader + builder.ToString() + OrderStatement.GetAllOrderInfoByRulePagerFooter;

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(string.Format(sqlText, desc), "Text"));
            if (carrierid > 0)
            {
                command.AddInputParameter("@Carrierid", DbType.Int32, carrierid);
            }
            if (storageid > 0)
            {
                command.AddInputParameter("@StorageID", DbType.Int32, storageid);
            }
            if (customerid > 0)
            {
                command.AddInputParameter("@CustomerID", DbType.Int32, customerid);
            }
            if (status > -1)
            {
                command.AddInputParameter("@Status", DbType.Int32, status);
            }
            if (uploadstatus > -1)
            {
                command.AddInputParameter("@UploadStatus", DbType.Int32, uploadstatus);
            }
            if (orderstatus > -1)
            {
                command.AddInputParameter("@OrderStatus", DbType.Int32, orderstatus);
            }
            if (!string.IsNullOrEmpty(ordertype))
            {
                command.AddInputParameter("@ordertype", DbType.String, ordertype);
            }
            //if (!string.IsNullOrEmpty(orderno))
            //{
            //    command.AddInputParameter("@orderno", DbType.String, orderno);
            //}
            if (!string.IsNullOrEmpty(begindate) && !string.IsNullOrEmpty(enddate))
            {
                command.AddInputParameter("@begindate", DbType.String, begindate);
                command.AddInputParameter("@enddate", DbType.String, enddate);
            }
            if (operatorid > -1)
            {
                command.AddInputParameter("@OperatorID", DbType.Int32, operatorid);
            }
            if (!string.IsNullOrEmpty(ordersource))
            {
                command.AddInputParameter("@OrderSource", DbType.String, ordersource);
            }
            if (!string.IsNullOrEmpty(subOrderType))
            {
                command.AddInputParameter("@SubOrderType", DbType.String, subOrderType);
            }
            if (!string.IsNullOrEmpty(OrderOutStatus))
            {
                command.AddInputParameter("@OrderOutStatus", DbType.String, OrderOutStatus);
            }
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);

            result = command.ExecuteEntityList<OrderInfo>();
            return result;
        }

        public OrderFeeInfo GetOrderCount(string name, int carrierid, int storageid, int customerid, int status, int uploadstatus,
            int orderstatus, string ordertype, string orderno, string begindate, string enddate, int operatorid, string ordersource, string subOrderType, string OrderOutStatus)
        {
            OrderFeeInfo feeInfo = new OrderFeeInfo();
            StringBuilder builder = new StringBuilder();
            builder.Append(OrderStatement.GetCount);
            if (carrierid > 0)
            {
                builder.Append(" AND Carrierid=@Carrierid");
            }
            if (storageid > 0)
            {
                builder.Append(" AND StorageID=@StorageID");
            }
            if (customerid > 0)
            {
                builder.Append(" AND CustomerID=@CustomerID");
            }
            if (status > -1)
            {
                builder.Append(" AND Status=@Status");
            }
            if (uploadstatus > -1)
            {
                builder.Append(" AND UploadStatus=@UploadStatus");
            }
            if (orderstatus > -1)
            {
                builder.Append(" AND orderstatus=@orderstatus");
            }
            if (!string.IsNullOrEmpty(ordertype))
            {
                builder.Append(" AND ordertype=@ordertype");
            }
            if (!string.IsNullOrEmpty(orderno))
            {
                //builder.Append(" AND orderno=@orderno ");
                builder.Append(" AND (orderno='" + orderno + "' or ReceiverID in(select ReceiverID from wms_ReceiverInfo where ReceiverName like '%" + orderno + "%') or ReceiverStorageID in (select StorageID from wms_StorageInfo where StorageName like '%" + orderno + "%' )) ");
            }
            if (!string.IsNullOrEmpty(begindate) && !string.IsNullOrEmpty(enddate))
            {
                builder.Append(" AND OrderDate BETWEEN @begindate AND @enddate ");
            }
            if (operatorid > -1)
            {
                builder.Append(" AND OperatorID=@OperatorID ");
            }
            if (!string.IsNullOrEmpty(ordersource))
            {
                builder.Append(" AND OrderSource=@OrderSource");
            }
            if (!string.IsNullOrEmpty(subOrderType))
            {
                builder.Append(" AND SubOrderType=@SubOrderType");
            }

            if (!string.IsNullOrEmpty(OrderOutStatus))
            {
                builder.Append(" AND OrderOutStatus=@OrderOutStatus");
            }

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(builder.ToString(), "Text"));

            if (carrierid > 0)
            {
                command.AddInputParameter("@Carrierid", DbType.Int32, carrierid);
            }
            if (storageid > 0)
            {
                command.AddInputParameter("@StorageID", DbType.Int32, storageid);
            }
            if (customerid > 0)
            {
                command.AddInputParameter("@CustomerID", DbType.Int32, customerid);
            }
            if (status > -1)
            {
                command.AddInputParameter("@Status", DbType.Int32, status);
            }
            if (uploadstatus > -1)
            {
                command.AddInputParameter("@UploadStatus", DbType.Int32, uploadstatus);
            }
            if (orderstatus > -1)
            {
                command.AddInputParameter("@OrderStatus", DbType.Int32, orderstatus);
            }
            if (!string.IsNullOrEmpty(ordertype))
            {
                command.AddInputParameter("@ordertype", DbType.String, ordertype);
            }
            //if (!string.IsNullOrEmpty(orderno))
            //{
            //    command.AddInputParameter("@orderno", DbType.String, orderno);
            //}
            if (!string.IsNullOrEmpty(begindate) && !string.IsNullOrEmpty(enddate))
            {
                command.AddInputParameter("@begindate", DbType.String, begindate);
                command.AddInputParameter("@enddate", DbType.String, enddate);
            }
            if (operatorid > -1)
            {
                command.AddInputParameter("@OperatorID", DbType.Int32, operatorid);
            }
            if (!string.IsNullOrEmpty(ordersource))
            {
                command.AddInputParameter("@OrderSource", DbType.String, ordersource);
            }
            if (!string.IsNullOrEmpty(subOrderType))
            {
                command.AddInputParameter("@SubOrderType", DbType.String, subOrderType);
            }

            if (!string.IsNullOrEmpty(OrderOutStatus))
            {
                command.AddInputParameter("@OrderOutStatus", DbType.String, OrderOutStatus);
            }
            feeInfo = command.ExecuteEntity<OrderFeeInfo>();
            return feeInfo;
            //var o = command.ExecuteScalar<object>();
            //return Convert.ToInt32(o);
        }

        #endregion
    }
}
