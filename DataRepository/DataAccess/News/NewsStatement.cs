using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.News
{
    public  class NewsStatement
    {
        public static string InsertSql = @"INSERT INTO News(ChannelID,Title,zhaiyao,Content,Sort,Status,CreateDate,ModifyDate,Operator,AttachmentIDs)VALUES(@ChannelID,@Title,@zhaiyao,@Content,@Sort,@Status,@CreateDate,@ModifyDate,@Operator,@AttachmentIDs)";

        public static string SelectSql = @"SELECT * FROM News(NOLOCK) WHERE 1=1  ";

        public static string UpdateSql = @"UPDATE News SET ChannelID = @ChannelID,Title = @Title,zhaiyao = @zhaiyao,Content = @Content,Sort = @Sort,Status = @Status,ModifyDate = @ModifyDate,Operator = @Operator,AttachmentIDs = @AttachmentIDs WHERE ID=@ID";

        public static string DeleteSql = @"UPDATE News SET Status = 0 WHERE ID=@ID";

        public static string SelectTopSql = @"SELECT {0} * FROM News(NOLOCK) WHERE 1=1 ";
    }
}
