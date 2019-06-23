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
    public  class VerificationCodeInfo
    {
        [DataMapping("ID", DbType.Int64)]
        public long ID { get; set; }

        [DataMapping("Mobile", DbType.String)]
        public string Mobile { get; set; }

        [DataMapping("Email", DbType.String)]
        public string Email { get; set; }

        [DataMapping("VCode", DbType.String)]
        public string VCode { get; set; }

        [DataMapping("Status", DbType.Int32)]
        public int Status { get; set; }

        [DataMapping("DeadLine", DbType.DateTime)]
        public DateTime DeadLine { get; set; }

        [DataMapping("CreateDate", DbType.DateTime)]
        public DateTime CreateDate { get; set; }
    }
}
