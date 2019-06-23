using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public class WebHelper
    {
        public static HttpCookie CreateSingleValueCookie(string cookieKey,string val,int timespan)
        {
            HttpCookie SingleValueCookie = new HttpCookie(cookieKey);
            SingleValueCookie.Value = val;
            //SingleValueCookie.Domain = "/";
            if (timespan > 0)
            {
                SingleValueCookie.Expires = DateTime.Now.AddMinutes(timespan);
            }           
            return SingleValueCookie;
        }

        public static HttpCookie CreateMultiValueCookie(string cookieKey, Dictionary<string, string> dict, int timespan)
        {
            HttpCookie MultiValueCookie = new HttpCookie(cookieKey);
            foreach (KeyValuePair<string, string> kp in dict)
            {
                MultiValueCookie.Values[kp.Key] = kp.Value;
            }
            //MultiValueCookie.Domain = "/";
            if (timespan > 0)
            {
                MultiValueCookie.Expires = DateTime.Now.AddMinutes(timespan);
            }   

            return MultiValueCookie;
        }


    }
}
