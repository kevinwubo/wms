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
    public class CarrierInfo
    {        
        /// <summary>
        /// 承运商ID
        /// </summary>
        [DataMapping("CarrierID", DbType.Int32)]
        public int CarrierID { get; set; }

        /// <summary>
        /// 承运商名称
        /// </summary>
        [DataMapping("CarrierName", DbType.String)]
        public string CarrierName { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        [DataMapping("CarrierShortName", DbType.String)]
        public string CarrierShortName { get; set; }

        /// <summary>
        /// 承运商编号
        /// </summary>
        [DataMapping("CarrierNo", DbType.String)]
        public string CarrierNo { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        [DataMapping("OperatorID", DbType.Int64)]
        public long OperatorID { get; set; }

        /// <summary>
        /// 承运商类型:个体驾驶员 三方物流 车队
        /// </summary>
        [DataMapping("Type", DbType.String)]
        public string Type { get; set; }
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
