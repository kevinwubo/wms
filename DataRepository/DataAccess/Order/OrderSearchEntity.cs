using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Order
{
    public class OrderSearchEntity
    {
        public string OrderID { get; set; }

        public long CustomerID { get; set; }

        public long SupplierID { get; set; }

        public int Status { get; set; }
    }
}
