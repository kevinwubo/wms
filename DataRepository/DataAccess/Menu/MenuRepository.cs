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

namespace DataRepository.DataAccess.Menu
{
    public class MenuRepository : DataAccessBase
    {
        public List<MenuInfo> GetAllMenu()
        {
            List<MenuInfo> result = new List<MenuInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(MenuSatement.GetAllMenus, "Text"));
            result = command.ExecuteEntityList<MenuInfo>();
            return result;
        }

        public List<MenuInfo> GetMenusByRule(string name,int status)
        {
            List<MenuInfo> result = new List<MenuInfo>();
            string sqlText=MenuSatement.GetAllMenusByRule;
            if(!string.IsNullOrEmpty(name))
            {
                sqlText+=" AND MenuName LIKE '%'+@key+'%'";
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
           
            result = command.ExecuteEntityList<MenuInfo>();
            return result;
        }

        public MenuInfo GetMenuByKey(long mid)
        {
            MenuInfo result = new MenuInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(MenuSatement.GetMenuByKey, "Text"));
            command.AddInputParameter("@MenuID", DbType.String, mid);
            result = command.ExecuteEntity<MenuInfo>();
            return result;
        }

        public List<MenuInfo> GetMenuByKeys(string keys)
        {
            List<MenuInfo> result = new List<MenuInfo>();
            string sqlText = MenuSatement.GetMenuByKeys;
            sqlText = sqlText.Replace("#ids#", keys);
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            result = command.ExecuteEntityList<MenuInfo>();
            return result;
        }

        public int CreateNew(MenuInfo menu)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(MenuSatement.CreateNewMenu, "Text"));
            command.AddInputParameter("@MenuName", DbType.String, menu.MenuName);
            command.AddInputParameter("@URL", DbType.String, menu.URL);
            command.AddInputParameter("@Status", DbType.Int32, menu.Status);
            command.AddInputParameter("@Remark", DbType.String, menu.Remark);
            command.AddInputParameter("@GroupCode", DbType.String, menu.GroupCode);
            command.AddInputParameter("@PreFlag", DbType.String, menu.PreFlag);
            command.AddInputParameter("@SufFlag", DbType.String, menu.SufFlag);
            command.AddInputParameter("@CreateDate", DbType.DateTime, menu.CreateDate);

            return command.ExecuteNonQuery();
        }

        public int ModifyMenu(MenuInfo menu)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(MenuSatement.ModifyMenu, "Text"));
            command.AddInputParameter("@MenuID",DbType.Int64, menu.MenuID);
            command.AddInputParameter("@MenuName", DbType.String, menu.MenuName);
            command.AddInputParameter("@URL", DbType.String, menu.URL);
            command.AddInputParameter("@Status", DbType.Int32, menu.Status);
            command.AddInputParameter("@Remark", DbType.String, menu.Remark);
            command.AddInputParameter("@PreFlag", DbType.String, menu.PreFlag);
            command.AddInputParameter("@SufFlag", DbType.String, menu.SufFlag);
            command.AddInputParameter("@GroupCode", DbType.String, menu.GroupCode);

            return command.ExecuteNonQuery();
        }

        public int RemoveMenu(long mid)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(MenuSatement.Remove, "Text"));
            command.AddInputParameter("@MenuID", DbType.Int64, mid);
            int result=command.ExecuteNonQuery();
            return result;
        }
    }
}
