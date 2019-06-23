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
    public class PriceSetInfo
    {
      
        /// <summary>
        /// 价格配置ID
        /// </summary>
        [DataMapping("PriceSetID", DbType.Int32)]
        public int PriceSetID { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        [DataMapping("CustomerID", DbType.Int32)]
        public int CustomerID { get; set; }

        /// <summary>
        /// 仓库ID
        /// </summary>
        [DataMapping("StorageID", DbType.Int32)]
        public int StorageID { get; set; }

        /// <summary>
        /// 承运商ID
        /// </summary>
        [DataMapping("CarrierID", DbType.Int32)]
        public int CarrierID { get; set; }


        /// <summary>
        /// 收货方类型 大仓、门店、经销商、其他
        /// </summary>
        [DataMapping("ReceivingType", DbType.String)]
        public string ReceivingType { get; set; }


        /// <summary>
        /// 收货方ID
        /// </summary>
        [DataMapping("ReceivingID", DbType.Int32)]
        public int ReceivingID { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        [DataMapping("TemType", DbType.String)]
        public string TemType { get; set; }

        /// <summary>
        /// 运输应收
        /// </summary>
        [DataMapping("configPrice", DbType.Decimal)]
        public decimal configPrice { get; set; }

        /// <summary>
        /// 装卸应收
        /// </summary>
        [DataMapping("configHandInAmt", DbType.Decimal)]
        public decimal configHandInAmt { get; set; }

        /// <summary>
        /// 分拣应收
        /// </summary>
        [DataMapping("configSortPrice", DbType.Decimal)]
        public decimal configSortPrice { get; set; }

        /// <summary>
        /// 运输应付
        /// </summary>
        [DataMapping("configCosting", DbType.Decimal)]
        public decimal configCosting { get; set; }

        /// <summary>
        /// 装卸应付
        /// </summary>
        [DataMapping("configHandOutAmt", DbType.Decimal)]
        public decimal configHandOutAmt { get; set; }

        /// <summary>
        /// 分拣应付
        /// </summary>
        [DataMapping("configSortCosting", DbType.Decimal)]
        public decimal configSortCosting { get; set; }

        /// <summary>
        /// 配送方式
        /// </summary>
        [DataMapping("DeliveryModel", DbType.String)]
        public string DeliveryModel { get; set; }

        /// <summary>
        /// 联系人ID
        /// </summary>
        [DataMapping("OperatorID", DbType.Int64)]
        public long OperatorID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMapping("Remark", DbType.String)]
        public string Remark { get; set; }

        /// <summary>
        /// 状态   (1:使用中 0：已删除)
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
