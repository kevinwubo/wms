using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    [Serializable]
    public class ContactJsonEntity
    {
        /// <summary>
        /// 联系人信息
        /// </summary>
        public List<ContactJson> listContact { get; set; }

        
    }

    public class ContactJson
    {
        /// <summary>
        /// 联系人ID
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
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
