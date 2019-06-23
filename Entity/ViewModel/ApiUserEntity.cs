using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    [Serializable]
    public class ApiUserEntity
    {
        /// <summary>
        ///  状态码 200成功
        /// </summary>
        public string code { get; set; }
        public string token { get; set; }

        /// <summary>
        /// 状态码信息
        /// </summary>
        public string codeinfo { get; set; }
        public string vcode { get; set; }

        public CustomerEntity customerEntity { get; set; }
    }

    public class ApiReservationsEntity
    {
        /// <summary>
        ///  状态码 200成功
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 状态码信息
        /// </summary>
        public string codeinfo { get; set; }
    }
}
