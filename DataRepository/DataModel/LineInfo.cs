using Infrastructure.EntityFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataModel
{
    public class LineInfo
    {
        /// <summary>
        /// 自动增长ID       
        /// </summary>
        [DataMapping("ID", DbType.Int32)]
        public int ID { get; set; }

        /// <summary>
        /// 线路号
        /// </summary>
        [DataMapping("LineID", DbType.Int32)]
        public int LineID { get; set; }

        /// <summary>
        /// 收货人名称
        /// </summary>
        [DataMapping("ReceiverName", DbType.String)]
        public string ReceiverName { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [DataMapping("Address", DbType.String)]
        public string Address { get; set; }

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
