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
    public class ReceiverInfo
    {
        /// <summary>
        /// 收货人ID 自动增长
        /// </summary>
        [DataMapping("ReceiverID", DbType.Int32)]
        public int ReceiverID { get; set; }

        /// <summary>
        /// 客户信息ID
        /// </summary>
        [DataMapping("CustomerID", DbType.Int32)]
        public int CustomerID { get; set; }

        /// <summary>
        /// 收货人编号
        /// </summary>
        [DataMapping("ReceiverNo", DbType.String)]
        public string ReceiverNo { get; set; }

        /// <summary>
        /// 收货人名称
        /// </summary>
        [DataMapping("ReceiverName", DbType.String)]
        public string ReceiverName { get; set; }

        /// <summary>
        /// 收货人类型：大仓、门店、经销商、其他
        /// </summary>
        [DataMapping("ReceiverType", DbType.String)]
        public string ReceiverType { get; set; }

        /// <summary>
        /// 省ID
        /// </summary>
        [DataMapping("ProvinceID", DbType.Int32)]
        public int ProvinceID { get; set; }

        /// <summary>
        /// 城市ID
        /// </summary>
        [DataMapping("CityID", DbType.Int32)]
        public int CityID { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [DataMapping("Address", DbType.String)]
        public string Address { get; set; }

        /// <summary>
        /// 操作员ID
        /// </summary>
        [DataMapping("OperatorID", DbType.Int64)]
        public long OperatorID { get; set; }

        /// <summary>
        /// Remark
        /// </summary>
        [DataMapping("Remark", DbType.String)]
        public string Remark { get; set; }

        /// <summary>
        /// 默认承运商编号
        /// </summary>
        [DataMapping("DefaultCarrierID", DbType.Int32)]
        public int DefaultCarrierID { get; set; }

         /// <summary>
        /// 默认仓库编号
        /// </summary>
        [DataMapping("DefaultStorageID", DbType.Int32)]
        public int DefaultStorageID { get; set; }
        

        /// <summary>
        /// 状态 1：使用中 0：已删除
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
