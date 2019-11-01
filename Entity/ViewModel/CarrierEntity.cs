using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class CarrierEntity
    {
        /// <summary>
        /// 承运商ID
        /// </summary>
        public int CarrierID { get; set; }

        /// <summary>
        /// 承运商名称
        /// </summary>
        public string CarrierName { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        public string CarrierShortName { get; set; }

        /// <summary>
        /// 承运商编号
        /// </summary>
        public string CarrierNo { get; set; }

        /// <summary>
        /// 承运商类型:个体驾驶员 三方物流 车队
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 操作人ID
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

        public string ContactJson { get; set; }


        public string contactJson { get; set; }
        /// <summary>
        /// 联系人信息
        /// </summary>
        public List<ContactEntity> listContact { get; set; }
    }
}
