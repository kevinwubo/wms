using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class CustomerEntity
    {
        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 客户编号
        /// </summary>
        public string CustomerNo { get; set; }


        /// <summary>
        /// 客户编号
        /// </summary>
        public long OperatorID { get; set; }

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
        /// <summary>
        /// 联系人信息
        /// </summary>
        public List<ContactEntity> listContact { get; set; }
    }
}
