/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：AttachmentInfo
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：5/4/2018 3:10:05 PM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using Infrastructure.EntityFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataModel
{
    [Serializable]
    public class AttachmentInfo
    {
        [DataMapping("AttachmentID", DbType.Int64)]
        public long AttachmentID { get; set; }
        [DataMapping("FileName", DbType.String)]
        public string FileName { get; set; }
        [DataMapping("FileExtendName", DbType.String)]
        public string FileExtendName { get; set; }
        [DataMapping("FilePath", DbType.String)]
        public string FilePath { get; set; }
        [DataMapping("UploadDate", DbType.String)]
        public string UploadDate { get; set; }
        [DataMapping("FileType", DbType.String)]
        public string FileType { get; set; }
        [DataMapping("BusinessType", DbType.String)]
        public string BusinessType { get; set; }
        [DataMapping("Channel", DbType.String)]
        public string Channel { get; set; }
        [DataMapping("FileSize", DbType.String)]
        public string FileSize { get; set; }
        [DataMapping("Remark", DbType.String)]
        public string Remark { get; set; }
        [DataMapping("Operator", DbType.Int64)]
        public long Operator { get; set; }
        [DataMapping("CreateDate", DbType.DateTime)]
        public DateTime CreateDate { get; set; }
    }
}
