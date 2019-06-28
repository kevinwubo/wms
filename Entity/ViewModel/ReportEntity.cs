using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class ReportEntity
    {
        //序号	订单编号	订单属性	订单归属	发货仓库	供应商	下单日期	收货方	收货地址	货物重量	配送数量
        //序号	订单编号	订单属性	订单归属	发货仓库	下单日期	收货方	收货地址	货物重量	配送数量	应收总额
        //序号	订单编号	订单属性	订单归属	发货仓库	供应商	下单日期	收货方	收货地址	货物重量	配送数量	应付总额
        //序号	订单编号	订单属性	订单归属	发货仓库	供应商	下单日期	收货方	收货地址	货物重量	配送数量	应收总额	应付总额	利润
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
        /// 应付总额
        /// </summary>
        public decimal TotalPayFee { get; set; }
        /// <summary>
        /// 利润
        /// </summary>
        public decimal Profit { get; set; }
    }
}
