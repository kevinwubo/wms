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
    public class GoodsInfo
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [DataMapping("GoodsID", DbType.Int32)]
        public int GoodsID { get; set; }

        /// <summary>
        /// 商品类别
        /// </summary>
        [DataMapping("TypeCode", DbType.String)]      
        public string TypeCode { get; set; }

        /// <summary>
        /// 所属客户ID
        /// </summary>
        [DataMapping("CustomerID", DbType.Int32)]      
        public int CustomerID { get; set; }

        /// <summary>
        /// 商品编号
        /// </summary>
        [DataMapping("GoodsNo", DbType.String)]      
        public string GoodsNo { get; set; }


        [DataMapping("GoodsName", DbType.String)]
        // 商品名称      
        public string GoodsName { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        [DataMapping("GoodsModel", DbType.String)]      
        public string GoodsModel { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        [DataMapping("Weight", DbType.String)]      
        public string Weight { get; set; }

        /// <summary>
        /// 尺寸
        /// </summary>
        [DataMapping("Size", DbType.String)]      
        public string Size { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [DataMapping("Units", DbType.String)]      
        public string Units { get; set; }

        /// <summary>
        /// 成本
        /// </summary>
        [DataMapping("Costing", DbType.Decimal)]      
        public decimal Costing { get; set; }

        /// <summary>
        /// 售价        
        /// </summary>
        [DataMapping("SalePrice", DbType.Decimal)]      
        public decimal SalePrice { get; set; }

        /// <summary>
        ///  托 
        /// </summary>
        [DataMapping("Torr", DbType.String)]  
        public string Torr { get; set; }

        /// <summary>
        /// 保质期
        /// </summary>
        [DataMapping("exDate", DbType.String)]             
        public string exDate { get; set; }

        /// <summary>
        /// 保质期单位
        /// </summary>
        [DataMapping("exUnits", DbType.String)]      
        public string exUnits { get; set; }

        /// <summary>
        /// 商品别名编号  
        /// </summary>
        [DataMapping("AnotherNO", DbType.String)]    
        public string AnotherNO { get; set; }

        /// <summary>
        ///商品别名名称      
        /// </summary>
        [DataMapping("AnotherName", DbType.String)]
        public string AnotherName { get; set; }


        /// <summary>
        /// 备注   
        /// </summary>
        [DataMapping("Remark", DbType.String)]   
        public string Remark { get; set; }
        
        /// <summary>
        /// 商品条形码   
        /// </summary>
        [DataMapping("BarCode", DbType.String)]
        public string BarCode { get; set; }
        /// <summary>
        /// 操作人ID   
        /// </summary>
        [DataMapping("OperatorID", DbType.Int64)]
        public long OperatorID { get; set; }

        /// <summary>
        ///  状态   (1:使用中 0：已删除)
        /// </summary>
        [DataMapping("Status", DbType.Int32)]
        public int Status { get; set; }

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
