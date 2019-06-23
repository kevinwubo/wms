using Common;
using Entity.ViewModel;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.ApiBiz
{
    public class SendSMSService
    {
        /// <summary>
        /// 用户注册验证码发送
        /// </summary>
        /// <param name="telephone"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string SendVCodeMess(string telephone, int timeout)
        {
            string vcode = ValidateCode.CreateValidateCode(6);
            string Content = "您的验证码是" + vcode + "，欢迎使用国车APP服务。如果非本人操作，请忽略。";
            SendSMSMess(telephone, Content);
            //插入短信息到表结构
            AddVerificationCode(telephone, vcode, timeout);
            return vcode;
        }

        /// <summary>
        /// 注册用户发送密码信息
        /// </summary>
        /// <param name="telephone"></param>
        public static void SendRegisterMess(string telephone,string password)
        {
            string Content = "欢迎使用国车APP服务,您的登陆密码" + password + ",请下载国车社区APP应用";
            SendSMSMess(telephone, Content);
        }

        /// <summary>
        /// 短信发送
        /// </summary>
        /// <param name="telephone"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static bool SendSMSMess(string telephone,string Content)
        {
            try
            {
                string _serverURL = "http://www.dh3t.com/json/sms/Submit";
                string _data = string.Empty;
                string _account = "dh84741";
                string _passWord = md5("0A73nybh");
                SendSmsData sendSmsData = new SendSmsData();
                sendSmsData.Phones = telephone.Trim();
                sendSmsData.Content = Content;
                sendSmsData.Msgid = Guid.NewGuid().ToString();
                sendSmsData.Sign = "【国车汽车超市】";
                sendSmsData.Subcode = "";//短信子码
                _data = packageSendSmsJsonData(_account, _passWord, sendSmsData);
                //调用接口发送短信
                postMethodConnServer(_serverURL, _data);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;  
        }

        public static void AddVerificationCode(string telephone,string vcode,int timeout)
        {
            VerificationCodeEntity entity = new VerificationCodeEntity();
            entity.Mobile = telephone;
            entity.Email = "";
            entity.VCode = vcode;
            entity.DeadLine = DateTime.Now.AddMinutes(timeout);
            entity.Status = 1;//0 失效 1有效
            BaseDataService.AddVerificationCode(entity);
        }
        //MD5加密程序（32位小写）
        private static string md5(string str)
        {
            byte[] result = Encoding.Default.GetBytes(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            String md = BitConverter.ToString(output).Replace("-", "");
            return md.ToLower();
        }


        private static string postMethodConnServer(string iServerURL, string iPostData)
        {
            string result = null;
            byte[] _buffer = Encoding.GetEncoding("utf-8").GetBytes(iPostData);
            HttpWebRequest _req = (HttpWebRequest)WebRequest.Create(iServerURL);
            _req.Method = "Post";
            _req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            _req.ContentLength = _buffer.Length;
            Stream _stream = null;
            Stream _resStream = null;
            StreamReader _resSR = null;
            try
            {
                _stream = _req.GetRequestStream();
                _stream.Write(_buffer, 0, _buffer.Length);
                _stream.Flush();
                HttpWebResponse _res = (HttpWebResponse)_req.GetResponse();

                //获取响应
                _resStream = _res.GetResponseStream();
                _resSR = new StreamReader(_resStream, Encoding.GetEncoding("utf-8"));
                result = _resSR.ReadToEnd();
                //MessageBox.Show(_resSR.ReadToEnd());
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message, "调用异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_stream != null)
                {
                    _stream.Close();
                }
                if (_resSR != null)
                {
                    _resSR.Close();
                }
                if (_resStream != null)
                {
                    _resStream.Close();
                }
            }
            return result;
        }

        private static string packageSendSmsJsonData(string account, string passwd, SendSmsData sendSmsData)
        {
            string data = "{\"account\":\"" + account + "\""
                        + ",\"password\":\"" + passwd + "\""
                        + ",\"msgid\":\"" + sendSmsData.Msgid + "\""
                        + ",\"phones\":\"" + sendSmsData.Phones + "\""
                        + ",\"content\":\"" + sendSmsData.Content + "\""
                        + ",\"sign\":\"" + sendSmsData.Sign + "\""
                        + ",\"subcode\":\"" + sendSmsData.Subcode + "\""
                        + "}";

            return data;
        }
    }
}
