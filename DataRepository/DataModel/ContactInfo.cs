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
    public class ContactInfo
    {
        /// <summary>
        /// 联系人编号
        /// </summary>
        [DataMapping("ContactID", DbType.Int32)]
        public int ContactID { get; set; }

        /// <summary>
        /// 联系人姓名
        /// </summary>
        [DataMapping("ContactName", DbType.String)]
        public string ContactName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [DataMapping("Mobile", DbType.String)]
        public string Mobile { get; set; }

        /// <summary>
        /// 座机
        /// </summary>
        [DataMapping("Telephone", DbType.String)]
        public string Telephone { get; set; }

        /// <summary>
        /// 关联类型：承运商：Carrier 仓库：Storage  客户：Customer  门店：Store
        /// </summary>
        [DataMapping("UnionType", DbType.String)]
        public string UnionType { get; set; }

        /// <summary>
        /// 关联ID
        /// </summary>
        [DataMapping("UnionID", DbType.Int64)]
        public long UnionID { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [DataMapping("Email", DbType.String)]
        public string Email { get; set; }

        /// <summary>
        /// 状态   (1:使用中 0：已删除)
        /// </summary>
        [DataMapping("Status", DbType.Int32)]
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMapping("Remark", DbType.String)]
        public string Remark { get; set; }

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
