using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class LineEntity
    {
        /// <summary>
        /// 自动增长ID       
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 线路号
        /// </summary>
        public int LineID { get; set; }

        /// <summary>
        /// 收货人名称
        /// </summary>
        public string ReceiverName { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public int OperatorID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

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
