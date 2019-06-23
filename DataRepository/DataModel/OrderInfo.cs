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
    public class OrderInfo
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [DataMapping("OrderID", DbType.Int32)]
        public int OrderID { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        [DataMapping("OrderNo", DbType.String)]
        public string OrderNo { get; set; }
        /// <summary>
        /// 合并编号
        /// </summary>
        [DataMapping("MergeNo", DbType.String)]
        public string MergeNo { get; set; }
        /// <summary>
        /// 订单类型【运输订单A/运输订单B 仓配订单/调拨单/破损单】
        /// </summary>
        [DataMapping("OrderType", DbType.String)]
        public string OrderType { get; set; }

        /// <summary>
        /// 收货方ID
        /// </summary>
        [DataMapping("ReceiverID", DbType.Int32)]
        public int ReceiverID { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        [DataMapping("CustomerID", DbType.Int32)]
        public int CustomerID { get; set; }
        ///// <summary>
        ///// 商品ID
        ///// </summary>
        //public int GoodsID { get; set; }
        /// <summary>
        /// 发货仓库ID
        /// </summary>
        [DataMapping("SendStorageID", DbType.Int32)]
        public int SendStorageID { get; set; }

        /// <summary>
        /// 收货方仓库ID
        /// </summary>
        [DataMapping("ReceiverStorageID", DbType.Int32)]
        public int ReceiverStorageID { get; set; }
        /// <summary>
        /// 承运商ID
        /// </summary>
        [DataMapping("CarrierID", DbType.Int32)]
        public int CarrierID { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        [DataMapping("OrderDate", DbType.DateTime)]
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// 送达时间
        /// </summary>
        [DataMapping("SendDate", DbType.DateTime)]
        public DateTime SendDate { get; set; }

        /// <summary>
        /// 运输应收
        /// </summary>
        [DataMapping("configPrice", DbType.Decimal)]
        public decimal configPrice { get; set; }

        /// <summary>
        /// 装卸应收
        /// </summary>
        [DataMapping("configHandInAmt", DbType.Decimal)]
        public decimal configHandInAmt { get; set; }

        /// <summary>
        /// 分拣应收
        /// </summary>        
        [DataMapping("configSortPrice", DbType.Decimal)]
        public decimal configSortPrice { get; set; }

        /// <summary>
        /// 运输应付
        /// </summary>
        [DataMapping("configCosting", DbType.Decimal)]
        public decimal configCosting { get; set; }

        /// <summary>
        /// 装卸应付
        /// </summary>
        [DataMapping("configHandOutAmt", DbType.Decimal)]
        public decimal configHandOutAmt { get; set; }

        /// <summary>
        /// 分拣应付
        /// </summary>
        [DataMapping("configSortCosting", DbType.Decimal)]
        public decimal configSortCosting { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        [DataMapping("TempType", DbType.String)]
        public string TempType { get; set; }

        /// <summary>
        /// 订单状态
        /// 未配送 = 0, 已配送 = 1,已回单 2, 订单完成 = 3, 已拒绝 = 4
        /// </summary>
        [DataMapping("OrderStatus", DbType.Int32)]
        public int OrderStatus { get; set; }

        /// <summary>
        /// 接单状态 0：(未接单) 1：(已接单)
        /// </summary>
        [DataMapping("UploadStatus", DbType.Int32)]
        public int UploadStatus { get; set; }

        /// <summary>
        /// 状态   (1:使用中 0：已删除)
        /// </summary>
        [DataMapping("Status", DbType.Int32)]
        public int Status { get; set; }
        
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
        /// 附件IDS
        /// </summary>
        [DataMapping("AttachmentIDs", DbType.String)]
        public string AttachmentIDs { get; set; }

        /// <summary>
        /// 线路
        /// </summary>
        [DataMapping("LineID", DbType.Int32)]
        public int LineID { get; set; }
        /// <summary>
        /// 订单来源 （手工录入，大润发订单，家乐福订单，卜蜂莲花订单，上蔬永辉订单，Costa订单，常规订单）
        /// </summary>
        [DataMapping("OrderSource", DbType.String)]
        public string OrderSource { get; set; }

        /// <summary>
        /// 销售人员
        /// </summary>
        [DataMapping("SalesMan", DbType.String)]
        public string SalesMan { get; set; }

        /// <summary>
        /// 促销人员
        /// </summary>
        [DataMapping("PromotionMan", DbType.String)]
        public string PromotionMan { get; set; }

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
