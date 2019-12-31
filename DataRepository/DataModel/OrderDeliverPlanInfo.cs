using Infrastructure.EntityFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataModel
{
    public class OrderDeliverPlanInfo
    {
        
        [DataMapping("PlanID", DbType.Int32)]
        public int PlanID { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        [DataMapping("OrderIDS", DbType.String)]
        public string OrderIDS { get; set; }

        /// <summary>
        /// 运单编号
        /// </summary>
        [DataMapping("DeliveryNo", DbType.String)]
        public string DeliveryNo { get; set; }
        
        /// <summary>
        /// 承运商编号
        /// </summary>
        [DataMapping("CarrierID", DbType.Int32)]
        public int CarrierID { get; set; }

        /// <summary>
        /// 承运商名称
        /// </summary>
        [DataMapping("CarrierName", DbType.String)]
        public string CarrierName { get; set; }


        /// <summary>
        /// 温区
        /// </summary>
        [DataMapping("Temp", DbType.String)]
        public string Temp { get; set; }

        /// <summary>
        /// 物流方式
        /// </summary>
        [DataMapping("DeliveryType", DbType.String)]
        public string DeliveryType { get; set; }


        /// <summary>
        /// 驾驶员姓名
        /// </summary>
        [DataMapping("DriverName", DbType.String)]
        public string DriverName { get; set; }

        /// <summary>
        /// 驾驶员联系电话
        /// </summary>
        [DataMapping("DriverTelephone", DbType.String)]
        public string DriverTelephone { get; set; }

        /// <summary>
        /// 驾驶员联系电话
        /// </summary>
        [DataMapping("CarModel", DbType.String)]
        public string CarModel { get; set; }

        /// <summary>
        /// 驾驶员联系电话
        /// </summary>
        [DataMapping("CarNo", DbType.String)]
        public string CarNo { get; set; }


        /// <summary>
        ///  油卡卡号
        /// </summary>
        [DataMapping("OilCardNo", DbType.String)]
        public string OilCardNo { get; set; }

        /// <summary>
        /// 油卡余额
        /// </summary>
        [DataMapping("OilCardBalance", DbType.Decimal)]
        public decimal OilCardBalance { get; set; }

        /// <summary>
        /// gps编号
        /// </summary>
        [DataMapping("GPSNo", DbType.String)]
        public string GPSNo { get; set; }

        /// <summary>
        /// 是否开票
        /// </summary>
        [DataMapping("NeedTicket", DbType.Boolean)]
        public bool NeedTicket { get; set; }

        /// <summary>
        /// 提货物流时间
        /// </summary>
        [DataMapping("DeliverDate", DbType.DateTime)]
        public DateTime DeliverDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMapping("Remark", DbType.String)]
        public string Remark { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        [DataMapping("OperatorID", DbType.Int32)]
        public int OperatorID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMapping("CreateDate", DbType.DateTime)]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [DataMapping("ChangeDate", DbType.DateTime)]
        public DateTime ChangeDate { get; set; }
    }
}
