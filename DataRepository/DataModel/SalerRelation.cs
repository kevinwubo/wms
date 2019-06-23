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
    public class SalerRelation
    {
        [DataMapping("ID", DbType.Int64)]
        public long ID { get; set; }

        [DataMapping("CustomerID", DbType.Int64)]
        public long CustomerID { get; set; }

        [DataMapping("SalerID", DbType.Int64)]
        public long SalerID { get; set; }

        [DataMapping("CustomerCode", DbType.String)]
        public string CustomerCode { get; set; }

        [DataMapping("SalerCode", DbType.String)]
        public string SalerCode { get; set; }

        [DataMapping("Status", DbType.Int32)]
        public int Status { get; set; }

        [DataMapping("CreateDate", DbType.DateTime)]
        public DateTime CreateDate { get; set; }

        [DataMapping("SalerSource", DbType.String)]
        public string SalerSource { get; set; }
    }
}
