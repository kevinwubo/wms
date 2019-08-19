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
    public class ReceiverRepository : DataAccessBase
    {
        public List<ReceiverInfo> GetAllReceiver()
        {
            List<ReceiverInfo> result = new List<ReceiverInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ReceiverStatement.GetAllReceiver, "Text"));
            result = command.ExecuteEntityList<ReceiverInfo>();
            return result;
        }

        public List<ReceiverInfo> GetReceiverByCustomerID(int CustomerID)
        {
            List<ReceiverInfo> result = new List<ReceiverInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ReceiverStatement.GetReceiverByCustomerID, "Text"));
            command.AddInputParameter("@CustomerID", DbType.Int32, CustomerID);            
            result = command.ExecuteEntityList<ReceiverInfo>();
            return result;
        }

        public List<ReceiverInfo> GetReceiverByKeys(string keys)
        {
            List<ReceiverInfo> result = new List<ReceiverInfo>();
            if (!string.IsNullOrEmpty(keys))
            {
                string sqlText = ReceiverStatement.GetReceiverByKeys;
                sqlText = sqlText.Replace("#ids#", keys);
                DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
                result = command.ExecuteEntityList<ReceiverInfo>();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="no">编号</param>
        /// <param name="receiverType">类型</param>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<ReceiverInfo> GetReceiverByRule(string name,string no, string receiverType, int status)
        {
            List<ReceiverInfo> result = new List<ReceiverInfo>();
            string sqlText = ReceiverStatement.GetAllReceiverByRule;
            if (!string.IsNullOrEmpty(name))
            {
                sqlText += " AND ReceiverName =@ReceiverName";
            }
            if (!string.IsNullOrEmpty(receiverType))
            {
                sqlText += " AND receiverType=@receiverType ";
            }
            if (!string.IsNullOrEmpty(no))
            {
                sqlText += " AND ReceiverNo=@ReceiverNo";
            }
            if (status > -1)
            {
                sqlText += " AND Status=@Status";
            }


            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            if (!string.IsNullOrEmpty(name))
            {
                command.AddInputParameter("@ReceiverName", DbType.String, name);
            }
            if (!string.IsNullOrEmpty(no))
            {
                command.AddInputParameter("@ReceiverNo", DbType.String, no); 
            }
            if (status > -1)
            {
                command.AddInputParameter("@Status", DbType.Int32, status);
            }
            if (!string.IsNullOrEmpty(receiverType))
            {
                command.AddInputParameter("@receiverType", DbType.String, receiverType);
            }
            result = command.ExecuteEntityList<ReceiverInfo>();
            return result;
        }

        public ReceiverInfo GetReceiverByKey(long gid)
        {
            ReceiverInfo result = new ReceiverInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ReceiverStatement.GetReceiverByKey, "Text"));
            command.AddInputParameter("@ReceiverID", DbType.String, gid);
            result = command.ExecuteEntity<ReceiverInfo>();
            return result;
        }

        public long CreateNew(ReceiverInfo Receiver)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ReceiverStatement.CreateNewReceiver, "Text"));
            command.AddInputParameter("@CustomerID", DbType.String, Receiver.CustomerID);
            command.AddInputParameter("@ReceiverName", DbType.String, Receiver.ReceiverName);
            command.AddInputParameter("@ReceiverNo", DbType.String, Receiver.ReceiverNo);
            command.AddInputParameter("@ProvinceID", DbType.Int32, Receiver.ProvinceID);
            command.AddInputParameter("@CityID", DbType.Int32, Receiver.CityID);
            command.AddInputParameter("@ReceiverType", DbType.String, Receiver.ReceiverType);
            command.AddInputParameter("@Address", DbType.String, Receiver.Address);
            command.AddInputParameter("@Remark", DbType.String, Receiver.Remark);
            command.AddInputParameter("@Status", DbType.Int32, Receiver.Status);
            command.AddInputParameter("@DefaultCarrierID", DbType.Int32, Receiver.DefaultCarrierID);
            command.AddInputParameter("@DefaultStorageID", DbType.Int32, Receiver.DefaultStorageID);
            command.AddInputParameter("@OperatorID", DbType.Int64, Receiver.OperatorID);
            command.AddInputParameter("@CreateDate", DbType.DateTime, Receiver.CreateDate);
            command.AddInputParameter("@ChangeDate", DbType.DateTime, Receiver.ChangeDate);

            var o = command.ExecuteScalar<object>();
            return Convert.ToInt64(o);
        }

        public int ModifyReceiver(ReceiverInfo Receiver)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ReceiverStatement.ModifyReceiver, "Text"));
            command.AddInputParameter("@CustomerID", DbType.String, Receiver.CustomerID);
            command.AddInputParameter("@ReceiverName", DbType.String, Receiver.ReceiverName);
            command.AddInputParameter("@ReceiverNo", DbType.String, Receiver.ReceiverNo);
            command.AddInputParameter("@ProvinceID", DbType.Int32, Receiver.ProvinceID);
            command.AddInputParameter("@CityID", DbType.Int32, Receiver.CityID);
            command.AddInputParameter("@ReceiverType", DbType.String, Receiver.ReceiverType);
            command.AddInputParameter("@Address", DbType.String, Receiver.Address);
            command.AddInputParameter("@Remark", DbType.String, Receiver.Remark);
            command.AddInputParameter("@Status", DbType.Int32, Receiver.Status);
            command.AddInputParameter("@DefaultCarrierID", DbType.Int32, Receiver.DefaultCarrierID);
            command.AddInputParameter("@DefaultStorageID", DbType.Int32, Receiver.DefaultStorageID);
            command.AddInputParameter("@OperatorID", DbType.Int64, Receiver.OperatorID);
            command.AddInputParameter("@ChangeDate", DbType.DateTime, Receiver.ChangeDate);
            command.AddInputParameter("@ReceiverID", DbType.Int64, Receiver.ReceiverID);
            return command.ExecuteNonQuery();
        }

        public int Remove(long ReceiverID)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ReceiverStatement.Remove, "Text"));
            command.AddInputParameter("@ReceiverID", DbType.Int64, ReceiverID);
            int result = command.ExecuteNonQuery();
            return result;
        }


        #region 分页相关
        public List<ReceiverInfo> GetAllReceiverInfoPager(PagerInfo pager)
        {
            List<ReceiverInfo> result = new List<ReceiverInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ReceiverStatement.GetAllReceiverInfoPager, "Text"));
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);
            result = command.ExecuteEntityList<ReceiverInfo>();
            return result;
        }


        public List<ReceiverInfo> GetReceiverInfoByRule(string name, string receiverType, int customerID, int status, PagerInfo pager)
        {
            List<ReceiverInfo> result = new List<ReceiverInfo>();

            StringBuilder builder = new StringBuilder();

            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" AND (ReceiverName LIKE '%'+@key+'%' OR ReceiverNO LIKE '%'+@key+'%')");
            }
            if (!string.IsNullOrEmpty(receiverType))
            {
                builder.Append(" AND receiverType=@receiverType");
            }
            if (customerID > 0)
            {
                builder.Append(" AND customerID=@customerID");
            }
            if (status > -1)
            {
                builder.Append(" AND Status=@Status");
            }
            string sqlText = ReceiverStatement.GetAllReceiverInfoByRulePagerHeader + builder.ToString() + ReceiverStatement.GetAllReceiverInfoByRulePagerFooter;

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            if (!string.IsNullOrEmpty(name))
            {
                command.AddInputParameter("@key", DbType.String, name);
            }
            if (!string.IsNullOrEmpty(receiverType))
            {
                command.AddInputParameter("@receiverType", DbType.String, receiverType);
            }
            if (customerID > 0)
            {
                command.AddInputParameter("@customerID", DbType.Int32, customerID);
            }
            if (status > -1)
            {
                command.AddInputParameter("@Status", DbType.Int32, status);
            }
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);

            result = command.ExecuteEntityList<ReceiverInfo>();
            return result;
        }

        public int GetReceiverCount(string name, string receiverType, int customerID, int status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(ReceiverStatement.GetCount);
            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" AND (ReceiverName LIKE '%'+@key+'%' OR ReceiverNO LIKE '%'+@key+'%')");
            }
            if (!string.IsNullOrEmpty(receiverType))
            {
                builder.Append(" AND receiverType=@receiverType");
            }
            if (customerID > 0)
            {
                builder.Append(" AND customerID=@customerID");
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
            if (!string.IsNullOrEmpty(receiverType))
            {
                command.AddInputParameter("@receiverType", DbType.String, receiverType);
            }
            if (customerID > 0)
            {
                command.AddInputParameter("@customerID", DbType.Int32, customerID);
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
