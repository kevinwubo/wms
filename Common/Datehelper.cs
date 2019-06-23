using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Datehelper
    {
        public static DateTime getDateTime(DateTime dt,int data, string units)
        {
            if (!string.IsNullOrEmpty(units))
            {
                if (units.Equals("天"))
                {
                    return dt.AddDays(data);
                }
                else if (units.Equals("月"))
                {
                    return dt.AddMonths(data);
                }
                else if (units.Equals("年"))
                {
                    return dt.AddYears(data);
                }
            }
            return dt;
        }
    }
}
