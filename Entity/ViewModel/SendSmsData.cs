using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class SendSmsData
    {
        private String content;

        public String Content
        {
            get { return content; }
            set { content = value; }
        }
        private String phones;

        public String Phones
        {
            get { return phones; }
            set { phones = value; }
        }
        private String sign;

        public String Sign
        {
            get { return sign; }
            set { sign = value; }
        }
        private String subcode;

        public String Subcode
        {
            get { return subcode; }
            set { subcode = value; }
        }
        private String msgid;

        public String Msgid
        {
            get { return msgid; }
            set { msgid = value; }
        }
    }
}
