using Infrastructure.EntityFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataModel
{
    public class BlackListInfo
    {
        /// <summary>
        /// 自动增长ID       
        /// </summary>
        [DataMapping("BlackID", DbType.Int32)]
        public int BlackID { get; set; }

        /// <summary>
        /// 黑名单类型 承运商：Carrier
        /// </summary>
        [DataMapping("BlackType", DbType.String)]
        public string BlackType { get; set; }

        /// <summary>
        /// 关联ID
        /// </summary>
        [DataMapping("UnionID", DbType.Int64)]
        public long UnionID { get; set; }

        /// <summary>
        /// 关联名称
        /// </summary>
        [DataMapping("UnionName", DbType.String)]
        public string UnionName { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        [DataMapping("OperatorID", DbType.Int32)]
        public int OperatorID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMapping("Remark", DbType.String)]
        public string Remark { get; set; }

        /// <summary>
        /// 状态  (1:使用中 0：已删除)
        /// </summary>
        [DataMapping("Status", DbType.Int32)]
        public int Status { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [DataMapping("CreateDate", DbType.DateTime)]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        [DataMapping("ChangeDate", DbType.DateTime)]
        public DateTime ChangeDate { get; set; }
    }
}
