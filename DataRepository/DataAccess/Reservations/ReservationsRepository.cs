using Common;
using DataRepository.DataModel;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Reservations
{
    public class ReservationsRepository : DataAccessBase
    {
        public int CreateNew(ReservationsInfo info)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ReservationsStatement.InsertSql, "Text"));
            command.AddInputParameter("@CustomerID", DbType.Int64, info.CustomerID);
            command.AddInputParameter("@CustomerName", DbType.String, info.CustomerName);
            command.AddInputParameter("@RType", DbType.String, info.RType);
            command.AddInputParameter("@PayType", DbType.Int32, info.PayType);
            command.AddInputParameter("@CarID", DbType.Int32, info.CarID);
            command.AddInputParameter("@LeaseTime", DbType.Int32, info.LeaseTime);
            command.AddInputParameter("@Price", DbType.Decimal, info.Price);
            command.AddInputParameter("@Remark", DbType.String, info.Remark);
            command.AddInputParameter("@RDate", DbType.DateTime, info.RDate);
            command.AddInputParameter("@Status", DbType.Int32, info.Status);
            command.AddInputParameter("@CreateDate", DbType.DateTime, info.CreateDate);

            return command.ExecuteNonQuery();
        }


        public ReservationsInfo GetReservationsInfoByID(long rid)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ReservationsStatement.GetReservationByID, "Text"));
            command.AddInputParameter("@ID", DbType.Int64, rid);
            return command.ExecuteEntity<ReservationsInfo>();
        }


        public List<ReservationsInfo> GetReservationsPagerByRule(ReservationsSearchEntity serach, PagerInfo pager)
        {
            List<ReservationsInfo> result = new List<ReservationsInfo>();


            StringBuilder builder = new StringBuilder();

            if (serach.CustomerID>0)
            {
                builder.Append(" AND CustomerID=@CustomerID ");
            }
            if (!string.IsNullOrEmpty(serach.RType))
            {
                builder.Append(" AND RType =@RType ");
            }
            if (!string.IsNullOrEmpty(serach.PayType))
            {
                builder.Append(" AND PayType=@PayType ");
            }
            if (serach.Status>-2)
            {
                builder.Append(" AND Status=@Status ");
            }

            string sql = ReservationsStatement.GetReservationPagerHeader + builder.ToString() + ReservationsStatement.GetReservationPagerFooter;

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sql, "Text"));

            if (serach.CustomerID > 0)
            {
                command.AddInputParameter("@CustomerID", DbType.Int64, serach.CustomerID);
            }
            if (!string.IsNullOrEmpty(serach.RType))
            {
                command.AddInputParameter("@RType", DbType.Int32, serach.RType);
            }
            if (!string.IsNullOrEmpty(serach.PayType))
            {
                command.AddInputParameter("@PayType", DbType.String, serach.PayType);
            }
            if (serach.Status > -2)
            {
                command.AddInputParameter("@Status", DbType.Int32, serach.Status);
            }
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);

            result = command.ExecuteEntityList<ReservationsInfo>();


            return result;
        }


        public int GetReservationsCount(ReservationsSearchEntity serach)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(ReservationsStatement.GetReservationCount);
            if (serach.CustomerID > 0)
            {
                builder.Append(" AND CustomerID=@CustomerID ");
            }
            if (!string.IsNullOrEmpty(serach.RType))
            {
                builder.Append(" AND RType =@RType ");
            }
            if (!string.IsNullOrEmpty(serach.PayType))
            {
                builder.Append(" AND PayType=@PayType ");
            }

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(builder.ToString(), "Text"));

            if (serach.CustomerID > 0)
            {
                command.AddInputParameter("@CustomerID", DbType.Int64, serach.CustomerID);
            }
            if (!string.IsNullOrEmpty(serach.RType))
            {
                command.AddInputParameter("@RType", DbType.String, serach.RType);
            }
            if (!string.IsNullOrEmpty(serach.PayType))
            {
                command.AddInputParameter("@PayType", DbType.String, serach.PayType);
            }


            var o = command.ExecuteScalar<object>();
            return Convert.ToInt32(o);
        }


        public int EditReservationsStatus(long rid, int status)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ReservationsStatement.EditReservationStatus, "Text"));
            command.AddInputParameter("@ID", DbType.Int64, rid);
            command.AddInputParameter("@Status", DbType.Int32, status);
            return command.ExecuteNonQuery();
        }
    }
}
