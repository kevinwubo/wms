using DataRepository.DataModel;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Role
{
    public class RoleRepository:DataAccessBase
    {
        public List<RoleInfo> GetAllRole()
        {
            List<RoleInfo> result = new List<RoleInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(RoleStatement.GetAllRoles, "Text"));
            result = command.ExecuteEntityList<RoleInfo>();
            return result;
        }

        public List<RoleInfo> GetRoleByKeys(string keys)
        {
            List<RoleInfo> result = new List<RoleInfo>();
            string sqlText = RoleStatement.GetRoleByKeys;
            sqlText = sqlText.Replace("#ids#", keys);
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            result = command.ExecuteEntityList<RoleInfo>();
            return result;
        }

        public List<RoleInfo> GetRolesByRule(string name, int status)
        {
            List<RoleInfo> result = new List<RoleInfo>();
            string sqlText = RoleStatement.GetAllRolesByRule;
            if (!string.IsNullOrEmpty(name))
            {
                sqlText += " AND RoleName LIKE '%'+@key+'%'";
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

            result = command.ExecuteEntityList<RoleInfo>();
            return result;
        }

        public RoleInfo GetRoleByKey(long rid)
        {
            RoleInfo result = new RoleInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(RoleStatement.GetRoleByKey, "Text"));
            command.AddInputParameter("@RoleID", DbType.String, rid);
            result = command.ExecuteEntity<RoleInfo>();
            return result;
        }

        public int CreateNew(RoleInfo role)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(RoleStatement.CreateNewRole, "Text"));
            command.AddInputParameter("@RoleName", DbType.String, role.RoleName);
            command.AddInputParameter("@MenuIDs", DbType.String, role.MenuIDs);
            command.AddInputParameter("@Status", DbType.Int32, role.Status);
            command.AddInputParameter("@CreateDate", DbType.DateTime, role.CreateDate);

            return command.ExecuteNonQuery();
        }

        public int ModifyRole(RoleInfo role)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(RoleStatement.ModifyRole, "Text"));
            command.AddInputParameter("@RoleID", DbType.Int32, role.RoleID);
            command.AddInputParameter("@RoleName", DbType.String, role.RoleName);
            command.AddInputParameter("@MenuIDs", DbType.String, role.MenuIDs);
            command.AddInputParameter("@Status", DbType.Int32, role.Status);

            return command.ExecuteNonQuery();
        }

        public int RemoveRole(int rid)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(RoleStatement.Remove, "Text"));
            command.AddInputParameter("@RoleID", DbType.Int32, rid);
            int result = command.ExecuteNonQuery();
            return result;
        }
    }
}
