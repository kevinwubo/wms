using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class SalerEntity
    {
        public long SID { get; set; }
        public string SCode { get; set; }
        public string Name { get; set; }
        public int Sex { get; set; }
        public DateTime Birthday { get; set; }
        public string CertificateType { get; set; }
        public string CertificateTypeDesc { get; set; }
        public string CertificateNo { get; set; }
        public string WXCode { get; set; }
        public string Mobile { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }

        public string StatusDesc
        {
            get
            {
                string result = string.Empty;
                switch (Status)
                {
                    case 0: result = "不可用"; break;
                    case 1: result = "可用"; break;
                }

                return result;
            }
        }

        public string ImageURL { get; set; }

        public AttachmentEntity Attachment { get; set; }

        public List<CustomerExtendEntity> Customers { get; set; }
    }
}
