using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ConvertHelper
    {
        public static string GetOrderType(string type)
        {
            string str = "";
            switch (type)
            {
                case "CPDD":
                    return OrderType.仓配订单.ToString();
                case "DBDD":
                    return OrderType.调拨订单.ToString();
                case "YSDDA":
                    return OrderType.运输订单A.ToString();
                case "YSDDB":
                    return OrderType.运输订单B.ToString();
                default:
                    return "";
            }
            return str;            
        }
    }
}
