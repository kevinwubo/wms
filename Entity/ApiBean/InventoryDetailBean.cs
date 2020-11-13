using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ApiBean
{
    public class InventoryDetailBean
    {
        /// <summary>
        /// 库存ID
        /// </summary>
        public int InventoryID { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int GoodsID { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 仓库ID
        /// </summary>
        public int StorageID { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string StorageName { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNumber { get; set; }
        /// <summary>
        /// 生产时间
        /// </summary>
        public string ProductDate { get; set; }
        /// <summary>
        /// 入库时间
        /// </summary>
        public string InventoryDate { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public string ExpDate { get; set; }

        /// <summary>
        /// 仓位编号
        /// </summary>
        public String InventoryLocationNo { get; set; }
        
    }
}
