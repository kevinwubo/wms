using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.User
{
    public class UserStatement
    {
        public static string GetAllUsers = @"SELECT * FROM UserInfo(NOLOCK)";

        public static string GetAllUsersByRule = @"SELECT * FROM UserInfo(NOLOCK) WHERE 1=1 ";

        public static string GetUserByKey = @"SELECT * FROM UserInfo(NOLOCK) WHERE UserID=@UserID";

        public static string Remove = @"UPDATE UserInfo SET Status=0 WHERE UserID=@UserID";

        public static string GetUserByKeys = @"SELECT * FROM UserInfo(NOLOCK) WHERE UserID IN (#ids#)";

        public static string CreateNewUser = @"INSERT INTO [UserInfo]([UserName],[NickName],[RoleIDs],[Password],[GroupIDs],[Status],[CreateDate]) VALUES (@UserName,@NickName,@RoleIDs,@Password,@GroupIDs,@Status,@CreateDate)";

        public static string ModifyUser = @"UPDATE [UserInfo]
                                               SET [UserName] =@UserName,[NickName]=@NickName,[RoleIDs]=@RoleIDs
                                                  ,[GroupIDs]=@GroupIDs
                                                  ,[Status] = @Status
                                             WHERE UserID=@UserID";
        public static string GetLoginUser = @"SELECT * FROM [UserInfo](NOLOCK) WHERE [UserName]=@UserName AND [Password]=@Password";

        public static string ModifyPassword = @"UPDATE [UserInfo] SET [Password]=@Password   WHERE UserID=@UserID";
    }
}
