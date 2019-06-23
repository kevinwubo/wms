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
    public class LineRepository : DataAccessBase
    {
        public List<LineInfo> GetAllLine()
        {
            List<LineInfo> result = new List<LineInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(LineStatement.GetAllLine, "Text"));
            result = command.ExecuteEntityList<LineInfo>();
            return result;
        }

        public LineInfo GetLineByKey(long ID)
        {
            LineInfo result = new LineInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(LineStatement.GetLineByKey, "Text"));
            command.AddInputParameter("@LineID", DbType.String, ID);
            result = command.ExecuteEntity<LineInfo>();
            return result;
        }

        public long CreateNew(LineInfo Line)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(LineStatement.CreateNewLine, "Text"));
            command.AddInputParameter("@LineID", DbType.Int32, Line.LineID);
            command.AddInputParameter("@ReceiverName", DbType.String, Line.ReceiverName);
            command.AddInputParameter("@Address", DbType.String, Line.Address);
            command.AddInputParameter("@Remark", DbType.String, Line.Remark);
            command.AddInputParameter("@OperatorID", DbType.String, Line.OperatorID);
            command.AddInputParameter("@CreateDate", DbType.DateTime, Line.CreateDate);
            command.AddInputParameter("@ChangeDate", DbType.DateTime, Line.ChangeDate);
            var o = command.ExecuteScalar<object>();
            return Convert.ToInt64(o);
        }

        public int ModifyLine(LineInfo Line)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(LineStatement.ModifyLine, "Text"));
            command.AddInputParameter("@ID", DbType.Int32, Line.ID);
            command.AddInputParameter("@LineID", DbType.Int32, Line.LineID);
            command.AddInputParameter("@ReceiverName", DbType.String, Line.ReceiverName);
            command.AddInputParameter("@Address", DbType.String, Line.Address);
            command.AddInputParameter("@Remark", DbType.String, Line.Remark);
            command.AddInputParameter("@OperatorID", DbType.String, Line.OperatorID);
            command.AddInputParameter("@ChangeDate", DbType.DateTime, Line.ChangeDate);
            return command.ExecuteNonQuery();            
        }

        public int Delete(long ID)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(LineStatement.Delete, "Text"));
            command.AddInputParameter("@ID", DbType.Int64, ID);
            int result = command.ExecuteNonQuery();
            return result;
        }


        #region 分页相关
        public List<LineInfo> GetAllLineInfoPager(PagerInfo pager)
        {
            List<LineInfo> result = new List<LineInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(LineStatement.GetAllLineInfoPager, "Text"));
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);
            result = command.ExecuteEntityList<LineInfo>();
            return result;
        }


        public List<LineInfo> GetLineInfoByRule(int LineID, PagerInfo pager)
        {
            List<LineInfo> result = new List<LineInfo>();

            StringBuilder builder = new StringBuilder();

            if (LineID > -1)
            {
                builder.Append(" AND LineID=@LineID");
            }
            string sqlText = LineStatement.GetAllLineInfoByRulePagerHeader + builder.ToString() + LineStatement.GetAllLineInfoByRulePagerFooter;

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            if (LineID > -1)
            {
                command.AddInputParameter("@LineID", DbType.Int32, LineID);
            }
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);

            result = command.ExecuteEntityList<LineInfo>();
            return result;
        }

        public int GetLineCount(int LineID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(LineStatement.GetCount);
            if (LineID > -1)
            {
                builder.Append(" AND LineID=@LineID");
            }

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(builder.ToString(), "Text"));
            if (LineID > -1)
            {
                command.AddInputParameter("@LineID", DbType.Int32, LineID);
            }

            var o = command.ExecuteScalar<object>();
            return Convert.ToInt32(o);
        }

        #endregion
    }
}
