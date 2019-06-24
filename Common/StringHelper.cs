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
        /// 未配送 = 0,配送中 = 1, 已配送 = 2, 已回单 = 3, 订单完成 = 4, 已拒绝 = 5
        /// </summary>
        /// <param name="orderStatus"></param>
        /// <param name="UploadStatus"></param>
        /// <returns></returns>
        public static string GetOrderStatusDesc(int orderStatus)
        {
            switch (orderStatus)
            {
                case 0:
                    return OrderStatus.未配送.ToString();
                case 1:
                    return OrderStatus.配送中.ToString();
                case 2:
                    return OrderStatus.已配送.ToString();
                case 3:
                    return OrderStatus.已回单.ToString();
                case 4:
                    return OrderStatus.订单完成.ToString();
                case 5:
                    return OrderStatus.已拒绝.ToString();
                default:
                    return OrderStatus.未配送.ToString();
            }
        }


        /// <summary>
        /// 获取接单状态
        /// </summary>
        /// <param name="uploadStatus"></param>
        /// <returns></returns>
        public static string GetUploadStatusDesc(int uploadStatus)
        {
            switch (uploadStatus)
            {
                case 0:
                    return UploadStatus.未接单.ToString();
                case 1:
                    return UploadStatus.已接单.ToString();
                default:
                    return UploadStatus.未接单.ToString();
            }
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
