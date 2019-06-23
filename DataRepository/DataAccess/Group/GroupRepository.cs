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

namespace DataRepository.DataAccess.Group
{
    public class GroupRepository : DataAccessBase
    {
        public List<GroupInfo> GetAllGroup()
        {
            List<GroupInfo> result = new List<GroupInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(GroupStatement.GetAllGroups, "Text"));
            result = command.ExecuteEntityList<GroupInfo>();
            return result;
        }

        public List<GroupInfo> GetGroupByKeys(string keys)
        {
            List<GroupInfo> result = new List<GroupInfo>();
            if (!string.IsNullOrEmpty(keys))
            {
                string sqlText = GroupStatement.GetGroupByKeys;
                sqlText = sqlText.Replace("#ids#", keys);
                DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
                result = command.ExecuteEntityList<GroupInfo>();
            }

            return result;
        }

        public List<GroupInfo> GetGroupsByRule(string name, int status)
        {
            List<GroupInfo> result = new List<GroupInfo>();
            string sqlText = GroupStatement.GetAllGroupsByRule;
            if(!string.IsNullOrEmpty(name))
            {
                sqlText+=" AND GroupName LIKE '%'+@key+'%'";
            }
            if(status>-1)
            {
                sqlText+=" AND Status=@Status";
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

            result = command.ExecuteEntityList<GroupInfo>();
            return result;
        }

        public GroupInfo GetGroupByKey(long gid)
        {
            GroupInfo result = new GroupInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(GroupStatement.GetGroupByKey, "Text"));
            command.AddInputParameter("@GroupID", DbType.String, gid);
            result = command.ExecuteEntity<GroupInfo>();
            return result;
        }

        public int CreateNew(GroupInfo group)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(GroupStatement.CreateNewGroup, "Text"));
            command.AddInputParameter("@GroupName", DbType.String, group.GroupName);
            command.AddInputParameter("@MenuIDs", DbType.String, group.MenuIDs);
            command.AddInputParameter("@Status", DbType.Int32, group.Status);
            command.AddInputParameter("@CreateDate", DbType.DateTime, group.CreateDate);

            return command.ExecuteNonQuery();
        }

        public int ModifyGroup(GroupInfo group)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(GroupStatement.ModifyGroup, "Text"));
            command.AddInputParameter("@GroupID", DbType.Int64, group.GroupID);
            command.AddInputParameter("@GroupName", DbType.String, group.GroupName);
            command.AddInputParameter("@MenuIDs", DbType.String, group.MenuIDs);
            command.AddInputParameter("@Status", DbType.Int32, group.Status);

            return command.ExecuteNonQuery();
        }

        public int RemoveGroup(long gid)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(GroupStatement.Remove, "Text"));
            command.AddInputParameter("@GroupID", DbType.Int64, gid);
            int result=command.ExecuteNonQuery();
            return result;
        }
    }
}
