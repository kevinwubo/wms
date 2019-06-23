using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class ContactEntity
    {
        /// <summary>
        /// 联系人编号
        /// </summary>
        public int ContactID { get; set; }

        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 座机
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 关联类型：承运商：Carrier 仓库：Storage  客户：Customer  门店：Store
        /// </summary>
        public string UnionType { get; set; }

        /// <summary>
        /// 关联ID
        /// </summary>
        public long UnionID { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

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
    }

    [Serializable]
    public class ContactModel
    {
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address { get; set; }
    }
}
