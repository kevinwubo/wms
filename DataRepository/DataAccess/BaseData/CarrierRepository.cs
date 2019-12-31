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
    public class CarrierRepository : DataAccessBase
    {
        public List<CarrierInfo> GetAllCarrier()
        {
            List<CarrierInfo> result = new List<CarrierInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CarrierStatement.GetAllCarrier, "Text"));
            result = command.ExecuteEntityList<CarrierInfo>();
            return result;
        }

        public List<CarrierInfo> GetCarrierByKeys(string keys)
        {
            List<CarrierInfo> result = new List<CarrierInfo>();
            if (!string.IsNullOrEmpty(keys))
            {
                string sqlText = CarrierStatement.GetCarrierByKeys;
                sqlText = sqlText.Replace("#ids#", keys);
                DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
                result = command.ExecuteEntityList<CarrierInfo>();
            }

            return result;
        }

        public List<CarrierInfo> GetCarrierByRule(string name, int status)
        {
            List<CarrierInfo> result = new List<CarrierInfo>();
            string sqlText = CarrierStatement.GetAllCarrierByRule;
            if (!string.IsNullOrEmpty(name))
            {
                sqlText += " AND CarrierName LIKE '%'+@key+'%'";
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

            result = command.ExecuteEntityList<CarrierInfo>();
            return result;
        }

        public CarrierInfo GetCarrierByKey(long gid)
        {
            CarrierInfo result = new CarrierInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CarrierStatement.GetCarrierByKey, "Text"));
            command.AddInputParameter("@CarrierID", DbType.String, gid);
            result = command.ExecuteEntity<CarrierInfo>();
            return result;
        }

        public long CreateNew(CarrierInfo Carrier)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CarrierStatement.CreateNewCarrier, "Text"));
            command.AddInputParameter("@CarrierName", DbType.String, Carrier.CarrierName);
            command.AddInputParameter("@CarrierShortName", DbType.String, Carrier.CarrierShortName);
            command.AddInputParameter("@CarrierNo", DbType.String, Carrier.CarrierNo);
            command.AddInputParameter("@Remark", DbType.String, Carrier.Remark);
            command.AddInputParameter("@Type", DbType.String, Carrier.Type);
            command.AddInputParameter("@CarNo", DbType.String, Carrier.CarNo);
            command.AddInputParameter("@OperatorID", DbType.String, Carrier.OperatorID);
            command.AddInputParameter("@CreateDate", DbType.DateTime, Carrier.CreateDate);
            command.AddInputParameter("@ChangeDate", DbType.DateTime, Carrier.ChangeDate);

            //return command.ExecuteNonQuery();
            var o = command.ExecuteScalar<object>();
            return Convert.ToInt64(o);
        }

        public int ModifyCarrier(CarrierInfo Carrier)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CarrierStatement.ModifyCarrier, "Text"));
            command.AddInputParameter("@CarrierName", DbType.String, Carrier.CarrierName);
            command.AddInputParameter("@CarrierShortName", DbType.String, Carrier.CarrierShortName);
            command.AddInputParameter("@CarrierNo", DbType.String, Carrier.CarrierNo);
            command.AddInputParameter("@Type", DbType.String, Carrier.Type);
            command.AddInputParameter("@Remark", DbType.String, Carrier.Remark);
            command.AddInputParameter("@CarNo", DbType.String, Carrier.CarNo);
            command.AddInputParameter("@OperatorID", DbType.Int64, Carrier.OperatorID);
            command.AddInputParameter("@Status", DbType.Boolean, Carrier.Status);   
            command.AddInputParameter("@ChangeDate", DbType.DateTime, Carrier.ChangeDate);
            command.AddInputParameter("@CarrierID", DbType.Int64, Carrier.CarrierID);
            return command.ExecuteNonQuery();
        }

        public int Remove(long CarrierID)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CarrierStatement.Remove, "Text"));
            command.AddInputParameter("@CarrierID", DbType.Int64, CarrierID);
            int result = command.ExecuteNonQuery();
            return result;
        }


        #region 分页相关
        public List<CarrierInfo> GetAllCarrierInfoPager(PagerInfo pager)
        {
            List<CarrierInfo> result = new List<CarrierInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CarrierStatement.GetAllCarrierInfoPager, "Text"));
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);
            result = command.ExecuteEntityList<CarrierInfo>();
            return result;
        }


        public List<CarrierInfo> GetCarrierInfoByRule(string name, string type, int status, PagerInfo pager)
        {
            List<CarrierInfo> result = new List<CarrierInfo>();

            StringBuilder builder = new StringBuilder();

            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" AND (CarrierName LIKE '%'+@key+'%' Or CarrierShortName LIKE '%'+@key+'%')");
            }
            if (!string.IsNullOrEmpty(type))
            {
                builder.Append(" AND Type=@Type");
            }
            if (status > -1)
            {
                builder.Append(" AND Status=@Status");
            }
            string sqlText = CarrierStatement.GetAllCarrierInfoByRulePagerHeader + builder.ToString() + CarrierStatement.GetAllCarrierInfoByRulePagerFooter;

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            if (!string.IsNullOrEmpty(name))
            {
                command.AddInputParameter("@key", DbType.String, name);
            }
            if (!string.IsNullOrEmpty(type))
            {
                command.AddInputParameter("@Type", DbType.String, type); 
            }
            if (status > -1)
            {
                command.AddInputParameter("@Status", DbType.Int32, status);
            }
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);

            result = command.ExecuteEntityList<CarrierInfo>();
            return result;
        }

        public int GetCarrierCount(string name, string type, int status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(CarrierStatement.GetCount);
            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" AND (CarrierName LIKE '%'+@key+'%' Or CarrierShortName LIKE '%'+@key+'%')");
            }
            if (!string.IsNullOrEmpty(type))
            {
                builder.Append(" AND Type=@Type");
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
            if (!string.IsNullOrEmpty(type))
            {
                command.AddInputParameter("@Type", DbType.String, type);
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
