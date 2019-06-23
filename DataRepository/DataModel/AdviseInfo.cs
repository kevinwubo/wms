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
    public class AdviseInfo
    {
        [DataMapping("AdviseID", DbType.Int64)]
        public long AdviseID { get; set; }

        [DataMapping("AdviseTitle", DbType.String)]
        public string AdviseTitle { get; set; }

        [DataMapping("Summary", DbType.String)]
        public string Summary { get; set; }

        [DataMapping("CustomerID", DbType.Int64)]
        public long CustomerID { get; set; }

        [DataMapping("CustomerName", DbType.String)]
        public string CustomerName { get; set; }

        [DataMapping("CustomerMobile", DbType.String)]
        public string CustomerMobile { get; set; }

        [DataMapping("DealStatus", DbType.Int32)]
        public int DealStatus { get; set; }

        [DataMapping("DealSummary", DbType.String)]
        public string DealSummary { get; set; }

        [DataMapping("Operator", DbType.Int64)]
        public long Operator { get; set; }

        [DataMapping("CreateDate", DbType.DateTime)]
        public DateTime CreateDate { get; set; }

        [DataMapping("ModifyDate", DbType.DateTime)]
        public DateTime ModifyDate { get; set; }
    }
}
