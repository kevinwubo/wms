using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public  class ReservationsEntity
    {
        public long ID { get; set; }
        public long CustomerID { get; set; }

        public string CustomerName { get; set; }
        /// <summary>
        /// SJ:汽车试驾  ZL：汽车租赁 DZ:电桩预约
        /// </summary>
        public string RType { get; set; }

        public string PayType { get; set; }

        public int CarID { get; set; }

        public int LeaseTime { get; set; }

        public Decimal Price { get; set; }

        public String Remark { get; set; }

        public DateTime RDate { get; set; }

        public int Status { get; set; }


        public CustomerEntity Customer { get; set; }


        public CarEntity Car { get; set; }

        public ChargingPileEntity ChargingPile { get; set; }


        public BaseDataEntity PayTypeInfo { get; set; }

        public string StatusDesc {
            get {

                string result = string.Empty;
                switch (Status)
                {
                    case 1: result = "已成交"; break;
                    case 0: result = "待处理"; break;
                    case -1: result = "已取消"; break;
                    default: result = "待处理"; break;
                }

                return result;
            }
        }


        public string RTypeDesc {

            get {
                string result = string.Empty;
                switch (RType)
                {
                    case "SJ": result = "汽车试驾"; break;
                    case "ZL": result = "汽车租赁"; break;
                    case "DZ": result = "电桩预约"; break;
                    default: result = "未知类型"; break;
                }

                return result;
            }
        }

    }
}
