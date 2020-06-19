using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class ReportEntity
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单属性
        /// </summary>
        public string OrderType { get; set; }
        /// <summary>
        /// 订单归属
        /// </summary>
        public string OrderOwner { get; set; }
        /// <summary>
        /// 发货仓库名称
        /// </summary>
        public string SendStorageName { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string CarrierName { get; set; }
        /// <summary>
        /// 订单日期
        /// </summary>
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// 收货方名称
        /// </summary>
        public string ReceiverName { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string ReceiverAddress { get; set; }
        

        /// <summary>
        /// 货物重量
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// 配送数量
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 应收总额
        /// </summary>
        public decimal TotalReceiverFee { get; set; }

        /// <summary>
        /// 运输应收
        /// </summary>
        public decimal configPrice { get; set; }

        /// <summary>
        /// 装卸应收
        /// </summary>
        public decimal configHandInAmt { get; set; }

        /// <summary>
        /// 分拣应收
        /// </summary>
        public decimal configSortPrice { get; set; }


        /// <summary>
        /// 应付总额
        /// </summary>
        public decimal TotalPayFee { get; set; }

        /// <summary>
        /// 运输应付
        /// </summary>
        public decimal configCosting { get; set; }

        /// <summary>
        /// 装卸应付
        /// </summary>
        public decimal configHandOutAmt { get; set; }

        /// <summary>
        /// 分拣应付
        /// </summary>
        public decimal configSortCosting { get; set; }
        /// <summary>
        /// 利润
        /// </summary>
        public decimal Profit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }

        public string Remark2 { get; set; }

        /// <summary>
        /// 运单编号
        /// </summary>
        public string DeliveryNo { get; set; }

        //车牌、车型，驾驶员
        public string CarNo { get; set; }
        public string CarModel { get; set; }
        public string DriverName { get; set; }
    }

    /// <summary>
    /// 报表
    /// </summary>
    public class ReEntity
    {
        /// <summary>
        /// 所有应付总额
        /// </summary>
        public decimal TotalAllPayAmount { get; set; }

        /// <summary>
        /// 所有应收总额
        /// </summary>
        public decimal TotalllReceiverAmount { get; set; }

        /// <summary>
        /// 报表明细
        /// </summary>
        public List<ReportEntity> reportList { get; set; }
    }
}
