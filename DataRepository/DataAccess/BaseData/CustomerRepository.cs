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
    public class CustomerRepository : DataAccessBase
    {
        public List<CustomerInfo> GetAllCustomer()
        {
            List<CustomerInfo> result = new List<CustomerInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CustomerStatement.GetAllCustomer, "Text"));
            result = command.ExecuteEntityList<CustomerInfo>();
            return result;
        }

        public List<CustomerInfo> GetCustomerByKeys(string keys)
        {
            List<CustomerInfo> result = new List<CustomerInfo>();
            if (!string.IsNullOrEmpty(keys))
            {
                string sqlText = CustomerStatement.GetCustomerByKeys;
                sqlText = sqlText.Replace("#ids#", keys);
                DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
                result = command.ExecuteEntityList<CustomerInfo>();
            }

            return result;
        }

        public List<CustomerInfo> GetCustomerByRule(string name, int status)
        {
            List<CustomerInfo> result = new List<CustomerInfo>();
            string sqlText = CustomerStatement.GetAllCustomerByRule;
            if (!string.IsNullOrEmpty(name))
            {
                sqlText += " AND CustomerName LIKE '%'+@key+'%'";
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

            result = command.ExecuteEntityList<CustomerInfo>();
            return result;
        }

        public CustomerInfo GetCustomerByKey(long gid)
        {
            CustomerInfo result = new CustomerInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CustomerStatement.GetCustomerByKey, "Text"));
            command.AddInputParameter("@CustomerID", DbType.String, gid);
            result = command.ExecuteEntity<CustomerInfo>();
            return result;
        }

        public long CreateNew(CustomerInfo Customer)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CustomerStatement.CreateNewCustomer, "Text"));
            command.AddInputParameter("@CustomerName", DbType.String, Customer.CustomerName);
            command.AddInputParameter("@CustomerNo", DbType.String, Customer.CustomerNo);
            command.AddInputParameter("@Status", DbType.Int32, Customer.Status);
            command.AddInputParameter("@OperatorID", DbType.String, Customer.OperatorID);
            command.AddInputParameter("@CreateDate", DbType.DateTime, Customer.CreateDate);
            command.AddInputParameter("@ChangeDate", DbType.DateTime, Customer.ChangeDate);

            var o = command.ExecuteScalar<object>();
            return Convert.ToInt64(o);
        }

        public int ModifyCustomer(CustomerInfo Customer)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CustomerStatement.ModifyCustomer, "Text"));
            command.AddInputParameter("@CustomerName", DbType.String, Customer.CustomerName);
            command.AddInputParameter("@CustomerNo", DbType.String, Customer.CustomerNo);
            command.AddInputParameter("@OperatorID", DbType.Int64, Customer.OperatorID);
            command.AddInputParameter("@Status", DbType.Boolean, Customer.Status);   
            command.AddInputParameter("@ChangeDate", DbType.DateTime, Customer.ChangeDate);
            command.AddInputParameter("@CustomerID", DbType.Int64, Customer.CustomerID);
            return command.ExecuteNonQuery();
        }

        public int Remove(long CustomerID)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CustomerStatement.Remove, "Text"));
            command.AddInputParameter("@CustomerID", DbType.Int64, CustomerID);
            int result = command.ExecuteNonQuery();
            return result;
        }


        #region 分页相关
        public List<CustomerInfo> GetAllCustomerInfoPager(PagerInfo pager)
        {
            List<CustomerInfo> result = new List<CustomerInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CustomerStatement.GetAllCustomerInfoPager, "Text"));
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);
            result = command.ExecuteEntityList<CustomerInfo>();
            return result;
        }


        public List<CustomerInfo> GetCustomerInfoByRule(string name, string modelCode, int status, PagerInfo pager)
        {
            List<CustomerInfo> result = new List<CustomerInfo>();

            StringBuilder builder = new StringBuilder();

            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" AND (CustomerName LIKE '%'+@key+'%' OR CustomerNO  LIKE '%'+@key+'%')");
            }
            if (status > -1)
            {
                builder.Append(" AND Status=@Status");
            }
            string sqlText = CustomerStatement.GetAllCustomerInfoByRulePagerHeader + builder.ToString() + CustomerStatement.GetAllCustomerInfoByRulePagerFooter;

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

            result = command.ExecuteEntityList<CustomerInfo>();
            return result;
        }

        public int GetCustomerCount(string name, string modelCode, int status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(CustomerStatement.GetCount);
            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" AND (CustomerName LIKE '%'+@key+'%' OR CustomerNO  LIKE '%'+@key+'%')");
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
