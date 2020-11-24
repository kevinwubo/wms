using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    /// <summary>
    /// 库存明细
    /// </summary>
    public class InventoryDetailEntity
    {
        /// <summary>
        /// 库存ID
        /// </summary>
        public int InventoryDetailID { get; set; }

        /// <summary>
        /// 库存ID
        /// </summary>
        public int InventoryID { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int GoodsID { get; set; }

        /// <summary>
        /// 仓库ID
        /// </summary>
        public int StorageID { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 商品所属客户ID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// 库存类型(入库/出库)
        /// </summary>
        public string InventoryType { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNumber { get; set; }
        /// <summary>
        /// 生产时间
        /// </summary>
        public DateTime ProductDate { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime InventoryDate { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice { get; set; }


        /// <summary>
        /// 操作人ID
        /// </summary>
        public long OperatorID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 订单类型：运输订单A/运输订单B/仓配订单/调拨单/报损单
        /// </summary>
        public string OrderType { get; set; }

        public string OrderTypeDesc { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ChangeDate { get; set; }


        //相关联对象
        /// <summary>
        /// 关联商品信息
        /// </summary>
        public GoodsEntity goods { get; set; }
        /// <summary>
        /// 关联客户信息
        /// </summary>
        public CustomerEntity customer { get; set; }
        /// <summary>
        /// 关联仓库
        /// </summary>
        public StorageEntity storages { get; set; }

    }    
}
