using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Role
{
    public class RoleStatement
    {
        public static string GetAllRoles = @"SELECT * FROM RoleInfo(NOLOCK)";

        public static string GetAllRolesByRule = @"SELECT * FROM RoleInfo(NOLOCK) WHERE 1=1 ";

        public static string GetRoleByKey = @"SELECT * FROM RoleInfo(NOLOCK) WHERE RoleID=@RoleID";

        public static string Remove = @"UPDATE RoleInfo SET Status=0 WHERE RoleID=@RoleID";

        public static string GetRoleByKeys = @"SELECT * FROM RoleInfo(NOLOCK) WHERE RoleID IN (#ids#)";

        public static string CreateNewRole = @"INSERT INTO [RoleInfo]([RoleName] ,[MenuIDS] ,[Status],[CreateDate]) VALUES (@RoleName,@MenuIDS,@Status,@CreateDate)";

        public static string ModifyRole = @"UPDATE [RoleInfo]
                                               SET [RoleName] =@RoleName
                                                  ,[MenuIDS] = @MenuIDS
                                                  ,[Status] = @Status
                                             WHERE RoleID=@RoleID";
    }
}
