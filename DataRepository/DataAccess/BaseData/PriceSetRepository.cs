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
    public class PriceSetRepository : DataAccessBase
    {
        public List<PriceSetInfo> GetAllPriceSet()
        {
            List<PriceSetInfo> result = new List<PriceSetInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(PriceSetStatement.GetAllPriceSet, "Text"));
            result = command.ExecuteEntityList<PriceSetInfo>();
            return result;
        }

        public List<PriceSetInfo> GetPriceSetByKeys(string keys)
        {
            List<PriceSetInfo> result = new List<PriceSetInfo>();
            if (!string.IsNullOrEmpty(keys))
            {
                string sqlText = PriceSetStatement.GetPriceSetByKeys;
                sqlText = sqlText.Replace("#ids#", keys);
                DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
                result = command.ExecuteEntityList<PriceSetInfo>();
            }

            return result;
        }

        public List<PriceSetInfo> GetPriceSetByRule(string name,int customerid,int receivingid,int sendstorageid,int carrierid, int status)
        {
            List<PriceSetInfo> result = new List<PriceSetInfo>();
            string sqlText = PriceSetStatement.GetAllPriceSetByRule;
            if (!string.IsNullOrEmpty(name))
            {
                sqlText += " AND PriceSetName LIKE '%'+@key+'%'";
            }
            if (status > -1)
            {
                sqlText += " AND Status=@Status";
            }
            if (customerid > -1)
            {
                sqlText += " AND CustomerID=@CustomerID";
            }
            if (receivingid > -1)
            {
                sqlText += " AND ReceivingID=@ReceivingID";
            }
            if (sendstorageid > -1)
            {
                sqlText += " AND StorageID=@StorageID";
            }
            if (carrierid > -1)
            {
                sqlText += " AND CarrierID=@CarrierID";
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
            if (customerid > -1)
            {
                command.AddInputParameter("@CustomerID", DbType.Int32, customerid);
            }
            if (receivingid > -1)
            {
                command.AddInputParameter("@ReceivingID", DbType.Int32, receivingid);
            }
            if (sendstorageid > -1)
            {
                command.AddInputParameter("@StorageID", DbType.Int32, sendstorageid);
            }
            if (carrierid > -1)
            {
                command.AddInputParameter("@CarrierID", DbType.Int32, carrierid);
            }

            result = command.ExecuteEntityList<PriceSetInfo>();
            return result;
        }

        public PriceSetInfo GetPriceSetByKey(long gid)
        {
            PriceSetInfo result = new PriceSetInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(PriceSetStatement.GetPriceSetByKey, "Text"));
            command.AddInputParameter("@PriceSetID", DbType.Int32, gid);
            result = command.ExecuteEntity<PriceSetInfo>();
            return result;
        }

        public long CreateNew(PriceSetInfo PriceSet)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(PriceSetStatement.CreateNewPriceSet, "Text"));
            command.AddInputParameter("@CustomerID", DbType.Int32, PriceSet.CustomerID);
            command.AddInputParameter("@StorageID", DbType.Int32, PriceSet.StorageID);
            command.AddInputParameter("@CarrierID", DbType.Int32, PriceSet.CarrierID);
            command.AddInputParameter("@ReceivingType", DbType.String, PriceSet.ReceivingType);
            command.AddInputParameter("@ReceivingID", DbType.Int32, PriceSet.ReceivingID);
            command.AddInputParameter("@TemType", DbType.String, PriceSet.TemType);

            command.AddInputParameter("@configPrice", DbType.Decimal, PriceSet.configPrice);
            command.AddInputParameter("@configHandInAmt", DbType.Decimal, PriceSet.configHandInAmt);
            command.AddInputParameter("@configSortPrice", DbType.Decimal, PriceSet.configSortPrice);
            command.AddInputParameter("@configCosting", DbType.Decimal, PriceSet.configCosting);
            command.AddInputParameter("@configHandOutAmt", DbType.Decimal, PriceSet.configHandOutAmt);
            command.AddInputParameter("@configSortCosting", DbType.Decimal, PriceSet.configSortCosting);

            command.AddInputParameter("@DeliveryModel", DbType.String, PriceSet.DeliveryModel);
            command.AddInputParameter("@Remark", DbType.String, PriceSet.Remark);
            command.AddInputParameter("@Status", DbType.Int32, PriceSet.Status);
            command.AddInputParameter("@OperatorID", DbType.Int32, PriceSet.OperatorID);
            command.AddInputParameter("@CreateDate", DbType.DateTime, PriceSet.CreateDate);
            command.AddInputParameter("@ChangeDate", DbType.DateTime, PriceSet.ChangeDate);

            var o = command.ExecuteScalar<object>();
            return Convert.ToInt64(o);
        }

        public int ModifyPriceSet(PriceSetInfo PriceSet)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(PriceSetStatement.ModifyPriceSet, "Text"));
            command.AddInputParameter("@CustomerID", DbType.Int32, PriceSet.CustomerID);
            command.AddInputParameter("@StorageID", DbType.Int32, PriceSet.StorageID);
            command.AddInputParameter("@CarrierID", DbType.Int32, PriceSet.CarrierID);
            command.AddInputParameter("@ReceivingType", DbType.String, PriceSet.ReceivingType);
            command.AddInputParameter("@ReceivingID", DbType.Int32, PriceSet.ReceivingID);
            command.AddInputParameter("@TemType", DbType.String, PriceSet.TemType);
            command.AddInputParameter("@configPrice", DbType.Decimal, PriceSet.configPrice);
            command.AddInputParameter("@configHandInAmt", DbType.Decimal, PriceSet.configHandInAmt);
            command.AddInputParameter("@configSortPrice", DbType.Decimal, PriceSet.configSortPrice);
            command.AddInputParameter("@configCosting", DbType.Decimal, PriceSet.configCosting);
            command.AddInputParameter("@configHandOutAmt", DbType.Decimal, PriceSet.configHandOutAmt);
            command.AddInputParameter("@configSortCosting", DbType.Decimal, PriceSet.configSortCosting);

            command.AddInputParameter("@DeliveryModel", DbType.String, PriceSet.DeliveryModel);
            command.AddInputParameter("@Remark", DbType.String, PriceSet.Remark);
            command.AddInputParameter("@Status", DbType.Int32, PriceSet.Status);
            command.AddInputParameter("@OperatorID", DbType.Int32, PriceSet.OperatorID);
            command.AddInputParameter("@ChangeDate", DbType.DateTime, PriceSet.ChangeDate);
            command.AddInputParameter("@PriceSetID", DbType.Int32, PriceSet.PriceSetID);

            return command.ExecuteNonQuery();
        }

        public int Remove(long PriceSetID)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(PriceSetStatement.Remove, "Text"));
            command.AddInputParameter("@PriceSetID", DbType.Int64, PriceSetID);
            int result = command.ExecuteNonQuery();
            return result;
        }


        #region 分页相关
        public List<PriceSetInfo> GetAllPriceSetInfoPager(PagerInfo pager)
        {
            List<PriceSetInfo> result = new List<PriceSetInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(PriceSetStatement.GetAllPriceSetInfoPager, "Text"));
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);
            result = command.ExecuteEntityList<PriceSetInfo>();
            return result;
        }


        public List<PriceSetInfo> GetPriceSetInfoByRule(string name,int carrierid, int storageid,int customerid, int status, PagerInfo pager)
        {
            List<PriceSetInfo> result = new List<PriceSetInfo>();

            StringBuilder builder = new StringBuilder();
            if (carrierid>0)
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
            string sqlText = PriceSetStatement.GetAllPriceSetInfoByRulePagerHeader + builder.ToString() + PriceSetStatement.GetAllPriceSetInfoByRulePagerFooter;

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

            result = command.ExecuteEntityList<PriceSetInfo>();
            return result;
        }

        public int GetPriceSetCount(string name, int carrierid, int storageid,int customerid, int status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(PriceSetStatement.GetCount);
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
