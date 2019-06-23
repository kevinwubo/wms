using DataRepository.DataModel;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.News
{
    public class NewsRepository : DataAccessBase
    {
        public int InsertNew(NewsInfo info)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(NewsStatement.InsertSql, "Text"));
            command.AddInputParameter("@ChannelID", DbType.Int32, info.ChannelID);
            command.AddInputParameter("@Title", DbType.String, info.Title);
            command.AddInputParameter("@zhaiyao", DbType.String, info.zhaiyao);
            command.AddInputParameter("@Content", DbType.String, info.Content);
            command.AddInputParameter("@AttachmentIDs", DbType.String, info.AttachmentIDs);
            command.AddInputParameter("@Sort", DbType.Int32, info.Sort);
            command.AddInputParameter("@Status", DbType.Int32, info.Status);
            command.AddInputParameter("@CreateDate", DbType.DateTime, DateTime.Now);
            command.AddInputParameter("@ModifyDate", DbType.DateTime, DateTime.Now);
            command.AddInputParameter("@Operator", DbType.Int64, info.Operator);
            return command.ExecuteNonQuery();
        }

        public int UpdateNew(NewsInfo info)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(NewsStatement.UpdateSql, "Text"));
            command.AddInputParameter("@ChannelID", DbType.Int32, info.ChannelID);
            command.AddInputParameter("@Title", DbType.String, info.Title);
            command.AddInputParameter("@zhaiyao", DbType.String, info.zhaiyao);
            command.AddInputParameter("@Content", DbType.String, info.Content);
            command.AddInputParameter("@AttachmentIDs", DbType.String, info.AttachmentIDs);
            command.AddInputParameter("@Sort", DbType.Int32, info.Sort);
            command.AddInputParameter("@Status", DbType.Int32, info.Status);
            command.AddInputParameter("@ModifyDate", DbType.DateTime, DateTime.Now);
            command.AddInputParameter("@Operator", DbType.Int64, info.Operator);
            command.AddInputParameter("@ID", DbType.Int32, info.ID);
            return command.ExecuteNonQuery();
        }

        public int DeleteNew(int ID)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(NewsStatement.DeleteSql, "Text"));
            command.AddInputParameter("@ID", DbType.Int32, ID);
            return command.ExecuteNonQuery();
        }

        public List<NewsInfo> GetNews(string title, int status)
        {
            List<NewsInfo> result = new List<NewsInfo>();
            string sqlText = NewsStatement.SelectSql;
            if (!string.IsNullOrEmpty(title))
            {
                sqlText += " AND Title like '%" + title + "'%";
            }
            if (status != -1)
            {
                sqlText += " AND Status = '" + status + "'";
            }

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            result = command.ExecuteEntityList<NewsInfo>();
            return result;
        }


        public List<NewsInfo> GetCountNews(int count)
        {
            List<NewsInfo> result = new List<NewsInfo>();
            string sqlText = string.Format(NewsStatement.SelectTopSql, count > 0 ? " TOP 3 " : "");

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            result = command.ExecuteEntityList<NewsInfo>();
            return result;
        }

        public NewsInfo GetNewsByID(int ID)
        {
            NewsInfo result = new NewsInfo();
            string sqlText = NewsStatement.SelectSql;
            if (ID>0)
            {
                sqlText += " AND ID = " + ID;
            }

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            result = command.ExecuteEntity<NewsInfo>();
            return result;
        }
    }
}
