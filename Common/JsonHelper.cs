/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：JsonHelper
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：5/2/2018 3:14:35 PM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization; 
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class JsonHelper
    {
        private static JsonSerializerSettings _jsonSettings;
        private static JsonSerializerSettings _jsonSettingsJs;


        static JsonHelper()
        {
            IsoDateTimeConverter datetimeConverter = new IsoDateTimeConverter();
            datetimeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

            _jsonSettings = new JsonSerializerSettings();
            _jsonSettings.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            _jsonSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            _jsonSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            _jsonSettings.Converters.Add(datetimeConverter);



            IsoDateTimeConverter dateConverter = new IsoDateTimeConverter();
            dateConverter.DateTimeFormat = "yyyy-MM-dd";

            _jsonSettingsJs = new JsonSerializerSettings();
            _jsonSettingsJs.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            _jsonSettingsJs.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            _jsonSettingsJs.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            _jsonSettingsJs.Converters.Add(dateConverter);
            _jsonSettingsJs.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        /// <summary>
        /// 将指定的对象序列化成 JSON 数据。
        /// </summary>
        /// <param name="obj">要序列化的对象。</param>
        /// <returns></returns>
        private static string ToJson<T>(T source, JsonSerializerSettings settings)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                if (null == source)
                    return "null";

                if (source is NameValueCollection)
                {
                    NameValueCollection nvc = source as NameValueCollection;
                    foreach (string key in nvc.Keys)
                    {
                        dict.Add(key, nvc[key]);
                    }
                    return JsonConvert.SerializeObject(dict, Formatting.None, settings);
                }

                return JsonConvert.SerializeObject(source, Formatting.None, settings);
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                return null;
            }
        }

        public static string ToJson<T>(T source, bool isCamelCase = false)
        {
            if (isCamelCase)
            {
                return ToJson(source, _jsonSettingsJs);
            }
            else
            {
                return ToJson(source, _jsonSettings);
            }
        }

        /// <summary>
        /// 将指定的 JSON 数据反序列化成指定对象。
        /// </summary>
        /// <typeparam name="T">对象类型。</typeparam>
        /// <param name="json">JSON 数据。</param>
        /// <returns></returns>
        public static T FromJson<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json, _jsonSettings);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public static object FromJson(string json, Type type)
        {
            try
            {
                var result = JsonConvert.DeserializeObject(json, type, _jsonSettings);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static T FromJson<T>(string json, string dateTimeFormat)
        {
            try
            {
                var datetimeConverter = new IsoDateTimeConverter();
                datetimeConverter.DateTimeFormat = dateTimeFormat;//"yyyy-MM-dd";
                var jsonSettings = new JsonSerializerSettings();
                jsonSettings.Converters.Add(datetimeConverter);

                return JsonConvert.DeserializeObject<T>(json, jsonSettings);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
