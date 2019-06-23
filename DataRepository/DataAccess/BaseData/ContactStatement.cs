using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.BaseData
{
    /// <summary>
    /// 联系人管理
    /// </summary>
    public class ContactStatement
    {
        public static string GetAllContact = @"SELECT * FROM wms_ContactInfo(NOLOCK)";

        public static string GetAllContactByRule = @"SELECT * FROM wms_ContactInfo(NOLOCK) WHERE 1=1 ";

        public static string GetContactByKey = @"SELECT * FROM wms_ContactInfo(NOLOCK) WHERE GroupID=@GroupID";

        public static string Remove = @"UPDATE wms_ContactInfo SET Status=0 WHERE ContactID=@ContactID";

        public static string GetContactByKeys = @"SELECT * FROM wms_ContactInfo(NOLOCK) WHERE ContactID IN (#ids#)";

        public static string CreateNewContact = @"INSERT INTO [wms_ContactInfo]([ContactName],[Mobile],[Telephone],[Email],[Remark],UnionType,UnionID)VALUES(@ContactName,@Mobile,@Telephone,@Email,@Remark,@UnionType,@UnionID)";

        public static string ModifyContact = @"UPDATE [wms_ContactInfo] SET [ContactName] = @ContactName,[Mobile] = @Mobile,[Telephone] = @Telephone,[Email] = @Email,[Remark] = @Remark,UnionType=@UnionType,UnionID=@UnionID WHERE ContactID=@ContactID";
    }
}
