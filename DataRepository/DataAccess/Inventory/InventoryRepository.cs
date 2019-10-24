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
    public class InventoryRepository : DataAccessBase
    {
        public List<InventoryInfo> GetAllInventory()
        {
            List<InventoryInfo> result = new List<InventoryInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(InventoryStatement.GetAllInventory, "Text"));
            result = command.ExecuteEntityList<InventoryInfo>();
            return result;
        }

        public List<InventoryInfo> GetInventoryByKeys(string keys)
        {
            List<InventoryInfo> result = new List<InventoryInfo>();
            if (!string.IsNullOrEmpty(keys))
            {
                string sqlText = InventoryStatement.GetInventoryByKeys;
                sqlText = sqlText.Replace("#ids#", keys);
                DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
                result = command.ExecuteEntityList<InventoryInfo>();
            }

            return result;
        }

        /// <summary>
        /// 相同产品ID、相同批次号
        /// </summary>
        /// <param name="goodsid"></param>
        /// <param name="storageID"></param>
        /// <param name="batchNumber"></param>
        /// <param name="DY0">是否查询大于0</param>
        /// <returns></returns>
        public List<InventoryInfo> GetInventoryByRule(int goodsid, int storageID, string batchNumber, int CustomerID,bool DY0)
        {
            List<InventoryInfo> result = new List<InventoryInfo>();
            string sqlText = InventoryStatement.GetAllInventoryByRule;
            if (goodsid > 0)
            {
                sqlText += " AND GoodsID=@GoodSID";
            }
            if (storageID > 0)
            {
                sqlText += " AND StorageID=@StorageID";
            }
            if (!string.IsNullOrEmpty(batchNumber))
            {
                sqlText += " AND BatchNumber=@BatchNumber";
            }

            if (CustomerID > 0)
            {
                sqlText += " AND CustomerID=@CustomerID";
            }

            // 只查询大于0的库存数据
            if (DY0)
            {
                sqlText += " AND Quantity>0";
            }

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));

            if (goodsid > 0)
            {
                command.AddInputParameter("@GoodSID", DbType.String, goodsid);
            }
            if (storageID > 0)
            {
                command.AddInputParameter("@StorageID", DbType.String, storageID);
            }
            if (!string.IsNullOrEmpty(batchNumber))
            {
                command.AddInputParameter("@BatchNumber", DbType.String, batchNumber);
            }

            if (CustomerID > 0)
            {
                command.AddInputParameter("@CustomerID", DbType.String, CustomerID);
            }

            result = command.ExecuteEntityList<InventoryInfo>();
            return result;
        }

        public InventoryInfo GetInventoryByKey(long gid)
        {
            InventoryInfo result = new InventoryInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(InventoryStatement.GetInventoryByKey, "Text"));
            command.AddInputParameter("@InventoryID", DbType.String, gid);
            result = command.ExecuteEntity<InventoryInfo>();
            return result;
        }

        public long CreateNew(InventoryInfo Inventory)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(InventoryStatement.CreateNewInventory, "Text"));
            command.AddInputParameter("@GoodsID", DbType.Int32, Inventory.GoodsID);
            command.AddInputParameter("@StorageID", DbType.Int32, Inventory.StorageID);
            command.AddInputParameter("@Quantity", DbType.Int32, Inventory.Quantity);
            command.AddInputParameter("@CustomerID", DbType.Int32, Inventory.CustomerID);
            command.AddInputParameter("@InventoryType", DbType.String, Inventory.InventoryType);
            command.AddInputParameter("@BatchNumber", DbType.String, Inventory.BatchNumber);
            command.AddInputParameter("@ProductDate", DbType.DateTime, Inventory.ProductDate);
            command.AddInputParameter("@InventoryDate", DbType.DateTime, Inventory.InventoryDate);
            command.AddInputParameter("@UnitPrice", DbType.Decimal, Inventory.UnitPrice);
            command.AddInputParameter("@Remark", DbType.String, Inventory.Remark);
            command.AddInputParameter("@OperatorID", DbType.String, Inventory.OperatorID);
            command.AddInputParameter("@CreateDate", DbType.DateTime, Inventory.CreateDate);
            command.AddInputParameter("@ChangeDate", DbType.DateTime, Inventory.ChangeDate);
            var o = command.ExecuteScalar<object>();
            return Convert.ToInt64(o);
        }

        public int ModifyInventoryQuantity(InventoryInfo Inventory)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(InventoryStatement.ModifyInventoryQuantity, "Text"));            
            command.AddInputParameter("@Quantity", DbType.Int32, Inventory.Quantity);
            command.AddInputParameter("@ChangeDate", DbType.DateTime, Inventory.ChangeDate);
            command.AddInputParameter("@InventoryID", DbType.Int64, Inventory.InventoryID);
            return command.ExecuteNonQuery();
        }

        
        /// <summary>
        /// 库存盘点 更改库存状态 锁定、盘点中
        /// </summary>
        /// <param name="Inventory"></param>
        /// <returns></returns>
        public int ModifyInventoryStatus(InventoryInfo Inventory)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(InventoryStatement.ModifyInventoryStatus, "Text"));
            command.AddInputParameter("@IsLock", DbType.String, Inventory.IsLock);
            command.AddInputParameter("@InventoryStatus", DbType.String, Inventory.InventoryStatus);
            command.AddInputParameter("@ChangeDate", DbType.DateTime, Inventory.ChangeDate);
            command.AddInputParameter("@InventoryID", DbType.Int64, Inventory.InventoryID);
            return command.ExecuteNonQuery();
        }

        public int ModifyInventory(InventoryInfo Inventory)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(InventoryStatement.ModifyInventory, "Text"));
            command.AddInputParameter("@GoodsID", DbType.Int32, Inventory.GoodsID);
            command.AddInputParameter("@StorageID", DbType.Int32, Inventory.StorageID);
            command.AddInputParameter("@Quantity", DbType.Int32, Inventory.Quantity);
            command.AddInputParameter("@CustomerID", DbType.Int32, Inventory.CustomerID);
            command.AddInputParameter("@InventoryType", DbType.String, Inventory.InventoryType);
            command.AddInputParameter("@BatchNumber", DbType.String, Inventory.BatchNumber);
            command.AddInputParameter("@ProductDate", DbType.DateTime, Inventory.ProductDate);
            command.AddInputParameter("@InventoryDate", DbType.DateTime, Inventory.InventoryDate);
            command.AddInputParameter("@UnitPrice", DbType.Decimal, Inventory.UnitPrice);
            command.AddInputParameter("@Remark", DbType.String, Inventory.Remark);
            command.AddInputParameter("@OperatorID", DbType.String, Inventory.OperatorID);
            command.AddInputParameter("@ChangeDate", DbType.DateTime, Inventory.ChangeDate);
            command.AddInputParameter("@InventoryID", DbType.Int64, Inventory.InventoryID);
            return command.ExecuteNonQuery();
        }

        public int Remove(long InventoryID)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(InventoryStatement.Delete, "Text"));
            command.AddInputParameter("@InventoryID", DbType.Int64, InventoryID);
            int result = command.ExecuteNonQuery();
            return result;
        }


        #region 分页相关
        public List<InventoryInfo> GetAllInventoryInfoPager(PagerInfo pager)
        {
            List<InventoryInfo> result = new List<InventoryInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(InventoryStatement.GetAllInventoryInfoPager, "Text"));
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);
            result = command.ExecuteEntityList<InventoryInfo>();
            return result;
        }


        /// <summary>
        /// 商品名称
        /// </summary>
        /// <param name="name"></param>
        /// <param name="batchNumber"></param>
        /// <param name="StorageID"></param>
        /// <param name="customerID"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public List<InventoryInfo> GetInventoryInfoByRule(string name, string batchNumber, int StorageID, int customerID, PagerInfo pager)
        {
            List<InventoryInfo> result = new List<InventoryInfo>();

            StringBuilder builder = new StringBuilder();

            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" and GoodsID in(select GoodsID from wms_GoodsInfo where GoodsName like '%" + name + "%')");
            }
            if (!string.IsNullOrEmpty(batchNumber))
            {
                builder.Append(" AND batchNumber=@batchNumber");
            }
            if (StorageID > 0)
            {
                builder.Append(" AND StorageID=@StorageID");
            }
            if (customerID > 0)
            {
                builder.Append(" AND customerID=@customerID");
            }

            builder.Append(" AND Quantity>0 ");//库存数量必须大于0

            string sqlText = InventoryStatement.GetAllInventoryInfoByRulePagerHeader + builder.ToString() + InventoryStatement.GetAllInventoryInfoByRulePagerFooter;

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            if (!string.IsNullOrEmpty(batchNumber))
            {
                command.AddInputParameter("@batchNumber", DbType.String, batchNumber);
            }
            if (StorageID > 0)
            {
                command.AddInputParameter("@StorageID", DbType.Int32, StorageID);
            }
            if (customerID > 0)
            {
                command.AddInputParameter("@customerID", DbType.Int32, customerID);
            }
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);

            result = command.ExecuteEntityList<InventoryInfo>();
            return result;
        }

        public int GetInventoryCount(string name, string batchNumber, int StorageID, int customerID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(InventoryStatement.GetCount);
            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" and GoodsID in(select GoodsID from wms_GoodsInfo where GoodsName like '%" + name + "%')");
            }
            if (!string.IsNullOrEmpty(batchNumber))
            {
                builder.Append(" AND batchNumber=@batchNumber");
            }
            if (StorageID > 0)
            {
                builder.Append(" AND StorageID=@StorageID");
            }
            if (customerID > 0)
            {
                builder.Append(" AND customerID=@customerID");
            }

            builder.Append(" AND Quantity>0 ");//库存数量必须大于0

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(builder.ToString(), "Text"));

            if (!string.IsNullOrEmpty(batchNumber))
            {
                command.AddInputParameter("@batchNumber", DbType.String, batchNumber);
            }
            if (StorageID > 0)
            {
                command.AddInputParameter("@StorageID", DbType.Int32, StorageID);
            }
            if (customerID > 0)
            {
                command.AddInputParameter("@customerID", DbType.Int32, customerID);
            }


            var o = command.ExecuteScalar<object>();
            return Convert.ToInt32(o);
        }

        #endregion
    }
}
