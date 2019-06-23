using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    [Serializable]
    public class NewsEntity
    {
        public long ID { get; set; }

        public int ChannelID { get; set; }

        public string Title { get; set; }

        public string AttachmentIDs { get; set; }

        public string zhaiyao { get; set; }

        public string Content { get; set; }

        public int Sort { get; set; }

        public int Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public long Operator { get; set; }

        public string ImageUrl { get; set; }

        public List<AttachmentEntity> Attachments { get; set; }
    }


    [Serializable]
    public class NewsApiEntity
    {
        public long ID { get; set; }

        public int ChannelID { get; set; }

        public string Title { get; set; }

        public string AttachmentIDs { get; set; }

        public string zhaiyao { get; set; }

        public string Content { get; set; }

        public int Sort { get; set; }

        public int Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public long Operator { get; set; }

        public string ImageUrl { get; set; }

    }
}
