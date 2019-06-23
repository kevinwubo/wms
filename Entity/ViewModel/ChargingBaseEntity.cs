using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class ChargingBaseEntity
    {
        public int ChargeBaseID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ChargeFee { get; set; }
        public string ServerFee { get; set; }
        public string ParkFee { get; set; }
        public int ChargeNum { get; set; }
        public string PayType { get; set; }
        public string Address { get; set; }
        public string Coordinate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public int CityID { get; set; }

        public int IsUse { get; set; }

        public string imageUrl { get; set; }

        public string UseStatus {
            get {
                string result = "正常使用";
                if (IsUse == 0)
                {
                    result = "停用";
                }

                return result;
            }
        }

        public City CityInfo { get; set; }

        public List<BaseDataEntity> PayTypeName { get; set; }

        /// <summary>
        /// 电站下面电桩数据
        /// </summary>
        public List<ChargingPileEntity> chargingPile { get; set; }

        
    }
}
