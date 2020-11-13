using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ApiBean
{
    public class GoodsBean
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public int GoodsID { get; set; }


        /// <summary>
        /// 所属客户ID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string CustomerName { get; set; }

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
        public string GoodsModel { get; set; }


        /// <summary>
        /// 尺寸
        /// </summary>
        public string Size { get; set; }


        /// <summary>
        /// 保质期
        /// </summary>
        public string exDate { get; set; }
    }
}
