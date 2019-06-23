using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class StringHelper
    {

        public static long ToLong(this string str, long defaultVal)
        {
            long result = 0;

            try
            {
                long.TryParse(str, out result);
                if (result == default(long))
                {
                    result = defaultVal;
                }
            }
            catch
            {
                result = defaultVal;
            }

            return result;
        }

        public static int ToInt(this string str, int defaultVal)
        {
            int result = 0;

            try
            {
                int.TryParse(str, out result);
                if (result == default(int))
                {
                    result = defaultVal;
                }
            }
            catch
            {
                result = defaultVal;
            }

            return result;
        }

        public static decimal ToDecimal(this string str, decimal defaultVal)
        {
            decimal result = 0;

            try
            {
                decimal.TryParse(str, out result);
                if (result == default(int))
                {
                    result = defaultVal;
                }
            }
            catch
            {
                result = defaultVal;
            }

            return result;
        }

        public static string getOrderType(string type)
        {
            if (!string.IsNullOrEmpty(type))
            {
                switch (type)
                {
                    case "YSDDA":
                        return OrderType.运输订单A.ToString();
                    case "YSDDB":
                        return OrderType.运输订单B.ToString();
                    case "CPDD":
                        return OrderType.仓配订单.ToString();
                    case "DBDD":
                        return OrderType.调拨订单.ToString();
                    case "BSDD":
                        return OrderType.报损订单.ToString();
                    default:
                        return type;
                }
            }
            return "";
        }

        /// <summary>
        /// 获取订单来源
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetOrderSource(string type)
        {
            string typename = "";
            if (!string.IsNullOrEmpty(type))
            {
                if (type.Equals("RtMart"))
                    typename = "大润发订单";
                else if (type.Equals("Carrefour"))
                    typename = "家乐福订单";
                else if (type.Equals("Lotus"))
                    typename = "卜蜂莲花订单";
                else if (type.Equals("YongHui"))
                    typename = "上蔬永辉订单";
                else if (type.Equals("Costa"))
                    typename = "Costa订单";
                else if (type.Equals("Regular"))
                    typename = "常规订单";
            }
            return typename;
        }
    }
}
