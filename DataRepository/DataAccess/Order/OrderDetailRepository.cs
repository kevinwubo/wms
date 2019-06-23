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
    public class OrderDetailRepository : DataAccessBase
    {

        public List<OrderDetailInfo> GetAllOrderDetail()
        {
            List<OrderDetailInfo> result = new List<OrderDetailInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderDetailStatement.GetAllOrderDetail, "Text"));
            result = command.ExecuteEntityList<OrderDetailInfo>();
            return result;
        }

        public List<OrderDetailInfo> GetOrderDetailByKeys(string keys)
        {
            List<OrderDetailInfo> result = new List<OrderDetailInfo>();
            if (!string.IsNullOrEmpty(keys))
            {
                string sqlText = OrderDetailStatement.GetOrderDetailByKeys;
                sqlText = sqlText.Replace("#ids#", keys);
                DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
                result = command.ExecuteEntityList<OrderDetailInfo>();
            }

            return result;
        }


        public List<OrderDetailInfo> GetAllOrderDetailByOrderID(int orderID)
        {
            List<OrderDetailInfo> result = new List<OrderDetailInfo>();
            string sqlText = OrderDetailStatement.GetAllOrderDetailByOrderID;
            if (orderID > -1)
            {
                sqlText += " AND orderID=@orderID";
            }


            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            if (orderID > -1)
            {
                command.AddInputParameter("@orderID", DbType.Int32, orderID);
            }

            result = command.ExecuteEntityList<OrderDetailInfo>();
            return result;
        }

        public List<OrderDetailInfo> GetOrderDetailByRule(string name, int status)
        {
            List<OrderDetailInfo> result = new List<OrderDetailInfo>();
            string sqlText = OrderDetailStatement.GetAllOrderDetailByRule;
            if (!string.IsNullOrEmpty(name))
            {
                sqlText += " AND OrderDetailName LIKE '%'+@key+'%'";
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

            result = command.ExecuteEntityList<OrderDetailInfo>();
            return result;
        }

        public OrderDetailInfo GetOrderDetailByKey(long gid)
        {
            OrderDetailInfo result = new OrderDetailInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderDetailStatement.GetOrderDetailByKey, "Text"));
            command.AddInputParameter("@OrderDetailID", DbType.String, gid);
            result = command.ExecuteEntity<OrderDetailInfo>();
            return result;
        }

        public long CreateNew(OrderDetailInfo OrderDetail)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderDetailStatement.CreateNewOrderDetail, "Text"));
            command.AddInputParameter("@OrderID", DbType.Int32, OrderDetail.OrderID);
            command.AddInputParameter("@GoodsID", DbType.Int32, OrderDetail.GoodsID);
            command.AddInputParameter("@InventoryID", DbType.Int32, OrderDetail.InventoryID);
            command.AddInputParameter("@GoodsNo", DbType.String, OrderDetail.GoodsNo);
            command.AddInputParameter("@GoodsName", DbType.String, OrderDetail.GoodsName);
            command.AddInputParameter("@GoodsModel", DbType.String, OrderDetail.GoodsModel);
            command.AddInputParameter("@Quantity", DbType.Int32, OrderDetail.Quantity);
            command.AddInputParameter("@Units", DbType.String, OrderDetail.Units);
            command.AddInputParameter("@Weight", DbType.String, OrderDetail.Weight);
            command.AddInputParameter("@TotalWeight", DbType.String, OrderDetail.TotalWeight);
            command.AddInputParameter("@BatchNumber", DbType.String, OrderDetail.BatchNumber);
            command.AddInputParameter("@ProductDate", DbType.DateTime, OrderDetail.ProductDate);
            command.AddInputParameter("@ExceedDate", DbType.DateTime, OrderDetail.ExceedDate);

            command.AddInputParameter("@CreateDate", DbType.DateTime, OrderDetail.CreateDate);
            command.AddInputParameter("@ChangeDate", DbType.DateTime, OrderDetail.ChangeDate);

            var o = command.ExecuteScalar<object>();
            return Convert.ToInt64(o);
        }

        public int ModifyOrderDetail(OrderDetailInfo OrderDetail)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderDetailStatement.ModifyOrderDetail, "Text"));
            command.AddInputParameter("@OrderID", DbType.Int32, OrderDetail.OrderID);
            command.AddInputParameter("@GoodsID", DbType.Int32, OrderDetail.GoodsID);
            command.AddInputParameter("@InventoryID", DbType.Int32, OrderDetail.InventoryID);
            command.AddInputParameter("@GoodsNo", DbType.String, OrderDetail.GoodsNo);
            command.AddInputParameter("@GoodsName", DbType.String, OrderDetail.GoodsName);
            command.AddInputParameter("@GoodsModel", DbType.String, OrderDetail.GoodsModel);
            command.AddInputParameter("@Quantity", DbType.Int32, OrderDetail.Quantity);
            command.AddInputParameter("@Units", DbType.String, OrderDetail.Units);
            command.AddInputParameter("@Weight", DbType.String, OrderDetail.Weight);
            command.AddInputParameter("@TotalWeight", DbType.String, OrderDetail.TotalWeight);
            command.AddInputParameter("@BatchNumber", DbType.String, OrderDetail.BatchNumber);
            command.AddInputParameter("@ProductDate", DbType.DateTime, OrderDetail.ProductDate);
            command.AddInputParameter("@ExceedDate", DbType.DateTime, OrderDetail.ExceedDate);

            command.AddInputParameter("@ChangeDate", DbType.DateTime, OrderDetail.ChangeDate);
            command.AddInputParameter("@ID", DbType.Int32, OrderDetail.ID);
            return command.ExecuteNonQuery();
        }

        public int Delete(long OrderDetailID)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderDetailStatement.Delete, "Text"));
            command.AddInputParameter("@ID", DbType.Int64, OrderDetailID);
            int result = command.ExecuteNonQuery();
            return result;
        }


        #region 分页相关
        public List<OrderDetailInfo> GetAllOrderDetailInfoPager(PagerInfo pager)
        {
            List<OrderDetailInfo> result = new List<OrderDetailInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderDetailStatement.GetAllOrderDetailInfoPager, "Text"));
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);
            result = command.ExecuteEntityList<OrderDetailInfo>();
            return result;
        }


        public List<OrderDetailInfo> GetOrderDetailInfoByRule(string name, int carrierid, int storageid, int customerid, int status, PagerInfo pager)
        {
            List<OrderDetailInfo> result = new List<OrderDetailInfo>();

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
            string sqlText = OrderDetailStatement.GetAllOrderDetailInfoByRulePagerHeader + builder.ToString() + OrderDetailStatement.GetAllOrderDetailInfoByRulePagerFooter;

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
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);

            result = command.ExecuteEntityList<OrderDetailInfo>();
            return result;
        }

        public int GetOrderDetailCount(string name, int carrierid, int storageid, int customerid, int status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(OrderDetailStatement.GetCount);
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


            var o = command.ExecuteScalar<object>();
            return Convert.ToInt32(o);
        }

        #endregion
    }
}
