using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class VerificationCodeEntity
    {
        public long ID { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string VCode { get; set; }

        public int Status { get; set; }

        public DateTime DeadLine { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
