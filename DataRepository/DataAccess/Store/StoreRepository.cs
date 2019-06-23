﻿using Common;
using DataRepository.DataModel;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Store
{
    public class StoreRepository : DataAccessBase
    {
        public List<StoreInfo> GetAllStore()
        {
            List<StoreInfo> result = new List<StoreInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(StoreStatement.GetAllStores, "Text"));
            result = command.ExecuteEntityList<StoreInfo>();
            return result;
        }

        public List<StoreInfo> GetAllStore(string cityid)
        {
            List<StoreInfo> result = GetAllStore().Where(t => t.CityID == cityid.ToInt(0)).ToList();
            return result;
        }

        public List<StoreInfo> GetAllStorePager(PagerInfo pager)
        {
            List<StoreInfo> result = new List<StoreInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(StoreStatement.GetAllStorePager, "Text"));
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);
            result = command.ExecuteEntityList<StoreInfo>();
            return result;
        }

        public List<StoreInfo> GetStoresByRule(string name, int status,PagerInfo pager)
        {
            List<StoreInfo> result = new List<StoreInfo>();
            StringBuilder builder = new StringBuilder();

            
            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" AND SupplierName LIKE '%'+@key+'%'");
            }
            if (status > -1)
            {
                builder.Append(" AND Status=@Status");
            }

            string sqlText = StoreStatement.GetAllStoreInfoByRulePagerHeader + builder.ToString() + StoreStatement.GetAllStoreInfoByRulePagerFooter;

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

            result = command.ExecuteEntityList<StoreInfo>();
            return result;
        }

        public int GetStoreCount(string name, int status)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(StoreStatement.GetStoreCount);
            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" AND SupplierName LIKE '%'+@key+'%'");
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

        public StoreInfo GetStoreByKey(long sid)
        {
            StoreInfo result = new StoreInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(StoreStatement.GetStoreByKey, "Text"));
            command.AddInputParameter("@SupplierID", DbType.Int32, sid);
            result = command.ExecuteEntity<StoreInfo>();
            return result;
        }

        public int CreateNew(StoreInfo store)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(StoreStatement.CreateNewStore, "Text"));
            command.AddInputParameter("@SupplierName", DbType.String, store.SupplierName);
            command.AddInputParameter("@SupplierCode", DbType.String, store.SupplierCode);
            command.AddInputParameter("@SupplierType", DbType.Int32, store.SupplierType);
            command.AddInputParameter("@CityID", DbType.Int32, store.CityID);
            command.AddInputParameter("@Address", DbType.String, store.Address);
            command.AddInputParameter("@Telephone", DbType.String, store.Telephone);
            command.AddInputParameter("@Mobile", DbType.String, store.Mobile);
            command.AddInputParameter("@StartTime", DbType.String, store.StartTime);
            command.AddInputParameter("@EndTime", DbType.String, store.EndTime);
            command.AddInputParameter("@Coordinate", DbType.String, store.Coordinate);
            command.AddInputParameter("@Status", DbType.Int32, store.Status);
            command.AddInputParameter("@AttachmentIDs", DbType.String, store.AttachmentIDs);
            command.AddInputParameter("@CreateDate", DbType.DateTime, store.CreateDate);
            command.AddInputParameter("@ModifyDate", DbType.DateTime, store.ModifyDate);
            command.AddInputParameter("@Operator", DbType.Int64, store.Operator);

            return command.ExecuteNonQuery();
        }

        public int ModifyStore(StoreInfo store)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(StoreStatement.ModifyStore, "Text"));
            command.AddInputParameter("@SupplierID", DbType.Int32, store.SupplierID);
            command.AddInputParameter("@SupplierName", DbType.String, store.SupplierName);
            command.AddInputParameter("@SupplierCode", DbType.String, store.SupplierCode);
            command.AddInputParameter("@SupplierType", DbType.Int32, store.SupplierType);
            command.AddInputParameter("@CityID", DbType.Int32, store.CityID);
            command.AddInputParameter("@Address", DbType.String, store.Address);
            command.AddInputParameter("@Telephone", DbType.String, store.Telephone);
            command.AddInputParameter("@Mobile", DbType.String, store.Mobile);
            command.AddInputParameter("@StartTime", DbType.String, store.StartTime);
            command.AddInputParameter("@EndTime", DbType.String, store.EndTime);
            command.AddInputParameter("@Coordinate", DbType.String, store.Coordinate);
            command.AddInputParameter("@Status", DbType.Int32, store.Status);
            command.AddInputParameter("@AttachmentIDs", DbType.String, store.AttachmentIDs);
            command.AddInputParameter("@ModifyDate", DbType.DateTime, store.ModifyDate);
            command.AddInputParameter("@Operator", DbType.Int64, store.Operator);

            return command.ExecuteNonQuery();
        }

        public int RemoveStore(int sid)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(StoreStatement.Remove, "Text"));
            command.AddInputParameter("@SupplierID", DbType.Int32, sid);
            int result = command.ExecuteNonQuery();
            return result;
        }

    }
}
