/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：MenuRepository
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：4/28/2018 9:56:46 AM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using DataRepository.DataModel;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Helper;
using Common;



namespace DataRepository.DataAccess.Advise
{
    public class AdviseRepository : DataAccessBase
    {

        public List<AdviseInfo> GetAllAdviseInfo()
        {
            List<AdviseInfo> result = new List<AdviseInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(AdviseStatement.GetAllAdviseInfo, "Text"));
            result = command.ExecuteEntityList<AdviseInfo>();
            return result;
        }

        public AdviseInfo GetAdviseInfoByID(long adviseId)
        {
            AdviseInfo result = new AdviseInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(AdviseStatement.GetAdviseInfoByID, "Text"));
            command.AddInputParameter("@AdviseID", DbType.Int64, adviseId);
            result = command.ExecuteEntity<AdviseInfo>();
            return result;
        }

        public List<AdviseInfo> GetAdviseInfoByCustomerID(long customerId)
        {
            List<AdviseInfo> result = new List<AdviseInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(AdviseStatement.GetAdviseInfoByCustomerID, "Text"));
            command.AddInputParameter("@CustomerID", DbType.Int64, customerId);
            result = command.ExecuteEntityList<AdviseInfo>();

            return result;
        }

        public List<AdviseInfo> GetAdviseInfoByUserID(long userId)
        {
            List<AdviseInfo> result = new List<AdviseInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(AdviseStatement.GetAdviseInfoByUserID, "Text"));
            command.AddInputParameter("@Operator", DbType.Int64, userId);
            result = command.ExecuteEntityList<AdviseInfo>();

            return result;
        }

        public int CreateNewAdvise(AdviseInfo adviseInfo)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(AdviseStatement.CreateNewtAdviseInfo, "Text"));
            command.AddInputParameter("@AdviseTitle", DbType.String, adviseInfo.AdviseTitle);
            command.AddInputParameter("@Summary", DbType.String, adviseInfo.Summary);
            command.AddInputParameter("@CustomerID", DbType.Int64, adviseInfo.CustomerID);
            command.AddInputParameter("@CustomerName", DbType.String, adviseInfo.CustomerName);
            command.AddInputParameter("@CustomerMobile", DbType.String, adviseInfo.CustomerMobile);
            command.AddInputParameter("@DealStatus", DbType.Int32, adviseInfo.DealStatus);
            command.AddInputParameter("@DealSummary", DbType.String, adviseInfo.DealSummary);
            command.AddInputParameter("@Operator", DbType.Int64, adviseInfo.Operator);
            command.AddInputParameter("@CreateDate", DbType.DateTime, adviseInfo.CreateDate);
            command.AddInputParameter("@ModifyDate", DbType.DateTime, adviseInfo.ModifyDate);

            return command.ExecuteNonQuery();
        }

        public int ModifyAdvise(AdviseInfo adviseInfo)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(AdviseStatement.ModifytAdviseInfo, "Text"));
            command.AddInputParameter("@AdviseID", DbType.Int64, adviseInfo.AdviseID);
            command.AddInputParameter("@AdviseTitle", DbType.String, adviseInfo.AdviseTitle);
            command.AddInputParameter("@Summary", DbType.String, adviseInfo.Summary);
            command.AddInputParameter("@CustomerID", DbType.Int64, adviseInfo.CustomerID);
            command.AddInputParameter("@CustomerName", DbType.String, adviseInfo.CustomerName);
            command.AddInputParameter("@CustomerMobile", DbType.String, adviseInfo.CustomerMobile);
            command.AddInputParameter("@DealStatus", DbType.Int32, adviseInfo.DealStatus);
            command.AddInputParameter("@DealSummary", DbType.String, adviseInfo.DealSummary);
            command.AddInputParameter("@Operator", DbType.Int64, adviseInfo.Operator);
            command.AddInputParameter("@ModifyDate", DbType.DateTime, adviseInfo.ModifyDate);

            return command.ExecuteNonQuery();
        }

        public int ModifyAdviseDealInfo(AdviseInfo adviseInfo)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(AdviseStatement.DealAdviseInfo, "Text"));
            command.AddInputParameter("@AdviseID", DbType.Int64, adviseInfo.AdviseID);
            command.AddInputParameter("@DealStatus", DbType.Int32, adviseInfo.DealStatus);
            command.AddInputParameter("@DealSummary", DbType.String, adviseInfo.DealSummary);
            command.AddInputParameter("@Operator", DbType.Int64, adviseInfo.Operator);
            command.AddInputParameter("@ModifyDate", DbType.DateTime, adviseInfo.ModifyDate);

            return command.ExecuteNonQuery();
        }




        public List<AdviseInfo> GetAllAdviseInfoByRule(string customerName, string title, int dealStatus, PagerInfo pager)
        {
            List<AdviseInfo> result = new List<AdviseInfo>();


            StringBuilder builder = new StringBuilder();

            if (!string.IsNullOrEmpty(customerName))
            {
                builder.Append(" AND CustomerName LIKE '%'+@CustomerName+'%' ");
            }
            if (!string.IsNullOrEmpty(title))
            {
                builder.Append(" AND AdviseTitle LIKE '%'+@AdviseTitle+'%' ");
            }
            if (dealStatus > -1)
            {
                builder.Append(" AND DealStatus=@DealStatus ");
            }

            string sql = AdviseStatement.GetAllAdviseInfoPagerHeader + builder.ToString() + AdviseStatement.GetAllAdviseInfoPagerFooter;

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sql, "Text"));

            if (!string.IsNullOrEmpty(customerName))
            {
                command.AddInputParameter("@CustomerName", DbType.Int64, customerName);
            }
            if (!string.IsNullOrEmpty(title))
            {
                command.AddInputParameter("@AdviseTitle", DbType.String, title);
            }
            if (dealStatus > -1)
            {
                command.AddInputParameter("@DealStatus", DbType.Int32, dealStatus);
            }
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);

            result = command.ExecuteEntityList<AdviseInfo>();
            return result;
        }


        public int GetAdviseCount(string customerName, string title, int dealStatus)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(AdviseStatement.GetAdviseCount);
            if (!string.IsNullOrEmpty(customerName))
            {
                builder.Append(" AND CustomerName LIKE '%'+@CustomerName+'%' ");
            }
            if (!string.IsNullOrEmpty(title))
            {
                builder.Append(" AND AdviseTitle LIKE '%'+@AdviseTitle+'%' ");
            }
            if (dealStatus > -1)
            {
                builder.Append(" AND DealStatus=@DealStatus ");
            }

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(builder.ToString(), "Text"));

            if (!string.IsNullOrEmpty(customerName))
            {
                command.AddInputParameter("@CustomerName", DbType.Int64, customerName);
            }
            if (!string.IsNullOrEmpty(title))
            {
                command.AddInputParameter("@AdviseTitle", DbType.String, title);
            }
            if (dealStatus > -1)
            {
                command.AddInputParameter("@DealStatus", DbType.Int32, dealStatus);
            }


            var o = command.ExecuteScalar<object>();
            return Convert.ToInt32(o);
        }

        public List<AdviseInfo> GetAllAdviseInfoPager(PagerInfo pager)
        {
            List<AdviseInfo> result = new List<AdviseInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(AdviseStatement.GetAllAdviseInfoPager, "Text"));
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);
            result = command.ExecuteEntityList<AdviseInfo>();
            return result;
        }

 
    }
}
