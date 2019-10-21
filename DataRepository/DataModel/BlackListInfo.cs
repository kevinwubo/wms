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
        /// 原因
        /// </summary>
        [DataMapping("Remark", DbType.String)]
        public string Remark { get; set; }

        /// <summary>
        /// 企业统一信用编号/身份证
        /// </summary>
        [DataMapping("CardCode", DbType.String)]
        public string CardCode { get; set; }

        /// <summary>
        /// 合作状态  (1:使用中 0：已删除) 禁止合作状态，系统无法对该承运商建单
        /// </summary>
        [DataMapping("Status", DbType.Int32)]
        public int Status { get; set; }

        /// <summary>
        /// 甄别状态  (1:使用中 0：已删除) 系统建单需提示该订单承运商为黑名单甄别状态，谨慎合作
        /// </summary>
        [DataMapping("SubStatus", DbType.Int32)]
        public int SubStatus { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        [DataMapping("OperatorID", DbType.Int32)]
        public int OperatorID { get; set; }
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
