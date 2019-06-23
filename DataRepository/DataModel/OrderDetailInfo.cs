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
    public class OrderDetailInfo
    {
        /// <summary>
        /// 明细ID
        /// </summary>
        [DataMapping("ID", DbType.Int32)]
        public int ID { get; set; }

        /// <summary>
        /// 订单主表 ID
        /// </summary>
        [DataMapping("OrderID", DbType.Int32)]
        public int OrderID { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        [DataMapping("GoodsID", DbType.Int32)]
        public int GoodsID { get; set; }

        /// <summary>
        /// 库存ID
        /// </summary>
        [DataMapping("InventoryID", DbType.Int32)]
        public int InventoryID { get; set; }

        /// <summary>
        /// 商品编号
        /// </summary>
        [DataMapping("GoodsNo", DbType.String)]
        public string GoodsNo { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [DataMapping("GoodsName", DbType.String)]
        public string GoodsName { get; set; }

        /// <summary>
        /// 商品规格
        /// </summary>
        [DataMapping("GoodsModel", DbType.String)]
        public string GoodsModel { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DataMapping("Quantity", DbType.Int32)]
        public int Quantity { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [DataMapping("Units", DbType.String)]
        public string Units { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        [DataMapping("Weight", DbType.String)]
        public string Weight { get; set; }

        /// <summary>
        /// 总重量
        /// </summary>
        [DataMapping("TotalWeight", DbType.String)]
        public string TotalWeight { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        [DataMapping("BatchNumber", DbType.String)]
        public string BatchNumber { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        [DataMapping("ProductDate", DbType.DateTime)]
        public DateTime ProductDate { get; set; }

        /// <summary>
        /// 过期日期
        /// </summary>
        [DataMapping("ExceedDate", DbType.DateTime)]
        public DateTime ExceedDate { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [DataMapping("CreateDate", DbType.DateTime)]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        [DataMapping("ChangeDate", DbType.DateTime)]
        public DateTime ChangeDate { get; set; }
    }
}
