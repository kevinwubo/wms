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
    public class OrderDeliverPlanRepository : DataAccessBase
    {

        public List<OrderDeliverPlanInfo> GetAllOrderDeliverPlan(string orderids)
        {
            List<OrderDeliverPlanInfo> result = new List<OrderDeliverPlanInfo>();
            string sqlText = OrderDeliverPlanStatement.GetAllOrderDeliverPlan;
            if (!string.IsNullOrEmpty(orderids))
            {
                sqlText += " AND OrderIDS like '%" + orderids + "%'";
            }
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            result = command.ExecuteEntityList<OrderDeliverPlanInfo>();
            return result;
        }

        public OrderDeliverPlanInfo GetOrderDeliverPlanByKey(long ID)
        {
            OrderDeliverPlanInfo result = new OrderDeliverPlanInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderDeliverPlanStatement.GetOrderDeliverPlanByKey, "Text"));
            command.AddInputParameter("@PlanID", DbType.String, ID);
            result = command.ExecuteEntity<OrderDeliverPlanInfo>();
            return result;
        }

        public int CreateNew(OrderDeliverPlanInfo info)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderDeliverPlanStatement.CreateNewOrderDeliverPlan, "Text"));
            command.AddInputParameter("@OrderIDS", DbType.String, info.OrderIDS);
            command.AddInputParameter("@DeliveryNo", DbType.String, info.DeliveryNo);
            command.AddInputParameter("@CarrierName", DbType.String, info.CarrierName);
            command.AddInputParameter("@CarrierID", DbType.String, info.CarrierID);
            command.AddInputParameter("@Temp", DbType.String, info.Temp);
            command.AddInputParameter("@DeliveryType", DbType.String, info.DeliveryType);
            command.AddInputParameter("@DriverName", DbType.String, info.DriverName);
            command.AddInputParameter("@DriverTelephone", DbType.String, info.DriverTelephone);
            command.AddInputParameter("@CarModel", DbType.String, info.CarModel);
            command.AddInputParameter("@CarNo", DbType.String, info.CarNo);
            command.AddInputParameter("@OilCardNo", DbType.String, info.OilCardNo);
            command.AddInputParameter("@OilCardBalance", DbType.Decimal, info.OilCardBalance);
            command.AddInputParameter("@GPSNo", DbType.String, info.GPSNo);
            command.AddInputParameter("@NeedTicket", DbType.Boolean, info.NeedTicket);
            command.AddInputParameter("@DeliverDate", DbType.DateTime, info.DeliverDate);
            command.AddInputParameter("@Remark", DbType.String, info.Remark);
            command.AddInputParameter("@OperatorID", DbType.String, info.OperatorID);
            command.AddInputParameter("@CreateDate", DbType.DateTime, info.CreateDate);
            command.AddInputParameter("@ChangeDate", DbType.DateTime, info.ChangeDate);
            var o = command.ExecuteScalar<object>();
            return Convert.ToInt32(o);
        }

        public int ModifyOrderDeliverPlan(OrderDeliverPlanInfo info)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderDeliverPlanStatement.ModifyOrderDeliverPlan, "Text"));
            command.AddInputParameter("@PlanID", DbType.Int32, info.PlanID);
            command.AddInputParameter("@OrderIDS", DbType.String, info.OrderIDS);
            command.AddInputParameter("@DeliveryNo", DbType.String, info.DeliveryNo);
            command.AddInputParameter("@CarrierName", DbType.String, info.CarrierName);
            command.AddInputParameter("@CarrierID", DbType.String, info.CarrierID);
            command.AddInputParameter("@Temp", DbType.String, info.Temp);
            command.AddInputParameter("@DeliveryType", DbType.String, info.DeliveryType);
            command.AddInputParameter("@DriverName", DbType.String, info.DriverName);
            command.AddInputParameter("@DriverTelephone", DbType.String, info.DriverTelephone);
            command.AddInputParameter("@CarModel", DbType.String, info.CarModel);
            command.AddInputParameter("@CarNo", DbType.String, info.CarNo);
            command.AddInputParameter("@OilCardNo", DbType.String, info.OilCardNo);
            command.AddInputParameter("@OilCardBalance", DbType.Decimal, info.OilCardBalance);
            command.AddInputParameter("@GPSNo", DbType.String, info.GPSNo);
            command.AddInputParameter("@NeedTicket", DbType.Boolean, info.NeedTicket);
            command.AddInputParameter("@DeliverDate", DbType.DateTime, info.DeliverDate);
            command.AddInputParameter("@Remark", DbType.String, info.Remark);
            command.AddInputParameter("@OperatorID", DbType.String, info.OperatorID);
            command.AddInputParameter("@ChangeDate", DbType.DateTime, info.ChangeDate);

            return command.ExecuteNonQuery();            
        }

        public int Delete(long BlackID)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderDeliverPlanStatement.Delete, "Text"));
            command.AddInputParameter("@BlackID", DbType.Int64, BlackID);
            int result = command.ExecuteNonQuery();
            return result;
        }


        #region 分页相关
        public List<OrderDeliverPlanInfo> GetAllOrderDeliverPlanInfoPager(PagerInfo pager)
        {
            List<OrderDeliverPlanInfo> result = new List<OrderDeliverPlanInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderDeliverPlanStatement.GetAllOrderDeliverPlanInfoPager, "Text"));
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);
            result = command.ExecuteEntityList<OrderDeliverPlanInfo>();
            return result;
        }


        public List<OrderDeliverPlanInfo> GetOrderDeliverPlanInfoByRule(int carrierID, string begindate, string enddate, PagerInfo pager)
        {
            List<OrderDeliverPlanInfo> result = new List<OrderDeliverPlanInfo>();

            StringBuilder builder = new StringBuilder();
            if (carrierID > -1)
            {
                builder.Append(" AND CarrierID=@CarrierID");
            }
            if (!string.IsNullOrEmpty(begindate) && !string.IsNullOrEmpty(enddate))
            {
                builder.Append(" AND DeliverDate BETWEEN @begindate AND @enddate ");
            }
            string sqlText = OrderDeliverPlanStatement.GetAllOrderDeliverPlanInfoByRulePagerHeader + builder.ToString() + OrderDeliverPlanStatement.GetAllOrderDeliverPlanInfoByRulePagerFooter;

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            if (carrierID > -1)
            {
                command.AddInputParameter("@CarrierID", DbType.Int32, carrierID);
            }
            if (!string.IsNullOrEmpty(begindate) && !string.IsNullOrEmpty(enddate))
            {
                command.AddInputParameter("@begindate", DbType.String, begindate);
                command.AddInputParameter("@enddate", DbType.String, enddate);
            }

            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);

            result = command.ExecuteEntityList<OrderDeliverPlanInfo>();
            return result;
        }

        public int GetOrderDeliverPlanCount(int carrierID, string begindate, string enddate)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(OrderDeliverPlanStatement.GetCount);
            if (carrierID > -1)
            {
                builder.Append(" AND CarrierID=@CarrierID");
            }
            if (!string.IsNullOrEmpty(begindate) && !string.IsNullOrEmpty(enddate))
            {
                builder.Append(" AND DeliverDate BETWEEN @begindate AND @enddate ");
            }
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(builder.ToString(), "Text"));
            if (carrierID > -1)
            {
                command.AddInputParameter("@CarrierID", DbType.Int32, carrierID);
            }
            if (!string.IsNullOrEmpty(begindate) && !string.IsNullOrEmpty(enddate))
            {
                command.AddInputParameter("@begindate", DbType.String, begindate);
                command.AddInputParameter("@enddate", DbType.String, enddate);
            }
            var o = command.ExecuteScalar<object>();
            return Convert.ToInt32(o);
        }

        #endregion
    }
}
