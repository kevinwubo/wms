using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class ReceiverEntity
    {
        /// <summary>
        /// 收货人ID 自动增长
        /// </summary>
        public int ReceiverID { get; set; }

        /// <summary>
        /// 客户信息ID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// 收货人编号
        /// </summary>
        public string ReceiverNo { get; set; }

        /// <summary>
        /// 收货人名称
        /// </summary>
        public string ReceiverName { get; set; }

        /// <summary>
        /// 收货人类型：大仓、门店、经销商、其他
        /// </summary>
        public string ReceiverType { get; set; }

        /// <summary>
        /// 省ID
        /// </summary>
        public int ProvinceID { get; set; }

        /// <summary>
        /// 城市ID
        /// </summary>
        public int CityID { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 操作员ID
        /// </summary>
        public long  OperatorID { get; set; }

        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 默认 承运商编号
        /// </summary>
        public int DefaultCarrierID { get; set; }

        /// <summary>
        /// 默认仓库编号
        /// </summary>
        public int DefaultStorageID { get; set; }

        public CarrierEntity Carrier { get; set; }
        public StorageEntity Storage { get; set; }
        /// <summary>
        /// 状态 1：使用中 0：已删除
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


        public string ContactJson { get; set; }
        /// <summary>
        /// 联系人信息
        /// </summary>
        public List<ContactEntity> listContact { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public Province province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public City city { get; set; }

        /// <summary>
        /// 默认客户
        /// </summary>
        public CustomerEntity customer { get; set; }

        /// <summary>
        /// URL参数
        /// </summary>
        public string Url { get; set; }
    }
}
