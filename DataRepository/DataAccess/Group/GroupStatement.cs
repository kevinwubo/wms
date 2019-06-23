/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：MenuSatement
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：4/28/2018 9:53:40 AM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Group
{
    public class GroupStatement
    {
        public static string GetAllGroups = @"SELECT * FROM GroupInfo(NOLOCK)";

        public static string GetAllGroupsByRule = @"SELECT * FROM GroupInfo(NOLOCK) WHERE 1=1 ";

        public static string GetGroupByKey = @"SELECT * FROM GroupInfo(NOLOCK) WHERE GroupID=@GroupID";

        public static string Remove = @"UPDATE GroupInfo SET Status=0 WHERE GroupID=@GroupID";

        public static string GetGroupByKeys = @"SELECT * FROM GroupInfo(NOLOCK) WHERE GroupID IN (#ids#)";

        public static string CreateNewGroup = @"INSERT INTO [GroupInfo]([GroupName] ,[MenuIDS] ,[Status],[CreateDate]) VALUES (@GroupName,@MenuIDS,@Status,@CreateDate)";

        public static string ModifyGroup = @"UPDATE [GroupInfo]
                                               SET [GroupName] =@GroupName
                                                  ,[MenuIDS] = @MenuIDS
                                                  ,[Status] = @Status
                                             WHERE GroupID=@GroupID";
    }
}
