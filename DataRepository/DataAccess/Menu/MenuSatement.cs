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

namespace DataRepository.DataAccess.Menu
{
    public class MenuSatement
    {
        public static string GetAllMenus = @"SELECT * FROM Menu(NOLOCK) ORDER BY GroupCode";

        public static string GetAllMenusByRule = @"SELECT * FROM Menu(NOLOCK) WHERE 1=1 ";

        public static string GetMenuByKey = @"SELECT * FROM Menu(NOLOCK) WHERE MenuID=@MenuID";

        public static string Remove = @"UPDATE Menu SET Status=0 WHERE MenuID=@MenuID";

        public static string GetMenuByKeys = @"SELECT * FROM Menu(NOLOCK) WHERE MenuID IN (#ids#)";

        public static string CreateNewMenu = @"INSERT INTO [Menu]([MenuName],[URL],[Status],[Remark],[PreFlag],[SufFlag],[CreateDate],[GroupCode]) VALUES (@MenuName,@URL,@Status,@Remark,@PreFlag,@SufFlag,@CreateDate,@GroupCode)";

        public static string ModifyMenu = @"UPDATE [Menu]
                                               SET [MenuName] =@MenuName
                                                  ,[URL] = @URL
                                                  ,[Status] = @Status
                                                  ,[Remark] = @Remark
                                                  ,[PreFlag] = @PreFlag
                                                  ,[GroupCode]=@GroupCode
                                                  ,[SufFlag] = @SufFlag
                                             WHERE MenuID=@MenuID";
    }
}
