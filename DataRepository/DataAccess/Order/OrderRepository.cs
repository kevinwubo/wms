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
            command.AddInputParameter("@LineID", DbType.Int32, Order.LineID); 

            command.AddInputParameter("@CreateDate", DbType.DateTime, Order.CreateDate);
            command.AddInputParameter("@ChangeDate", DbType.DateTime, Order.ChangeDate);

            var o = command.ExecuteScalar<object>();
            return Convert.ToInt64(o);
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
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="carrierid">承运商ID</param>
        /// <param name="storageid">仓库ID</param>
        /// <param name="customerid">客户ID</param>
        /// <param name="status">数据状态</param>
        /// <param name="uploadstatus">接单状态</param>
        /// <param name="orderstatus">订单状态</param>
        /// <param name="ordertype">订单类型</param>
        /// <param name="orderno">订单编号</param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public List<OrderInfo> GetOrderInfoByRule(string name, int carrierid, int storageid, int customerid, int status, int uploadstatus,
            int orderstatus, string ordertype, string orderno, string begindate, string enddate, int operatorid, string ordersource, PagerInfo pager)
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
            if (!string.IsNullOrEmpty(orderno))
            {
                builder.Append(" AND orderno=@orderno ");
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

            string sqlText = OrderStatement.GetAllOrderInfoByRulePagerHeader + builder.ToString() + OrderStatement.GetAllOrderInfoByRulePagerFooter;

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
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
            if (!string.IsNullOrEmpty(orderno))
            {
                command.AddInputParameter("@orderno", DbType.String, orderno);
            }
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
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);

            result = command.ExecuteEntityList<OrderInfo>();
            return result;
        }

        public int GetOrderCount(string name, int carrierid, int storageid, int customerid, int status, int uploadstatus,
            int orderstatus, string ordertype, string orderno, string begindate, string enddate, int operatorid, string ordersource)
        {
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
                builder.Append(" AND orderno=@orderno ");
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
            if (!string.IsNullOrEmpty(orderno))
            {
                command.AddInputParameter("@orderno", DbType.String, orderno);
            }
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
            var o = command.ExecuteScalar<object>();
            return Convert.ToInt32(o);
        }

        #endregion
    }
}
