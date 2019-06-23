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
    /// <summary>
    /// 库位管理
    /// </summary>
    public class StorageLocationRepository : DataAccessBase
    {
        public List<StorageLocationInfo> GetAllStorageLocation()
        {
            List<StorageLocationInfo> result = new List<StorageLocationInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(StorageLocationStatement.GetAllStorageLocation, "Text"));
            result = command.ExecuteEntityList<StorageLocationInfo>();
            return result;
        }

        public List<StorageLocationInfo> GetStorageLocationByKeys(string keys)
        {
            List<StorageLocationInfo> result = new List<StorageLocationInfo>();
            if (!string.IsNullOrEmpty(keys))
            {
                string sqlText = StorageLocationStatement.GetStorageLocationByKeys;
                sqlText = sqlText.Replace("#ids#", keys);
                DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
                result = command.ExecuteEntityList<StorageLocationInfo>();
            }

            return result;
        }

        public List<StorageLocationInfo> GetStorageLocationByRule(string name, int storageID, int status)
        {
            List<StorageLocationInfo> result = new List<StorageLocationInfo>();
            string sqlText = StorageLocationStatement.GetAllStorageLocationByRule;
            if (!string.IsNullOrEmpty(name))
            {
                sqlText += " AND StorageLocationName LIKE '%'+@key+'%'";
            }
            if (storageID > 0)
            {
                sqlText += " AND storageID=@storageID";
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
            if (storageID > 0)
            {
                command.AddInputParameter("@storageID", DbType.Int32, storageID);
            }
            if (status > -1)
            {
                command.AddInputParameter("@Status", DbType.Int32, status);
            }

            result = command.ExecuteEntityList<StorageLocationInfo>();
            return result;
        }

        public StorageLocationInfo GetStorageLocationByKey(long gid)
        {
            StorageLocationInfo result = new StorageLocationInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(StorageLocationStatement.GetStorageLocationByKey, "Text"));
            command.AddInputParameter("@StorageLocationID", DbType.String, gid);
            result = command.ExecuteEntity<StorageLocationInfo>();
            return result;
        }

        public long CreateNew(StorageLocationInfo StorageLocation)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(StorageLocationStatement.CreateNew, "Text"));
            command.AddInputParameter("@StorageID", DbType.String, StorageLocation.StorageID);
            command.AddInputParameter("@StorageAreaNo", DbType.String, StorageLocation.StorageAreaNo);
            command.AddInputParameter("@StorageSubAreaNo", DbType.String, StorageLocation.StorageSubAreaNo);
            command.AddInputParameter("@StorageLocationNo", DbType.String, StorageLocation.StorageLocationNo);
            command.AddInputParameter("@Remark", DbType.String, StorageLocation.Remark);
            command.AddInputParameter("@IsLock", DbType.String, string.IsNullOrEmpty(StorageLocation.IsLock) ? "F" : StorageLocation.IsLock);
            command.AddInputParameter("@Status", DbType.String, StorageLocation.Status);
            command.AddInputParameter("@OperatorID", DbType.String, StorageLocation.OperatorID);
            command.AddInputParameter("@CreateDate", DbType.DateTime, StorageLocation.CreateDate);
            command.AddInputParameter("@ChangeDate", DbType.DateTime, StorageLocation.ChangeDate);

            var o = command.ExecuteScalar<object>();
            return Convert.ToInt64(o);
        }

        public int ModifyStorageLocation(StorageLocationInfo StorageLocation)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(StorageLocationStatement.Modify, "Text"));
            command.AddInputParameter("@StorageID", DbType.String, StorageLocation.StorageID);
            command.AddInputParameter("@StorageAreaNo", DbType.String, StorageLocation.StorageAreaNo);
            command.AddInputParameter("@StorageLocationNo", DbType.String, StorageLocation.StorageLocationNo);
            command.AddInputParameter("@IsLock", DbType.String, string.IsNullOrEmpty(StorageLocation.IsLock) ? "F" : StorageLocation.IsLock);
            command.AddInputParameter("@Remark", DbType.String, StorageLocation.Remark);
            command.AddInputParameter("@Status", DbType.String, StorageLocation.Status);
            command.AddInputParameter("@OperatorID", DbType.String, StorageLocation.OperatorID);
            command.AddInputParameter("@ChangeDate", DbType.DateTime, StorageLocation.ChangeDate);
            command.AddInputParameter("@StorageLocationID", DbType.Int64, StorageLocation.StorageLocationID);
            return command.ExecuteNonQuery();
        }

        public int Remove(long StorageLocationID)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(StorageLocationStatement.Remove, "Text"));
            command.AddInputParameter("@StorageLocationID", DbType.Int64, StorageLocationID);
            int result = command.ExecuteNonQuery();
            return result;
        }


        #region 分页相关
        public List<StorageLocationInfo> GetAllStorageLocationInfoPager(PagerInfo pager)
        {
            List<StorageLocationInfo> result = new List<StorageLocationInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(StorageLocationStatement.GetAllStorageLocationInfoPager, "Text"));
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);
            result = command.ExecuteEntityList<StorageLocationInfo>();
            return result;
        }


        public List<StorageLocationInfo> GetStorageLocationInfoByRule(int StorageID, string storageAreaNo, string storagesubareano, int status, PagerInfo pager)
        {
            List<StorageLocationInfo> result = new List<StorageLocationInfo>();

            StringBuilder builder = new StringBuilder();

            if (StorageID > 0)
            {
                builder.Append(" AND StorageID =@StorageID ");
            }
            if (!string.IsNullOrEmpty(storageAreaNo))
            {
                builder.Append(" AND storageAreaNo =@storageAreaNo ");
            }
            if (!string.IsNullOrEmpty(storagesubareano))
            {
                builder.Append(" AND StorageSubAreaNo =@StorageSubAreaNo ");
            }
            if (status > -1)
            {
                builder.Append(" AND Status=@Status");
            }
            string sqlText = StorageLocationStatement.GetAllStorageLocationInfoByRulePagerHeader + builder.ToString() + StorageLocationStatement.GetAllStorageLocationInfoByRulePagerFooter;

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            if (StorageID > 0)
            {
                command.AddInputParameter("@StorageID", DbType.Int32, StorageID);
            }
            if (!string.IsNullOrEmpty(storageAreaNo))
            {
                command.AddInputParameter("@storageAreaNo", DbType.String, storageAreaNo);
            }
            if (!string.IsNullOrEmpty(storagesubareano))
            {
                command.AddInputParameter("@StorageSubAreaNo", DbType.String, storagesubareano);
            }
            if (status > -1)
            {
                command.AddInputParameter("@Status", DbType.Int32, status);
            }
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);

            result = command.ExecuteEntityList<StorageLocationInfo>();
            return result;
        }

        public int GetStorageLocationCount(int StorageID, string storageAreaNo, string storagesubareano, int status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(StorageLocationStatement.GetCount);
            if (StorageID > 0)
            {
                builder.Append(" AND StorageID =@StorageID ");
            }
            if (!string.IsNullOrEmpty(storageAreaNo))
            {
                builder.Append(" AND storageAreaNo =@storageAreaNo ");
            }
            if (!string.IsNullOrEmpty(storagesubareano))
            {
                builder.Append(" AND StorageSubAreaNo =@StorageSubAreaNo ");
            }
            if (status > -1)
            {
                builder.Append(" AND Status=@Status");
            }

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(builder.ToString(), "Text"));

            if (StorageID > 0)
            {
                command.AddInputParameter("@StorageID", DbType.Int32, StorageID);
            }
            if (!string.IsNullOrEmpty(storageAreaNo))
            {
                command.AddInputParameter("@storageAreaNo", DbType.String, storageAreaNo);
            }
            if (!string.IsNullOrEmpty(storagesubareano))
            {
                command.AddInputParameter("@StorageSubAreaNo", DbType.String, storagesubareano);
            }
            if (status > -1)
            {
                command.AddInputParameter("@Status", DbType.Int32, status);
            }


            var o = command.ExecuteScalar<object>();
            return Convert.ToInt32(o);
        }

        #endregion

        #region 自定义SQL
        public List<StorageLocationInfo> GetAreaNoByStorageID(int storageID, int status)
        {
            List<StorageLocationInfo> result = new List<StorageLocationInfo>();
            string sqlText = StorageLocationStatement.GetAreaNo;
            if (storageID > 0)
            {
                sqlText += " AND storageID=@storageID";
            }
            if (status > -1)
            {
                sqlText += " AND Status=@Status";
            }

            sqlText += " ORDER BY StorageAreaNo";


            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            if (storageID > 0)
            {
                command.AddInputParameter("@storageID", DbType.Int32, storageID);
            }
            if (status > -1)
            {
                command.AddInputParameter("@Status", DbType.Int32, status);
            }

            result = command.ExecuteEntityList<StorageLocationInfo>();
            return result;
        }


        /// <summary>
        /// 获取自动分配库位信息
        /// </summary>
        /// <param name="storageID"></param>
        /// <returns></returns>
        public List<StorageLocationInfo> GetAutoAllocationLocation(int storageID,int count)
        {
            List<StorageLocationInfo> result = new List<StorageLocationInfo>();
            string sqlText = StorageLocationStatement.GetAutoAllocationLocation;
            if (storageID > 0)
            {
                sqlText += " AND storageID=@storageID";
            }

            sqlText += " ORDER BY StorageLocationNo";

            sqlText = string.Format(sqlText, count > 0 ? " TOP " + count + "" : "");

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            if (storageID > 0)
            {
                command.AddInputParameter("@storageID", DbType.Int32, storageID);
            }

            result = command.ExecuteEntityList<StorageLocationInfo>();
            return result;
        }

        /// <summary>
        /// 更新库位状态(是否锁定T/F)
        /// </summary>
        /// <param name="StorageLocationID"></param>
        /// <returns></returns>
        public int ModifyLock(long StorageLocationID, string isLock)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(StorageLocationStatement.ModifyLock, "Text"));
            command.AddInputParameter("@StorageLocationID", DbType.Int64, StorageLocationID);
            command.AddInputParameter("@IsLock", DbType.String, isLock);
            int result = command.ExecuteNonQuery();
            return result;
        }


        public List<StorageLocationInfo> GetSubAreaNoByStorageAreaNo(string StorageAreaNo, int status)
        {
            List<StorageLocationInfo> result = new List<StorageLocationInfo>();
            string sqlText = StorageLocationStatement.GetSubAreaNo;
            if (!string.IsNullOrEmpty(StorageAreaNo))
            {
                sqlText += " AND StorageAreaNo=@StorageAreaNo";
            }
            if (status > -1)
            {
                sqlText += " AND Status=@Status";
            }

            sqlText += " ORDER BY StorageSubAreaNo";

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            if (!string.IsNullOrEmpty(StorageAreaNo))
            {
                command.AddInputParameter("@StorageAreaNo", DbType.String, StorageAreaNo);
            }
            if (status > -1)
            {
                command.AddInputParameter("@Status", DbType.Int32, status);
            }

            result = command.ExecuteEntityList<StorageLocationInfo>();
            return result;
        }
        #endregion
    }
}
