/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：CommonStatement
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：4/28/2018 10:05:43 AM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess
{
    public class DataAccessBase
    {

        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

        protected static DbCommand GetDbCommand(string commandText, string commandType)
        {
            DbCommand cmd = new SqlCommand();
            cmd.CommandText = commandText.Trim();
            cmd.CommandType = (CommandType)Enum.Parse(typeof(CommandType), commandType);

            return cmd;
        }
    }
}
