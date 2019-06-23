using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class BaseDataEntity
    {
        public int ID { get; set; }

        public string TypeCode { get; set; }

        public string PCode { get; set; }

        public string ValueInfo { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }

        public DateTime CreateDate { get; set; }
        
    }
}
