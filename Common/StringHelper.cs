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
                    case "CPDD":
                        return "仓配订单";
                    case "DBDD":
                        return "调拨订单";
                    case "YSDDA":
                        return "运输订单A";
                    case "YSDDB":
                        return "运输订单B";
                    case "RKD":
                        return "入库单";
                    default:
                        return type;
                }
            }
            return "";
        }

        public static string getSubOrderType(string subType)
        {
            if (!string.IsNullOrEmpty(subType))
            {
                switch (subType)
                {
                    case "SHD":
                        return "送货单";
                    case "BSD":
                        return "补损单";
                    default:
                        return subType;
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

        public static string GetBlackTypeDesc(string type)
        {
            string desc = type;
            if (!string.IsNullOrEmpty(type))
            {
                if (type.Equals("GYS"))
                    desc = "供应商";
                else if (type.Equals("FWS"))
                    desc = "服务商";                
            }
            return desc;
        }

        /// <summary>
        /// 从字符串里随机得到，规定个数的字符串.
        /// </summary>
        /// <param name="allChar"></param>
        /// <param name="CodeCount"></param>
        /// <returns></returns>
        public static string GetRandomCode(int CodeCount)
        {
            string allChar = "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,i,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z"; 
            string[] allCharArray = allChar.Split(',');
            string RandomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < CodeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(temp * i * ((int)DateTime.Now.Ticks));
                }

                int t = rand.Next(allCharArray.Length - 1);

                while (temp == t)
                {
                    t = rand.Next(allCharArray.Length - 1);
                }

                temp = t;
                RandomCode += allCharArray[t];
            }
            return RandomCode;
        }
    }
}
