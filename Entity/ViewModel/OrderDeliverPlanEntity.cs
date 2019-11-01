using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class OrderDeliverPlanEntity
    {
        public int PlanID { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderIDS { get; set; }

        /// <summary>
        /// 承运商编号
        /// </summary>
        public int CarrierID { get; set; }

        /// <summary>
        /// 承运商名称
        /// </summary>
        public string CarrierName { get; set; }


        /// <summary>
        /// 温区
        /// </summary>
        public string Temp { get; set; }

        /// <summary>
        /// 物流方式
        /// </summary>
        public string DeliveryType { get; set; }


        /// <summary>
        /// 驾驶员姓名
        /// </summary>
        public string DriverName { get; set; }

        /// <summary>
        /// 驾驶员联系电话
        /// </summary>
        public string DriverTelephone { get; set; }

        /// <summary>
        /// 驾驶员联系电话
        /// </summary>
        public string CarModel { get; set; }

        /// <summary>
        /// 驾驶员联系电话
        /// </summary>
        public string CarNo { get; set; }

        /// <summary>
        /// 提货物流时间
        /// </summary>
        public DateTime DeliverDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public int OperatorID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ChangeDate { get; set; }
    }
}
