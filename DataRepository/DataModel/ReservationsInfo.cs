using Infrastructure.EntityFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataModel
{
    public class ReservationsInfo
    {
        [DataMapping("ID", DbType.Int64)]
        public long ID { get; set; }
        [DataMapping("CustomerID", DbType.Int64)]
        public long CustomerID { get; set; }

        [DataMapping("CustomerName", DbType.String)]
        public string CustomerName { get; set; }

        [DataMapping("RType", DbType.String)]
        //SJ:汽车试驾  ZL：汽车租赁 DZ:电桩预约
        public string  RType { get; set; }

        [DataMapping("PayType", DbType.String)]
        public string PayType { get; set; }

        [DataMapping("CarID", DbType.Int32)]
        public int CarID { get; set; }

        [DataMapping("LeaseTime", DbType.Int32)]
        public int LeaseTime { get; set; }

        [DataMapping("Price", DbType.Decimal)]
        public Decimal Price { get; set; }

        [DataMapping("Remark", DbType.String)]
        public String Remark { get; set; }

        [DataMapping("RDate", DbType.DateTime)]
        public DateTime RDate { get; set; }

        [DataMapping("Status", DbType.Int32)]
        public int Status { get; set; }

        [DataMapping("CreateDate", DbType.DateTime)]
        public DateTime CreateDate { get; set; }
    }
}
