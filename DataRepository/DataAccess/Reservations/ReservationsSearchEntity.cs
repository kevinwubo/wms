using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Reservations
{
    public class ReservationsSearchEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long CustomerID { get; set; }

        /// <summary>
        /// 预定类型 DZ--预约充电桩
                    //SJ--预约试驾  
                    //ZL--预约购买汽车
        /// </summary>
        public string RType { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string PayType { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int CarID { get; set; }

        /// <summary>
        /// 预定日期
        /// </summary>
        public DateTime RDate { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }
}
