using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class OrderDetailEntity
    {
        /// <summary>
        /// 明细ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 订单主表 ID
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int GoodsID { get; set; }

        /// <summary>
        /// 库存ID
        /// </summary>
        public int InventoryID { get; set; }

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
        public int Quantity { get; set; }

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
        public DateTime ProductDate { get; set; }

        /// <summary>
        /// 过期日期
        /// </summary>
        public DateTime ExceedDate { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime ChangeDate { get; set; }

        /// <summary>
        /// 可用库存数量  库存数量-待出库数量
        /// </summary>
        public int CanUseQuantity { get; set; }

        /// <summary>
        /// 待出库数量
        /// </summary>
        public int WaitQuantity { get; set; }

        #region 其他对象
        public GoodsEntity goods { get; set; }
        public InventoryEntity inventory { get; set; }
        #endregion
    }
}
