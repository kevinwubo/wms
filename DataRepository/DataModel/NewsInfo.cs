using Infrastructure.EntityFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataModel
{
    public class NewsInfo
    {
        [DataMapping("ID", DbType.Int64)]
        public long ID { get; set; }

        [DataMapping("ChannelID", DbType.Int32)]
        public int ChannelID { get; set; }

        [DataMapping("Title", DbType.String)]
        public String Title { get; set; }

        [DataMapping("AttachmentIDs", DbType.String)]
        public string AttachmentIDs { get; set; }

        [DataMapping("zhaiyao", DbType.String)]
        public String zhaiyao { get; set; }

        [DataMapping("Content", DbType.String)]
        public String Content { get; set; }

        [DataMapping("Sort", DbType.Int32)]
        public int Sort { get; set; }

        [DataMapping("Status", DbType.Int32)]
        public int Status { get; set; }

        [DataMapping("CreateDate", DbType.DateTime)]
        public DateTime CreateDate { get; set; }

        [DataMapping("ModifyDate", DbType.DateTime)]
        public DateTime ModifyDate { get; set; }

        [DataMapping("Operator", DbType.Int64)]
        public long Operator { get; set; }
    }
}
