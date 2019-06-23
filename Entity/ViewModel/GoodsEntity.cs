using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class GoodsEntity
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public int GoodsID { get; set; }

        /// <summary>
        /// 商品类别
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        /// 所属客户ID
        /// </summary>
        public int CustomerID { get; set; }

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
        /// 重量
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// 尺寸
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Units { get; set; }

        /// <summary>
        /// 成本
        /// </summary>
        public decimal Costing { get; set; }

        /// <summary>
        /// 售价        
        /// </summary>
        public decimal SalePrice { get; set; }

        /// <summary>
        ///  托 
        /// </summary>
        public string Torr { get; set; }

        /// <summary>
        /// 保质期
        /// </summary>
        public string exDate { get; set; }

        /// <summary>
        /// 保质期单位
        /// </summary>
        public string exUnits { get; set; }

        /// <summary>
        /// 商品别名编号  
        /// </summary>
        public string AnotherNO { get; set; }

        /// <summary>
        ///商品别名名称      
        /// </summary>
        public string AnotherName { get; set; }


        /// <summary>
        /// 备注   
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 商品条形码
        /// </summary>
        public string BarCode { get; set; }

        /// <summary>
        /// 操作人ID   
        /// </summary>
        public long OperatorID { get; set; }

        /// <summary>
        ///  状态  (1:使用中 0：已删除)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ChangeDate { get; set; }

        //关联对象
        /// <summary>
        /// 默认客户
        /// </summary>
        public CustomerEntity customer { get; set; }
    }
}
