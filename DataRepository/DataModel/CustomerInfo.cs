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
    public class CustomerInfo
    {
        /// <summary>
        /// 客户ID
        /// </summary>
        [DataMapping("CustomerID", DbType.Int32)]
        public int CustomerID { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        [DataMapping("CustomerName", DbType.String)]
        public string CustomerName { get; set; }

        /// <summary>
        /// 客户编号
        /// </summary>
        [DataMapping("CustomerNo", DbType.String)]
        public string CustomerNo { get; set; }


        /// <summary>
        /// 客户编号
        /// </summary>
        [DataMapping("OperatorID", DbType.Int64)]
        public long OperatorID { get; set; }

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
