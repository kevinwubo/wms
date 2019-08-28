using Infrastructure.EntityFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataModel
{
    public class InventoryDetailInfo
    {
        /// <summary>
        /// 库存ID
        /// </summary>
        [DataMapping("InventoryDetailID", DbType.Int32)]
        public int InventoryDetailID { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        [DataMapping("GoodsID", DbType.Int32)]
        public int GoodsID { get; set; }

        /// <summary>
        /// 仓库ID
        /// </summary>
        [DataMapping("StorageID", DbType.Int32)]
        public int StorageID { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        [DataMapping("Quantity", DbType.Int32)]
        public int Quantity { get; set; }

        /// <summary>
        /// 商品所属客户ID
        /// </summary>
        [DataMapping("CustomerID", DbType.Int32)]
        public int CustomerID { get; set; }

        /// <summary>
        /// 库存类型(入库/出库)
        /// </summary>
        [DataMapping("InventoryType", DbType.String)]
        public string InventoryType { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        [DataMapping("BatchNumber", DbType.String)]
        public string BatchNumber { get; set; }
        /// <summary>
        /// 生产时间
        /// </summary>
        [DataMapping("ProductDate", DbType.DateTime)]
        public DateTime ProductDate { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        [DataMapping("InventoryDate", DbType.DateTime)]
        public DateTime InventoryDate { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        [DataMapping("OperatorID", DbType.Decimal)]
        public decimal UnitPrice { get; set; }


        /// <summary>
        /// 操作人ID
        /// </summary>
        [DataMapping("OperatorID", DbType.Int64)]
        public long OperatorID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMapping("Remark", DbType.String)]
        public string Remark { get; set; }

        /// <summary>
        /// 订单类型：运输订单A/运输订单B/仓配订单/调拨单/报损单
        /// </summary>
        [DataMapping("OrderType", DbType.String)]
        public string OrderType { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        [DataMapping("OrderNo", DbType.String)]
        public string OrderNo { get; set; }

        /// <summary>
        /// OrderID
        /// </summary>
        [DataMapping("OrderID", DbType.Int64)]
        public long OrderID { get; set; }

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
