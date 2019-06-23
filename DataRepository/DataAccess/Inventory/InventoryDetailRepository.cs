using Common;
using DataRepository.DataModel;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.BaseData
{
    public class InventoryDetailRepository : DataAccessBase
    {
        public List<InventoryDetailInfo> GetAllInventoryDetail()
        {
            List<InventoryDetailInfo> result = new List<InventoryDetailInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(InventoryDetailStatement.GetAllInventoryDetail, "Text"));
            result = command.ExecuteEntityList<InventoryDetailInfo>();
            return result;
        }

        public List<InventoryDetailInfo> GetInventoryDetailByKeys(string keys)
        {
            List<InventoryDetailInfo> result = new List<InventoryDetailInfo>();
            if (!string.IsNullOrEmpty(keys))
            {
                string sqlText = InventoryDetailStatement.GetInventoryDetailByKeys;
                sqlText = sqlText.Replace("#ids#", keys);
                DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
                result = command.ExecuteEntityList<InventoryDetailInfo>();
            }

            return result;
        }

        public InventoryDetailInfo GetInventoryDetailByKey(long gid)
        {
            InventoryDetailInfo result = new InventoryDetailInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(InventoryDetailStatement.GetInventoryDetailByKey, "Text"));
            command.AddInputParameter("@InventoryDetailID", DbType.String, gid);
            result = command.ExecuteEntity<InventoryDetailInfo>();
            return result;
        }

        public long CreateNew(InventoryDetailInfo InventoryDetail)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(InventoryDetailStatement.CreateNewInventoryDetail, "Text"));
            command.AddInputParameter("@GoodsID", DbType.Int32, InventoryDetail.GoodsID);
            command.AddInputParameter("@StorageID", DbType.Int32, InventoryDetail.StorageID);
            command.AddInputParameter("@Quantity", DbType.Int32, InventoryDetail.Quantity);
            command.AddInputParameter("@CustomerID", DbType.Int32, InventoryDetail.CustomerID);
            command.AddInputParameter("@InventoryType", DbType.String, InventoryDetail.InventoryType);
            command.AddInputParameter("@BatchNumber", DbType.String, InventoryDetail.BatchNumber);
            command.AddInputParameter("@ProductDate", DbType.DateTime, InventoryDetail.ProductDate);
            command.AddInputParameter("@InventoryDate", DbType.DateTime, InventoryDetail.InventoryDate);
            command.AddInputParameter("@UnitPrice", DbType.Decimal, InventoryDetail.UnitPrice);
            command.AddInputParameter("@Remark", DbType.String, InventoryDetail.Remark);
            command.AddInputParameter("@OrderType", DbType.String, InventoryDetail.OrderType);
            command.AddInputParameter("@OrderNo", DbType.String, InventoryDetail.OrderNo);
            command.AddInputParameter("@OperatorID", DbType.String, InventoryDetail.OperatorID);
            command.AddInputParameter("@CreateDate", DbType.DateTime, InventoryDetail.CreateDate);
            command.AddInputParameter("@ChangeDate", DbType.DateTime, InventoryDetail.ChangeDate);
            var o = command.ExecuteScalar<object>();
            return Convert.ToInt64(o);
        }       



        #region 分页相关
        public List<InventoryDetailInfo> GetAllInventoryDetailInfoPager(PagerInfo pager)
        {
            List<InventoryDetailInfo> result = new List<InventoryDetailInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(InventoryDetailStatement.GetAllInventoryDetailInfoPager, "Text"));
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);
            result = command.ExecuteEntityList<InventoryDetailInfo>();
            return result;
        }


        public List<InventoryDetailInfo> GetInventoryDetailInfoByRule(string name, string inventoryType, int StorageID, int customerID, string inventoryDate, string orderType, PagerInfo pager)
        {
            List<InventoryDetailInfo> result = new List<InventoryDetailInfo>();

            StringBuilder builder = new StringBuilder();

            if (!string.IsNullOrEmpty(name))
            {

            }
            if (!string.IsNullOrEmpty(inventoryType))
            {
                builder.Append(" AND inventoryType=@inventoryType");
            }
            if (StorageID > 0)
            {
                builder.Append(" AND StorageID=@StorageID");
            }
            if (customerID > 0)
            {
                builder.Append(" AND customerID=@customerID");
            }
            if (!string.IsNullOrEmpty(orderType))
            {
                builder.Append(" AND orderType=@orderType");
            }

            string sqlText = InventoryDetailStatement.GetAllInventoryDetailInfoByRulePagerHeader + builder.ToString() + InventoryDetailStatement.GetAllInventoryDetailInfoByRulePagerFooter;

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            if (!string.IsNullOrEmpty(name))
            {

            }
            if (!string.IsNullOrEmpty(inventoryType))
            {
                command.AddInputParameter("@inventoryType", DbType.String, inventoryType);
            }
            if (StorageID > 0)
            {
                command.AddInputParameter("@StorageID", DbType.Int32, StorageID);
            }
            if (customerID > 0)
            {
                command.AddInputParameter("@customerID", DbType.Int32, customerID);
            }
            if (!string.IsNullOrEmpty(inventoryDate))
            {

            }
            if (!string.IsNullOrEmpty(orderType))
            {
                command.AddInputParameter("@orderType", DbType.String, orderType);
            }
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);

            result = command.ExecuteEntityList<InventoryDetailInfo>();
            return result;
        }

        public int GetInventoryDetailCount(string name, string inventoryType, int StorageID, int customerID, string inventoryDate, string orderType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(InventoryDetailStatement.GetCount);
            if (!string.IsNullOrEmpty(name))
            {
                
            }
            if (!string.IsNullOrEmpty(inventoryType))
            {
                builder.Append(" AND inventoryType=@inventoryType");
            }
            if (StorageID > 0)
            {
                builder.Append(" AND StorageID=@StorageID");
            }
            if (customerID > 0)
            {
                builder.Append(" AND customerID=@customerID");
            }
            if (!string.IsNullOrEmpty(orderType))
            {
                builder.Append(" AND orderType=@orderType");
            }

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(builder.ToString(), "Text"));

            if (!string.IsNullOrEmpty(name))
            {

            }
            if (!string.IsNullOrEmpty(inventoryType))
            {
                command.AddInputParameter("@inventoryType", DbType.String, inventoryType);
            }
            if (StorageID > 0)
            {
                command.AddInputParameter("@StorageID", DbType.Int32, StorageID);
            }
            if (customerID > 0)
            {
                command.AddInputParameter("@customerID", DbType.Int32, customerID);
            }
            if (!string.IsNullOrEmpty(inventoryDate))
            {

            }
            if (!string.IsNullOrEmpty(orderType))
            {
                command.AddInputParameter("@orderType", DbType.String, orderType);
            }

            var o = command.ExecuteScalar<object>();
            return Convert.ToInt32(o);
        }

        #endregion
    }
}
