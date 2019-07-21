using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class OrderEntity
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 合并编号
        /// </summary>
        public string MergeNo { get; set; }
        /// <summary>
        /// 订单类型【运输订单A/运输订单B 仓配订单/调拨单/破损单】
        /// </summary>
        public string OrderType { get; set; }

        public string OrderTypeDesc { get; set; }

        /// <summary>
        /// 收货门店ID
        /// </summary>
        public int ReceiverID { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustomerID { get; set; }
        ///// <summary>
        ///// 商品ID
        ///// </summary>
        //public int GoodsID { get; set; }
        /// <summary>
        /// 发货仓库ID
        /// </summary>
        public int SendStorageID { get; set; }

        /// <summary>
        /// 收货仓库ID
        /// </summary>
        public int ReceiverStorageID { get; set; }
        /// <summary>
        /// 承运商ID
        /// </summary>
        public int CarrierID { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public string OrderDate { get; set; }
        /// <summary>
        /// 送货时间
        /// </summary>
        public DateTime SendDate { get; set; }

        /// <summary>
        /// 送达时间
        /// </summary>
        public DateTime ArriverDate { get; set; }
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
        /// 温度
        /// </summary>
        public string TempType { get; set; }

        /// <summary>
        /// 订单状态
        /// 未配送 = 0,配送中 = 1, 已配送 = 2, 已回单 = 3, 订单完成 = 4, 已拒绝 = 5
        /// /// </summary>
        public int OrderStatus { get; set; }

        /// <summary>
        /// 订单状态描述
        /// 未配送 = 0,配送中 = 1, 已配送 = 2, 已回单 = 3, 订单完成 = 4, 已拒绝 = 5
        /// /// </summary>
        public string OrderStatusDesc { get; set; }

        /// <summary>
        /// 接单状态 0：(未接单) 1：(已接单)
        /// </summary>
        public int UploadStatus { get; set; }

        /// <summary>
        /// 接单状态描述 0：(未接单) 1：(已接单)
        /// </summary>
        public string UploadStatusDesc { get; set; }

        /// <summary>
        /// 状态   (1:使用中 0：已删除)
        /// </summary>
        public int Status { get; set; }

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

        /// <summary>
        /// 附件 图片
        /// </summary>
        public string AttachmentIDs { get; set; }

        /// <summary>
        /// 订单来源 （手工录入，大润发订单，家乐福订单，卜蜂莲花订单，上蔬永辉订单，Costa订单，常规订单）
        /// </summary>
        public string OrderSource  { get; set; }

        /// <summary>
        /// 销售人员
        /// </summary>
        public string SalesMan { get; set; }

        /// <summary>
        /// 促销人员
        /// </summary>
        public string PromotionMan { get; set; }


        /// <summary>
        /// 送货单 补损单
        /// </summary>
        public string TypeDesc { get; set; }
        /// <summary>
        /// 公司名称  
        /// </summary>
        public string CompanyName { get; set; }

        #region 其他属性
        /// <summary>
        /// 订单明细对象
        /// </summary>
        public string orderDetailJson { get; set; }

        public CustomerEntity customer { get; set; }

        public CarrierEntity carrier { get; set; }

        public ReceiverEntity receiver { get; set; }

        public StorageEntity sendstorage { get; set; }

        public UserEntity user { get; set; }

        public OrderContactEntity contact { get; set; }
        /// <summary>
        /// 订单明细
        /// </summary>
        public List<OrderDetailEntity> orderDetailList { get; set; }


        #endregion
    }
    [Serializable]
    public class OrderContactEntity
    {
        public string name { get; set; }

        public string contact { get; set; }
        public string telephone { get; set; }
        public string address { get; set; }

    }

    [Serializable]
    public class OrderJsonEntity
    {
        public List<OrderDetailJsonEntity> listOrderDetail { get; set; }
    }

    public class OrderDetailJsonEntity
    {
        /// <summary>
        /// 订单明细ID
        /// </summary>
        public int ID { get; set; }

        public int OrderID { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public string GoodsID { get; set; }
        /// <summary>
        /// 库存ID
        /// </summary>
        public string InventoryID { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public string GoodsNo { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 商品规格
        /// </summary>
        public string GoodsModel { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string Quantity { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Units { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// 总重量
        /// </summary>
        public string TotalWeight { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNumber { get; set; }
        /// <summary>
        /// 生产日期
        /// </summary>
        public string ProductDate { get; set; }
        /// <summary>
        /// 过期日期
        /// </summary>
        public string ExceedDate { get; set; }
    }

    public class ImportIDSEntity
    {
        /// <summary>
        /// 发货单ID
        /// </summary>
        public string SHDIds { get; set; }

        /// <summary>
        /// 补损单ID
        /// </summary>
        public string BSDIds { get; set; }
    }

    public class ImportOrderEntity
    {
        /// <summary>
        /// 导入类型 送货单 补损单
        /// </summary>
        public string ImportType { get; set; }
        //送货单号								
        /// <summary>
        /// 日期
        /// </summary>
        public string OrderDate { get; set; }
        ///// <summary>
        ///// 到货日期
        ///// </summary>
        //public string SendDate { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 门店编号
        /// </summary>
        public string ShopNo { get; set; }
        /// <summary>
        /// 门店
        /// </summary>
        public string ShopName { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 商品编号
        /// </summary>
        public string GoodsNo { get; set; }
        /// <summary>
        /// 品名
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 包装规格
        /// </summary>
        public string GoodsModel { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Units { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string Quantity { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 条码
        /// </summary>
        public string BarCode { get; set; }
        /// <summary>
        /// 销售人员
        /// </summary>
        public string SalesMan { get; set; }
        /// <summary>
        /// 促销人员
        /// </summary>
        public string PromotionMan { get; set; }

        /// <summary>
        /// 预约时间 到货日期
        /// </summary>
        public string YyDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }


    public class ImportInventoryEntity
    {
        /// <summary>
        /// 所属客户
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 仓库编号
        /// </summary>
        public string StorageNo { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        public string StorageName { get; set; }

        /// <summary>
        /// 商品编号
        /// </summary>
        public string GoodsNo { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string Models { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Units { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNumber { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        public string ProductDate { get; set; }

        /// <summary>
        /// 到期日期
        /// </summary>
        public string ExitDate { get; set; }

        /// <summary>
        /// 余量
        /// </summary>
        public string Quantity { get; set; }
    }
    //										

}
