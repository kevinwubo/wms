using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class PriceSetEntity
    {
        /// <summary>
        /// 价格配置ID
        /// </summary>
        public int PriceSetID { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// 仓库ID
        /// </summary>
        public int StorageID { get; set; }

        /// <summary>
        /// 承运商ID
        /// </summary>
        public int CarrierID { get; set; }


        /// <summary>
        /// 收货方类型 大仓、门店、经销商、其他
        /// </summary>
        public string ReceivingType { get; set; }        
        /// <summary>
        /// 收货方ID
        /// </summary>
        public int ReceivingID { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        public string TemType { get; set; }

        /// <summary>
        /// 运输应收
        /// </summary>
        public decimal configPrice { get; set; }

        /// <summary>
        /// 装卸应收
        /// </summary>
        public decimal configHandInAmt { get; set; }

        /// <summary>
        /// 分拣应收
        /// </summary>
        public decimal configSortPrice { get; set; }

        /// <summary>
        /// 运输应付
        /// </summary>
        public decimal configCosting { get; set; }

        /// <summary>
        /// 装卸应付
        /// </summary>
        public decimal configHandOutAmt { get; set; }

        /// <summary>
        /// 分拣应付
        /// </summary>
        public decimal configSortCosting { get; set; }

        /// <summary>
        /// 配送方式
        /// </summary>
        public string DeliveryModel { get; set; }
        /// <summary>
        /// 联系人ID
        /// </summary>
        public long OperatorID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 状态   (1:使用中 0：已删除)
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

        /// <summary>
        /// 仓库信息
        /// </summary>
        public StorageEntity storage { get; set; }

        /// <summary>
        /// 承运商信息
        /// </summary>
        public CarrierEntity carrier { get; set; }

        /// <summary>
        /// 收货方信息
        /// </summary>
        public ReceiverEntity receiver { get; set; }
    }
}
