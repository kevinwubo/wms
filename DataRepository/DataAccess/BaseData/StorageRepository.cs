using Common;
using DataRepository.DataAccess.Storage;
using DataRepository.DataModel;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Storage
{
    public class StorageRepository : DataAccessBase
    {
        public List<StorageInfo> GetAllstorage()
        {
            List<StorageInfo> result = new List<StorageInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(StorageStatement.GetAllStorage, "Text"));
            result = command.ExecuteEntityList<StorageInfo>();
            return result;
        }

        public List<StorageInfo> GetStorageByKeys(string keys)
        {
            List<StorageInfo> result = new List<StorageInfo>();
            if (!string.IsNullOrEmpty(keys))
            {
                string sqlText = StorageStatement.GetStorageByKey;
                sqlText = sqlText.Replace("#ids#", keys);
                DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
                result = command.ExecuteEntityList<StorageInfo>();
            }

            return result;
        }

        public List<StorageInfo> GetStorageByRule(string name, int status)
        {
            List<StorageInfo> result = new List<StorageInfo>();
            string sqlText = StorageStatement.GetAllStorageByRule;
            if (!string.IsNullOrEmpty(name))
            {
                sqlText += " AND storageName LIKE '%'+@key+'%'";
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

            result = command.ExecuteEntityList<StorageInfo>();
            return result;
        }

        public StorageInfo GetStorageByKey(long gid)
        {
            StorageInfo result = new StorageInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(StorageStatement.GetStorageByKey, "Text"));
            command.AddInputParameter("@StorageID", DbType.String, gid);
            result = command.ExecuteEntity<StorageInfo>();
            return result;
        }

        public long CreateNew(StorageInfo storage)
        {            
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(StorageStatement.CreateNewStorage, "Text"));            
            command.AddInputParameter("@StorageName", DbType.String, storage.StorageName);
            command.AddInputParameter("@StorageNo", DbType.String, storage.StorageNo);
            command.AddInputParameter("@ProvinceID", DbType.Int32, storage.ProvinceID);
            command.AddInputParameter("@CityID", DbType.Int32, storage.CityID);
            command.AddInputParameter("@Address", DbType.String, storage.Address);
            command.AddInputParameter("@Status", DbType.Int32, storage.Status);
            command.AddInputParameter("@OperatorID", DbType.Int64, storage.OperatorID);
            command.AddInputParameter("@CreateDate", DbType.DateTime, storage.CreateDate);
            command.AddInputParameter("@ChangeDate", DbType.DateTime, storage.ChangeDate);
            var o = command.ExecuteScalar<object>();
            return Convert.ToInt64(o);
        }

        public int ModifyStorage(StorageInfo storage)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(StorageStatement.ModifyStorage, "Text"));
            command.AddInputParameter("@StorageName", DbType.String, storage.StorageName);
            command.AddInputParameter("@StorageNo", DbType.String, storage.StorageNo);
            command.AddInputParameter("@ProvinceID", DbType.Int32, storage.ProvinceID);
            command.AddInputParameter("@CityID", DbType.Int32, storage.CityID);
            command.AddInputParameter("@Address", DbType.String, storage.Address);
            command.AddInputParameter("@Status", DbType.Int32, storage.Status);
            command.AddInputParameter("@OperatorID", DbType.Int64, storage.OperatorID);            
            command.AddInputParameter("@ChangeDate", DbType.DateTime, storage.ChangeDate);
            command.AddInputParameter("@StorageID", DbType.Int32, storage.StorageID);
            return command.ExecuteNonQuery();
        }

        public int RemoveStorage(long StorageID)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(StorageStatement.Remove, "Text"));
            command.AddInputParameter("@StorageID", DbType.Int64, StorageID);
            int result = command.ExecuteNonQuery();
            return result;
        }


        #region 分页相关
        public List<StorageInfo> GetAllStorageInfoPager(PagerInfo pager)
        {
            List<StorageInfo> result = new List<StorageInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(StorageStatement.GetAllStorageInfoPager, "Text"));
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);
            result = command.ExecuteEntityList<StorageInfo>();
            return result;
        }


        public List<StorageInfo> GetStorageInfoByRule(string name, string modelCode, int status, PagerInfo pager)
        {
            List<StorageInfo> result = new List<StorageInfo>();

            StringBuilder builder = new StringBuilder();

            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" AND (StorageName LIKE '%'+@key+'%' OR StorageNo LIKE '%'+@key+'%')");
            }
            if (status > -1)
            {
                builder.Append(" AND Status=@Status");
            }
            string sqlText = StorageStatement.GetAllStorageInfoByRulePagerHeader + builder.ToString() + StorageStatement.GetAllStorageInfoByRulePagerFooter;

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            if (!string.IsNullOrEmpty(name))
            {
                command.AddInputParameter("@key", DbType.String, name);
            }
            if (status > -1)
            {
                command.AddInputParameter("@Status", DbType.Int32, status);
            }
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);

            result = command.ExecuteEntityList<StorageInfo>();
            return result;
        }

        public int GetStorageCount(string name, string modelCode, int status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(StorageStatement.GetCount);
            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" AND (StorageName LIKE '%'+@key+'%' OR StorageNo LIKE '%'+@key+'%')");
            }
            if (status > -1)
            {
                builder.Append(" AND Status=@Status");
            }

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(builder.ToString(), "Text"));

            if (!string.IsNullOrEmpty(name))
            {
                command.AddInputParameter("@key", DbType.String, name);
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
