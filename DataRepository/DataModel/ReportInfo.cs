using Infrastructure.EntityFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataModel
{
    public class ReportInfo
    {
        /// <summary>
        /// 序号
        /// </summary>
        [DataMapping("ID", DbType.Int32)]
        public int ID { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        [DataMapping("OrderNo", DbType.String)]
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单属性
        /// </summary>
        [DataMapping("OrderType", DbType.String)]
        public string OrderType { get; set; }
        /// <summary>
        /// 订单归属
        /// </summary>
        [DataMapping("OrderOwner", DbType.String)]
        public string OrderOwner { get; set; }
        /// <summary>
        /// 发货仓库名称
        /// </summary>
        [DataMapping("SendStorageName", DbType.String)]
        public string SendStorageName { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        [DataMapping("CarrierName", DbType.String)]
        public string CarrierName { get; set; }
        /// <summary>
        /// 订单日期
        /// </summary>
        [DataMapping("OrderDate", DbType.String)]
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// 收货方名称
        /// </summary>
        [DataMapping("ReceiverName", DbType.String)]
        public string ReceiverName { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        [DataMapping("ReceiverAddress", DbType.String)]
        public string ReceiverAddress { get; set; }
        /// <summary>
        /// 货物重量
        /// </summary>
        [DataMapping("Weight", DbType.Int32)]
        public int Weight { get; set; }
        /// <summary>
        /// 配送数量
        /// </summary>
        [DataMapping("Quantity", DbType.Int32)]
        public int Quantity { get; set; }
        /// <summary>
        /// 应收总额
        /// </summary>
        [DataMapping("TotalReceiverFee", DbType.Decimal)]
        public decimal TotalReceiverFee { get; set; }
        /// <summary>
        /// 应付总额
        /// </summary>
        [DataMapping("TotalPayFee", DbType.Decimal)]
        public decimal TotalPayFee { get; set; }
        /// <summary>
        /// 利润
        /// </summary>
        [DataMapping("Profit", DbType.Decimal)]
        public decimal Profit { get; set; }
    }
}
