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
        /// 原因
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 企业统一信用编号/身份证
        /// </summary>
        public string CardCode { get; set; }

        /// <summary>
        /// 合作状态  (1:使用中 0：已删除) 禁止合作状态，系统无法对该承运商建单
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 甄别状态  (1:使用中 0：已删除) 系统建单需提示该订单承运商为黑名单甄别状态，谨慎合作
        /// </summary>
        public int SubStatus { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public int OperatorID { get; set; }

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
