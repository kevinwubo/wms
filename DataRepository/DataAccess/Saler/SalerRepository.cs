using Common;
using DataRepository.DataModel;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Saler
{
    public class SalerRepository:DataAccessBase
    {

        public SalerInfo GetSalerByID(long sid)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(SalerStatement.GetSalersByKey, "Text"));
            command.AddInputParameter("@SID", DbType.Int64, sid);
            return command.ExecuteEntity<SalerInfo>();
        }

        public List<SalerInfo> GetSalerAll()
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(SalerStatement.GetSalers, "Text"));
            return command.ExecuteEntityList<SalerInfo>();
        }

        public List<SalerInfo> GetSaler(string name,int status, PagerInfo pager)
        {
            List<SalerInfo> result = new List<SalerInfo>();


            StringBuilder builder = new StringBuilder();

            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" AND Name LIKE '%'+@Name+'%' ");
            }

            if (status > -1)
            {
                builder.Append(" AND Status = @Status ");
            }

            string sql = SalerStatement.GetSalerAllPagerHeader + builder.ToString() + SalerStatement.GetSalerAllPagerFooter;

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sql, "Text"));

            if (!string.IsNullOrEmpty(name))
            {
                command.AddInputParameter("@Name", DbType.String, name);
            }
            if (status > -1)
            {
                command.AddInputParameter("@Status", DbType.Int32, status);
            }

            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);

            result = command.ExecuteEntityList<SalerInfo>();
            return result;
        }

        public int GetSalerCount(string name, int status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(SalerStatement.GetSalersAllCount);
            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" AND Name LIKE '%'+@Name+'%' ");
            }

            if (status > -1)
            {
                builder.Append(" AND Status = @Status ");
            }

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(builder.ToString(), "Text"));

            if (!string.IsNullOrEmpty(name))
            {
                command.AddInputParameter("@Name", DbType.String, name);
            }

            if (status > -1)
            {
                command.AddInputParameter("@Status", DbType.Int32, status);
            }


            var o = command.ExecuteScalar<object>();
            return Convert.ToInt32(o);
        }


        public int CreateSaler(SalerInfo saler)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(SalerStatement.CreateSaler, "Text"));
            command.AddInputParameter("@SCode", DbType.String, saler.SCode);
            command.AddInputParameter("@Name", DbType.String, saler.Name);
            command.AddInputParameter("@Sex", DbType.Int32, saler.Sex);
            command.AddInputParameter("@Birthday", DbType.DateTime, (saler.Birthday == null || saler.Birthday <= DateTime.Parse("1753-01-01")) ? DateTime.Parse("1753-01-01") : saler.Birthday);
            command.AddInputParameter("@CertificateType", DbType.String, saler.CertificateType);
            command.AddInputParameter("@CertificateNo", DbType.String, saler.CertificateNo);
            command.AddInputParameter("@WXCode", DbType.String, saler.WXCode);
            command.AddInputParameter("@Mobile", DbType.String, saler.Mobile);
            command.AddInputParameter("@Status", DbType.Int32, saler.Status);
            command.AddInputParameter("@AttachmentIDs", DbType.String, saler.ImageURL);
            command.AddInputParameter("@CreateDate", DbType.DateTime, saler.CreateDate);
            return command.ExecuteNonQuery();
        }


        public int ModifySaler(SalerInfo saler)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(SalerStatement.ModifySaler, "Text"));
            command.AddInputParameter("@SID", DbType.Int64, saler.SID);
            command.AddInputParameter("@SCode", DbType.String, saler.SCode);
            command.AddInputParameter("@Name", DbType.String, saler.Name);
            command.AddInputParameter("@Sex", DbType.Int32, saler.Sex);
            command.AddInputParameter("@Birthday", DbType.DateTime, (saler.Birthday == null || saler.Birthday <= DateTime.Parse("1753-01-01")) ? DateTime.Parse("1753-01-01") : saler.Birthday);
            command.AddInputParameter("@CertificateType", DbType.String, saler.CertificateType);
            command.AddInputParameter("@CertificateNo", DbType.String, saler.CertificateNo);
            command.AddInputParameter("@WXCode", DbType.String, saler.WXCode);
            command.AddInputParameter("@Mobile", DbType.String, saler.Mobile);
            command.AddInputParameter("@Status", DbType.Int32, saler.Status);
            command.AddInputParameter("@AttachmentIDs", DbType.String, saler.ImageURL);
            return command.ExecuteNonQuery();
        }

        public int RemoveSaler(long sid)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(SalerStatement.Remove, "Text"));
            command.AddInputParameter("@SID", DbType.Int64, sid);
            int result = command.ExecuteNonQuery();
            return result;
        }


        public int CreateSalerRelation(SalerRelation salerRelation)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(SalerStatement.CreateRelation, "Text"));
            command.AddInputParameter("@SalerCode", DbType.String, salerRelation.SalerCode);
            command.AddInputParameter("@CustomerCode", DbType.String, salerRelation.CustomerCode);
            command.AddInputParameter("@CustomerID", DbType.Int64, salerRelation.CustomerID);
            command.AddInputParameter("@SalerID", DbType.Int64, salerRelation.SalerID);
            command.AddInputParameter("@Status", DbType.Int32, salerRelation.Status);
            command.AddInputParameter("@SalerSource", DbType.String, salerRelation.SalerSource);
            return command.ExecuteNonQuery();
        }


        public List<SalerRelation> GetSalerCustomerBySalerCode(string salerCode)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(SalerStatement.GetSalerCustomerBySalerCode, "Text"));
            command.AddInputParameter("@SalerCode", DbType.String, salerCode);
            return command.ExecuteEntityList<SalerRelation>();
        }

        public List<SalerRelation> GetSalerCustomerByTelephone(string telephone)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(SalerStatement.GetSalerCustomerByTelephone, "Text"));
            command.AddInputParameter("@Mobile", DbType.String, telephone);
            return command.ExecuteEntityList<SalerRelation>();
        }

        
    }
}
