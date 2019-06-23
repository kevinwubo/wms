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
    public class SalerInfo
    {
        [DataMapping("SID", DbType.Int64)]
        public long SID { get; set; }
        [DataMapping("SCode", DbType.AnsiString)]
        public string SCode { get; set; }
        [DataMapping("Name", DbType.AnsiString)]
        public string Name { get; set; }
        [DataMapping("Sex", DbType.Int32)]
        public int Sex { get; set; }
        [DataMapping("Birthday", DbType.DateTime)]
        public DateTime Birthday { get; set; }
        [DataMapping("CertificateType", DbType.AnsiString)]
        public string CertificateType { get; set; }
        [DataMapping("CertificateNo", DbType.AnsiString)]
        public string CertificateNo { get; set; }
         [DataMapping("WXCode", DbType.AnsiString)]
        public string WXCode { get; set; }
         [DataMapping("Mobile", DbType.AnsiString)]
        public string Mobile { get; set; }
         [DataMapping("Status", DbType.Int32)]
        public int Status { get; set; }
         [DataMapping("CreateDate", DbType.DateTime)]
        public DateTime CreateDate { get; set; }
         [DataMapping("AttachmentIDs", DbType.AnsiString)]
         public string ImageURL { get; set; }
    }
}
