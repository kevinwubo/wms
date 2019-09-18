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
    public class BlackListRepository : DataAccessBase
    {
        public List<BlackListInfo> GetAllBlackList()
        {
            List<BlackListInfo> result = new List<BlackListInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BlackListStatement.GetAllBlackList, "Text"));
            result = command.ExecuteEntityList<BlackListInfo>();
            return result;
        }

        public BlackListInfo GetBlackListByKey(long ID)
        {
            BlackListInfo result = new BlackListInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BlackListStatement.GetBlackListByKey, "Text"));
            command.AddInputParameter("@BlackListID", DbType.String, ID);
            result = command.ExecuteEntity<BlackListInfo>();
            return result;
        }

        public long CreateNew(BlackListInfo BlackList)
        {
            //,,,Remark,OperatorID,,CreateTime,ChangeDate
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BlackListStatement.CreateNewBlackList, "Text"));
            command.AddInputParameter("@BlackType", DbType.String, BlackList.BlackType);
            command.AddInputParameter("@UnionID", DbType.Int32, BlackList.UnionID);
            command.AddInputParameter("@UnionName", DbType.String, BlackList.UnionName);
            command.AddInputParameter("@Remark", DbType.String, BlackList.Remark);
            command.AddInputParameter("@OperatorID", DbType.String, BlackList.OperatorID);
            command.AddInputParameter("@Status", DbType.Int32, BlackList.Status);
            command.AddInputParameter("@CreateDate", DbType.DateTime, BlackList.CreateDate);
            command.AddInputParameter("@ChangeDate", DbType.DateTime, BlackList.ChangeDate);
            var o = command.ExecuteScalar<object>();
            return Convert.ToInt64(o);
        }

        public int ModifyBlackList(BlackListInfo BlackList)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BlackListStatement.ModifyBlackList, "Text"));
            command.AddInputParameter("@BlackType", DbType.String, BlackList.BlackType);
            command.AddInputParameter("@UnionID", DbType.Int32, BlackList.UnionID);
            command.AddInputParameter("@UnionName", DbType.String, BlackList.UnionName);
            command.AddInputParameter("@Remark", DbType.String, BlackList.Remark);
            command.AddInputParameter("@OperatorID", DbType.String, BlackList.OperatorID);
            command.AddInputParameter("@Status", DbType.Int32, BlackList.Status);            
            command.AddInputParameter("@ChangeDate", DbType.DateTime, BlackList.ChangeDate);
            command.AddInputParameter("@BlackID", DbType.Int32, BlackList.BlackID);
            return command.ExecuteNonQuery();            
        }

        public int Delete(long ID)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BlackListStatement.Delete, "Text"));
            command.AddInputParameter("@ID", DbType.Int64, ID);
            int result = command.ExecuteNonQuery();
            return result;
        }


        #region 分页相关
        public List<BlackListInfo> GetAllBlackListInfoPager(PagerInfo pager)
        {
            List<BlackListInfo> result = new List<BlackListInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BlackListStatement.GetAllBlackListInfoPager, "Text"));
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);
            result = command.ExecuteEntityList<BlackListInfo>();
            return result;
        }


        public List<BlackListInfo> GetBlackListInfoByRule(int BlackListID, PagerInfo pager)
        {
            List<BlackListInfo> result = new List<BlackListInfo>();

            StringBuilder builder = new StringBuilder();

            if (BlackListID > -1)
            {
                builder.Append(" AND BlackListID=@BlackListID");
            }
            string sqlText = BlackListStatement.GetAllBlackListInfoByRulePagerHeader + builder.ToString() + BlackListStatement.GetAllBlackListInfoByRulePagerFooter;

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            if (BlackListID > -1)
            {
                command.AddInputParameter("@BlackListID", DbType.Int32, BlackListID);
            }
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);

            result = command.ExecuteEntityList<BlackListInfo>();
            return result;
        }

        public int GetBlackListCount(int BlackListID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(BlackListStatement.GetCount);
            if (BlackListID > -1)
            {
                builder.Append(" AND BlackListID=@BlackListID");
            }

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(builder.ToString(), "Text"));
            if (BlackListID > -1)
            {
                command.AddInputParameter("@BlackListID", DbType.Int32, BlackListID);
            }

            var o = command.ExecuteScalar<object>();
            return Convert.ToInt32(o);
        }

        #endregion
    }
}
