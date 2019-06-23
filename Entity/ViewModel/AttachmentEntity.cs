/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：AttachmentEntity
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：5/2/2018 2:29:46 PM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    [Serializable]
    public class AttachmentEntity
    {
        public long AttachmentID { get; set; }
        public string FileName { get; set; }
        public string FileExtendName { get; set; }
        public string FilePath { get; set; }
        public string UploadDate { get; set; }
        public string FileType { get; set; }
        public string BusinessType { get; set; }
        public string Channel { get; set; }
        public string FileSize { get; set; }
        public string Remark { get; set; }
        public long Operator { get; set; }
    }
}
