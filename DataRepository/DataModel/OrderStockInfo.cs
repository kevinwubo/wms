using Infrastructure.EntityFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataModel
{
    public class OrderStockInfo
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
        /// 库存ID
        /// </summary>
        [DataMapping("InventoryID", DbType.Int32)]
        public int InventoryID { get; set; }

        /// <summary>
        /// 待出库数量
        /// </summary>
        [DataMapping("Quantity", DbType.Int32)]
        public int Quantity { get; set; }

        /// <summary>
        /// 订单时间
        /// </summary>
        [DataMapping("OrderDate", DbType.DateTime)]
        public DateTime OrderDate { get; set; }
    }
}
