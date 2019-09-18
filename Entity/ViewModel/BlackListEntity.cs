using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class BlackListEntity
    {
        /// <summary>
        /// 自动增长ID       
        /// </summary>
        public int BlackID { get; set; }

        /// <summary>
        /// 黑名单类型 承运商：Carrier
        /// </summary>
        public string BlackType { get; set; }

        public string BlackTypeDesc { get; set; }

        /// <summary>
        /// 关联ID
        /// </summary>
        public long UnionID { get; set; }

        /// <summary>
        /// 关联名称
        /// </summary>
        public string UnionName { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public int OperatorID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 状态  (1:使用中 0：已删除)
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime ChangeDate { get; set; }
    }
}
