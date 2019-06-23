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
    public class ChargingBaseInfo
    {
        [DataMapping("ChargeBaseID", DbType.Int32)]
        public int ChargeBaseID { get; set; }

        [DataMapping("Name", DbType.String)]
        public string Name { get; set; }

        [DataMapping("Code", DbType.String)]
        public string Code { get; set; }

        [DataMapping("ChargeFee", DbType.String)]
        public String ChargeFee { get; set; }

        [DataMapping("ServerFee", DbType.String)]
        public String ServerFee { get; set; }

        [DataMapping("ParkFee", DbType.String)]
        public String ParkFee { get; set; }

        [DataMapping("ChargeNum", DbType.Int32)]
        public int ChargeNum { get; set; }

        [DataMapping("PayType", DbType.String)]
        public string PayType { get; set; }

        [DataMapping("Address", DbType.String)]
        public string Address { get; set; }

        [DataMapping("Coordinate", DbType.String)]
        public string Coordinate { get; set; }

        [DataMapping("StartTime", DbType.String)]
        public string StartTime { get; set; }

        [DataMapping("EndTime", DbType.String)]
        public string EndTime { get; set; }

        [DataMapping("CreateDate", DbType.DateTime)]
        public DateTime CreateDate { get; set; }

        [DataMapping("IsUse", DbType.Int32)]
        public int IsUse { get; set; }

        [DataMapping("CityID", DbType.Int32)]
        public int CityID { get; set; }
    }
}
