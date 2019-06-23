using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class BrandEntity
    {

        public long BrandID { get; set; }

        public string BrandName { get; set; }

        public string BrandNameEN { get; set; }

        public string ImageURL { get; set; }

        public string Description { get; set; }

        public int IsUse { get; set; }

        public AttachmentEntity Attachment { get; set; }
    }
}
