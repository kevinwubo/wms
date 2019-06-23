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
    public class BaseDataInfo
    {
        [DataMapping("ID", DbType.Int32)]
        public int ID { get; set; }

        [DataMapping("TypeCode", DbType.String)]
        public string TypeCode { get; set; }

        [DataMapping("PCode", DbType.String)]
        public string PCode { get; set; }

        [DataMapping("ValueInfo", DbType.String)]
        public string ValueInfo { get; set; }

        [DataMapping("Description", DbType.String)]
        public string Description { get; set; }

        [DataMapping("Status", DbType.Int32)]
        public int Status { get; set; }

        [DataMapping("CreateDate", DbType.DateTime)]
        public DateTime CreateDate { get; set; }
    }
}
