using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class CustomerExtendEntity
    {
        public long ID { get; set; }
        public long CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public string AttachmentIDs { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// 1--身份证 2--驾驶证
        /// </summary>
        public string CardType { get; set; }
        public string CardNo { get; set; }
        public int Channel { get; set; }
        public DateTime RegisterTime { get; set; }
        public int Status { get; set; }
        public DateTime AuditTime { get; set; }
        public long Auditor { get; set; }
        public string ModifyDate { get; set; }
        public long Operator { get; set; }

        public string WXCode { get; set; }

        public BaseDataEntity CardTypeInfo { get; set; }

        public List<AttachmentEntity> AttachmentInfos { get; set; }

        public string Base64 { get; set; }

        public string ChannelName {
            get {
                string result = string.Empty;
                switch (Channel)
                {
                    case 1: result = "APP注册"; break;
                    case 2: result = "ONLINE注册"; break;
                    case 3: result = "后台创建"; break;
                    case 4: result = "扫码注册"; break;
                }

                return result;
            }
        }

        public string StatusDesc
        {
            get
            {
                string result = string.Empty;
                switch (Status)
                {
                    case 0: result = "初始化"; break;
                    case 1: result = "待审核"; break;
                    case 2: result = "已审核"; break;
                    case 9: result = "注销"; break;
                }

                return result;
            }
        }
    }
}
