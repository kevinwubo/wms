using Infrastructure.EntityFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataModel
{
        [Serializable]
    public class CustomerExtendInfo
    {
        [DataMapping("ID", DbType.Int64)]
        public long ID { get; set; }

        [DataMapping("CustomerID", DbType.Int64)]
        public long CustomerID { get; set; }

        [DataMapping("CustomerCode", DbType.String)]
        public string CustomerCode { get; set; }

        [DataMapping("AttachmentIDs", DbType.String)]
        public string AttachmentIDs { get; set; }

        [DataMapping("CustomerName", DbType.String)]
        public string CustomerName { get; set; }

        [DataMapping("Mobile", DbType.String)]
        public string Mobile { get; set; }

        [DataMapping("Email", DbType.String)]
        public string Email { get; set; }

        [DataMapping("CardType", DbType.String)]
        public string CardType { get; set; }

        [DataMapping("CardNo", DbType.String)]
        public string CardNo { get; set; }

        [DataMapping("Channel", DbType.String)]
        public int Channel { get; set; }

        [DataMapping("RegisterTime", DbType.DateTime)]
        public DateTime RegisterTime { get; set; }

        [DataMapping("Status", DbType.Int32)]
        public int Status { get; set; }

        [DataMapping("AuditTime", DbType.DateTime)]
        public DateTime AuditTime { get; set; }

        [DataMapping("Auditor", DbType.Int64)]
        public long Auditor { get; set; }

        [DataMapping("ModifyDate", DbType.DateTime)]
        public string ModifyDate { get; set; }

        [DataMapping("CreateDate", DbType.DateTime)]
        public string CreateDate { get; set; }

        [DataMapping("Operator", DbType.Int64)]
        public long Operator { get; set; }

        [DataMapping("Base64", DbType.String)]
        public string Base64 { get; set; }

        [DataMapping("WXCode", DbType.String)]
        public string WXCode { get; set; }


    }
}
